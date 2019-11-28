using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        List<string> listaCodNE = new List<string>();
        DataTable tablaEntregados = new DataTable();
        DataTable tablaNoEntregados = new DataTable();
        List<int> posSeparador = new List<int>();
        List<string> reportesReingresados = new List<string>();
        Dictionary<string,int> dictSpliteado = new Dictionary<string,int>();
        string nombreArchivo;
        bool nada = true;
        int cont = 0;
        int contSeparador = 1;
        int contadorboludo = 1;
        bool modificado = false;

        // ----------------------------------------------- cargar reporte, validar nombre de archivo y formato --------------------------
        private void buttonLeerReporteEntrega_Click(object sender, EventArgs e)
        {

            // abre dialogo de seleccion de archivo
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {

                nombreArchivo = Path.GetFileName(openFileDialog1.FileName);

                // valida el nombre del archivo
                if (nombreArchivo.StartsWith("Entrega_")
                    && nombreArchivo.Contains("_C")
                    && nombreArchivo.Contains("_L")
                    && (nombreArchivo.IndexOf("C") < nombreArchivo.IndexOf("L"))
                    && nombreArchivo.Contains(".txt"))
                {
                    // ahora valida el contenido
                    bool seprendiofuego = false;
                    string[] lineas = File.ReadAllLines(openFileDialog1.FileName);
                    foreach (var linea in lineas)
                    {
                        string[] ASD = linea.Split(';');
                        foreach (var line in ASD)
                        {
                            if (line.StartsWith("R") && int.TryParse(line.Remove(0, 1), out int rowParseado))
                            {
                                seprendiofuego = false;
                            }
                            else if ((line.ToLower() == "true" || line.ToLower() == "false"))
                            {
                                seprendiofuego = false;
                            }
                            else
                            {
                                seprendiofuego = true;
                            }

                        }
                    }

                    // si el nombre de archivo es correcto pero el contenido no es correcto, falla. 
                    if (seprendiofuego == true)
                    {
                        DialogResult resultadoMSGbox = MessageBox.Show("Ha elegido un archivo con un formato incorrecto. " +
                            "Debe tener líneas del estilo \n \n 'Rxxx;true' o bien 'Rxxx;false'", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // sino, lo carga, y separa entre entregados y no entregados
                        string archivoCodCliente = nombreArchivo.Substring(nombreArchivo.IndexOf("C"), (nombreArchivo.IndexOf("_L") - nombreArchivo.IndexOf("C")));
                        string archivoCodLote = nombreArchivo.Substring(nombreArchivo.IndexOf("L"), (nombreArchivo.IndexOf(".txt") - nombreArchivo.IndexOf("L")));
                        textBoxCodClienteReporte.Text = archivoCodCliente;
                        textBoxCodLoteReporte.Text = archivoCodLote;

                        dgwEntregados.DataSource = tablaEntregados;
                        dgwNoEntregados.DataSource = tablaNoEntregados;

                        tablaEntregados.Rows.Clear();
                        tablaNoEntregados.Rows.Clear();
                        dgwEntregados.Refresh();
                        dgwNoEntregados.Refresh();

                        
                        string[] linesR = File.ReadAllLines(openFileDialog1.FileName);
                        string[] valueR;

                        for (int i = 0; i < linesR.Length; i++)
                        {
                            valueR = linesR[i].ToString().Split(';');
                            string[] rowR = new string[valueR.Length];

                            if (valueR.Contains("true"))
                            {
                                for (int j = 0; j < valueR.Length; j++)
                                {
                                    rowR[j] = valueR[j].Trim();
                                }
                                tablaEntregados.Rows.Add(rowR);

                            }
                            else
                            {
                                for (int j = 0; j < valueR.Length; j++)
                                {
                                    rowR[j] = valueR[j].Trim();
                                    if (rowR[j] != "false")
                                    {
                                         listaCodNE.Add(rowR[j].ToString());
                                    }
                                }
                                tablaNoEntregados.Rows.Add(rowR);

                            }


                        }




                    }


                }
                //si el nombre de archivo no es correcto o no es un archivo .txt, falla
                else
                {
                    DialogResult resultadoMSGbox = MessageBox.Show("Ha elegido un archivo con un nombre incorrecto, " +
                        "o no es un archivo de texto. Debe ser del estilo \n \n 'Entrega_Cxxx_Lxxx.txt'", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        // ------------------------- boton de carga de pedidos no entregados -------------------

        private void limpiarPantallaReporteEntrega()
        {
            tablaEntregados.Clear();
            tablaNoEntregados.Clear();
            textBoxCodClienteReporte.Clear();
            textBoxCodLoteReporte.Clear();
        }

        private void btnCargarStockNoEntregados_Click(object sender, EventArgs e)
        {
            refrescarstock();

            // reviso si se ha enviado algún lote, por si se cargo un reporte antes de confirmar un lote
            if (codLote == 0)
           {
                MessageBox.Show("Todavía no ha enviado ningún lote a logística, " +
                    "por lo tanto no podemos ingresar stock que no salió", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiarPantallaReporteEntrega();
            }
            else
            {
                // revisar si este reporte había sido reingresado anteriormente
                if (reportesReingresados.Contains(nombreArchivo))
                {
                    MessageBox.Show("Este reporte ya fue reingresado anteriormente. Se descarta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiarPantallaReporteEntrega();
                }
                else
                {
                    // dado que hubo al menos un envío, este reporte nos puede pertenecer, reviso la salida por los archivos de lotes que se generaron
                    DirectoryInfo d = new DirectoryInfo(@"c:\Grupo2\");
                    FileInfo[] Files = d.GetFiles("*.txt");
                    foreach (FileInfo archivosLotes in Files)
                    {
                        // para cada uno que haya en el directorio, reviso que tenga el mismo 
                        // codigo de cliente y de lote que el archivo de entrega que me dieron
                        string nombresLotes = archivosLotes.ToString();
                        if (nombresLotes.Substring(5) == nombreArchivo.Substring(8))
                        {
                            // dado que coincide, tengo el archivo que quiero para el reporte 
                            // que me dieron de todos los que puede haber en el directorio, 
                            // ahora quiero buscar las posiciones de los separadores "---" 
                            nada = false;
                            string[] contenidoLote = File.ReadAllLines(@"c:\Grupo2\" + nombresLotes);
                            foreach (var line in contenidoLote)
                            {
                                if (line == "---")
                                {
                                    posSeparador.Add(cont);
                                }
                                cont++;
                            }

                            // teniendo la posición de cada separador, por cada separador se que en la posición inmediata
                            // siguiente tengo la línea que comienza con el código de referencia
                            foreach (var separador in posSeparador)
                            {
                                // Esto no incluye el último separador dado que se va del rango de la lista de separadores
                                if (contSeparador < posSeparador.Count)
                                {
                                    // me quedo con el primer resultado del split de la línea inmediata siguiente para saber el código de referencia
                                    // si la lista de no entregados contiene este número de referencia, leo las líneas siguientes. sino sigo
                                    var nroRef = contenidoLote[separador + 1].Split(';')[0];
                                    if (listaCodNE.Contains(nroRef))
                                    {
                                        // ahora leo de la posición +1 +1 en adelante hasta el próximo separador
                                        // spliteo cada producto y su cantidad y lo acumulo en un dictionary
                                        while (contenidoLote[(separador + 1 + contadorboludo)] != "---")
                                        {
                                            dictSpliteado.Add(contenidoLote[(separador + 1 + contadorboludo)].Split(';')[0], int.Parse(contenidoLote[(separador + 1 + contadorboludo)].Split(';')[1]));
                                            contadorboludo++;
                                        }

                                        // ahora que tengo en un dictionary todos los productos y sus cantidades para 
                                        // el codigo de referencia, veo que carajo hago por cada uno
                                        foreach (var item in dictSpliteado)
                                        {
                                            // recorro el dgw de stock por la columna de IDproducto y al encontrar 
                                            // el producto de este punto del dictionary, voy a su correspondiente
                                            // fila con el stock real y lo sumo
                                            foreach (DataGridViewRow dr in dgwStock.Rows)
                                            {
                                                if (dr.Cells[tablaStock.Columns.IndexOf("ID")].Value.ToString() == item.Key.ToString().Substring(1))
                                                {
                                                    dr.Cells[tablaStock.Columns.IndexOf("Real")].Value = (int.Parse(dr.Cells[tablaStock.Columns.IndexOf("Real")].Value.ToString()) + item.Value);
                                                }
                                            }

                                        }
                                    }
                                }


                                // por cada separador que se revisa y se avanza en el archivo al próximo codref, borro las referencias anteriores para evitar duplicados
                                // marco que modifiqué el stock, entonces debo pisar el stock.txt existente al terminar de iterar
                                contSeparador++;
                                contadorboludo = 1;
                                dictSpliteado.Clear();
                                modificado = true;
                            }
                        }
                    }

                    // si ninguno de los archivos del directorio tuvo coincidencias, este reporte no nos pertenece. Se descarta
                    if (nada == true)
                    {
                        MessageBox.Show("Este reporte de entrega no coincide " +
                        "con ninguno de los lotes enviados a logística", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarPantallaReporteEntrega();
                    }


                    if (modificado == true)
                    {
                        // ahora que termine, piso el stock.txt con lo que tiene el datagridview de stock luego de este proceso
                        using (StreamWriter objWriter = new StreamWriter("StockReingresado.txt"))
                        {
                            for (Int32 row = 0; row < dgwStock.Rows.Count; row++)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                for (Int32 col = 0; col < dgwStock.Rows[row].Cells.Count; col++)
                                {
                                    if (!String.IsNullOrEmpty(sb.ToString()))
                                        sb.Append(";");
                                    sb.Append(dgwStock.Rows[row].Cells[col].Value.ToString());
                                }
                                objWriter.WriteLine(sb.ToString().Trim());
                            }
                        }

                        // guardo el nombre del archivo para no procesarlo nuevamente si se reingresa
                        reportesReingresados.Add(nombreArchivo);

                        File.Delete("Stock.txt");
                        File.Move("StockReingresado.txt", "Stock.txt");

                        // confirmación de que salió todo bien
                        MessageBox.Show("Pedidos no entregados del reporte de entrega " + nombreArchivo + " ingresados nuevamente al stock", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarPantallaReporteEntrega();
                    }


                }

                
               
            }

        }

    }

}
