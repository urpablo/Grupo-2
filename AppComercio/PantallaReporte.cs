using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        private List<string> listaRefNE = new List<string>();
        private DataTable tablaEntregados = new DataTable();
        private DataTable tablaNoEntregados = new DataTable();
        private List<string> reportesReingresados = new List<string>();
        private string nombreArchivoReporte;

        // -------------------- cargar reporte, validar nombre de archivo y formato
        private void btnLeerReporteEntrega_Click(object sender, EventArgs e)
        {
            List<string> listaEntregados = new List<string>();
            List<string> listaNoEntregados = new List<string>();
            bool ArchivoOK = false;
            bool fallar = false;
            limpiezaValidacionesCarga();

            // Abre diálogo de seleccion de archivo
            DialogResult resultado = elegirReporteEntrega.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                nombreArchivoReporte = Path.GetFileName(elegirReporteEntrega.FileName);
                // Primero, quiero ver si el archivo está vacío
                if (new FileInfo(elegirReporteEntrega.FileName).Length == 0)
                {
                    MessageBox.Show($"El archivo {nombreArchivoReporte} está vacío." +
                                    $"\n \nSe lo descarta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiezaValidacionesCarga();
                    ArchivoOK = false;
                }
                else // como no está vacío,
                {
                    // Valida el nombre del archivo
                    if (nombreArchivoReporte.StartsWith("Entrega_")
                        && nombreArchivoReporte.Contains("_C")
                        && nombreArchivoReporte.Contains("_L")
                        && (nombreArchivoReporte.IndexOf("C") < nombreArchivoReporte.IndexOf("L"))
                        && nombreArchivoReporte.Contains(".txt"))
                    {
                        // Ahora valida el contenido
                        string[] lineasReporte = File.ReadAllLines(elegirReporteEntrega.FileName);
                        foreach (var linea in lineasReporte)
                        {
                            string[] lineaSpliteada = linea.Split(';');
                            foreach (var sublinea in lineaSpliteada)
                            {
                                if (sublinea.StartsWith("R") && int.TryParse(sublinea.Remove(0, 1), out int sublineaParseada))
                                {
                                    fallar = false;
                                }
                                else if ((sublinea.ToLower() == "true" || sublinea.ToLower() == "false"))
                                {
                                    fallar = false;
                                }
                                else
                                {
                                    fallar = true;
                                }
                            }
                        }

                        // Si el nombre de archivo es correcto pero el contenido no es correcto, falla...
                        if (fallar == true)
                        {
                            MessageBox.Show($"El archivo {nombreArchivoReporte} tiene un formato incorrecto. " +
                            "Debe tener líneas del estilo \n \n 'Rxxx;true' o bien 'Rxxx;false'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            limpiezaValidacionesCarga();
                        }
                        else
                        {
                            ArchivoOK = true;
                            btnCargarStockNoEntregados.Enabled = true;
                        }
                    }
                    // Si el nombre de archivo no es correcto o no es un archivo .txt, falla
                    else
                    {
                        MessageBox.Show($"El archivo {nombreArchivoReporte} tiene un nombre incorrecto, " +
                        "o no es un archivo de texto. Debe ser del estilo \n \n'Entrega_Cxxx_Lxxx.txt'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiezaValidacionesCarga();
                        ArchivoOK = false;
                    }
                }
            }
            else
            {
                btnCargarStockNoEntregados.Enabled = false;
                limpiezaValidacionesCarga();
            }

            // pasadas las validaciones iniciales y teniendo un archivo válido,
            if (ArchivoOK)
            {
                // carga el archivo y separa entre entregados / no entregados, tanto para los dgws como en listas.
                // Busca referencias duplicadas y prepara las cosas para un chequeo de reporte vacío.
                string archivoCodCliente = nombreArchivoReporte.Substring(nombreArchivoReporte.IndexOf("C"), (nombreArchivoReporte.IndexOf("_L") - nombreArchivoReporte.IndexOf("C")));
                string archivoCodLote = nombreArchivoReporte.Substring(nombreArchivoReporte.IndexOf("L"), (nombreArchivoReporte.IndexOf(".txt") - nombreArchivoReporte.IndexOf("L")));
                textBoxCodClienteReporte.Text = archivoCodCliente;
                textBoxCodLoteReporte.Text = archivoCodLote;

                tablaEntregados.Clear();
                tablaNoEntregados.Clear();

                string[] linesR = File.ReadAllLines(elegirReporteEntrega.FileName);
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
                        listaEntregados.Add(rowR[0].ToString());
                    }
                    else
                    {
                        for (int j = 0; j < valueR.Length; j++)
                        {
                            rowR[j] = valueR[j].Trim();
                            if (rowR[j] != "false")
                            {
                                listaRefNE.Add(rowR[j].ToString());
                            }
                        }
                        tablaNoEntregados.Rows.Add(rowR);
                        listaNoEntregados.Add(rowR[0].ToString());
                    }
                }

                dgwEntregados.Refresh();
                dgwNoEntregados.Refresh();

                // ahora reviso si hay duplicados en los codigos de referencia del reporte cargado. Si hay, error
                var BuscarDuplicadosEntregados = listaEntregados.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                var BuscarDuplicadosNoEntregados = listaNoEntregados.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                var BuscarDuplicadosEnAmbasListas = listaEntregados.Intersect(listaNoEntregados);
                if (BuscarDuplicadosNoEntregados.Count() >= 1 || BuscarDuplicadosEntregados.Count() >= 1 || BuscarDuplicadosEnAmbasListas.Count() >= 1)
                {
                    MessageBox.Show($"El reporte {nombreArchivoReporte} contiene códigos de referencia duplicados. \n \n" +
                                    $"Se lo descarta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BuscarDuplicadosEntregados.Clear();
                    BuscarDuplicadosNoEntregados.Clear();
                    limpiezaValidacionesCarga();
                }

                // chequeo por reporte vacío
                if (listaEntregados.Count() == 0 && listaNoEntregados.Count() == 0)
                {
                    MessageBox.Show($"El reporte {nombreArchivoReporte} está vacío. \n \nSe lo descarta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BuscarDuplicadosEntregados.Clear();
                    BuscarDuplicadosNoEntregados.Clear();
                    limpiezaValidacionesCarga();
                }

                //chequeo por todos verdaderos
                if (listaNoEntregados.Count() == 0)
                {
                    btnCargarStockNoEntregados.Enabled = false;
                    MessageBox.Show($"El reporte {nombreArchivoReporte} no contiene lotes devueltos o no entregados", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BuscarDuplicadosEntregados.Clear();
                    BuscarDuplicadosNoEntregados.Clear();
                }
            }
        }

        // -------------------- boton de carga de pedidos no entregados

        private void btnCargarStockNoEntregados_Click(object sender, EventArgs e)
        {
            List<int> listaPosicionesSeparadores = new List<int>();
            Dictionary<string, int> contenidoSpliteado = new Dictionary<string, int>();
            bool nada = true;
            bool modificado = false;
            int contadorSeparador = 1;
            int contadorPosicion = 1;

            // Resfresco stock para operar contra el último stock.txt cargado en el dgw de stock
            RefrescarStock();

            // ----------------------------------------------------------------------------------------------------------------------------------------------------
            // -------------------------------------------------------------- validaciones iniciales --------------------------------------------------------------
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            if (almenosunlotedespachado == false)
            {
                MessageBox.Show("Todavía no ha enviado ningún lote a logística, " +
                    "por lo tanto no podemos ingresar stock que no salió", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiarPantallaReporteEntrega();
            }

            if (reportesReingresados.Contains(nombreArchivoReporte))
            {
                MessageBox.Show($"El reporte {nombreArchivoReporte} ya fue reingresado anteriormente. Se descarta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiarPantallaReporteEntrega();
            }

            // ----------------------------------------------------------------------------------------------------------------------------------------------------
            // ------------------------------------------ comienza proceso de lectura de la salida y reingreso de stock -------------------------------------------
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            // Si este reporte no fue reingresado y el código de lote es distinto de cero, prosigo
            if (almenosunlotedespachado == true && !reportesReingresados.Contains(nombreArchivoReporte))
            {
                // Dado que hubo al menos un envío, este reporte nos puede pertenecer, reviso la salida por cada lote que fue enviado
                DirectoryInfo d = new DirectoryInfo(@"c:\Grupo2\");
                FileInfo[] Archivo = d.GetFiles("*.txt");
                foreach (FileInfo archivosLotes in Archivo)
                {
                    // Para cada uno que haya en el directorio, reviso que tenga el mismo
                    // codigo de cliente y de lote que el archivo de entrega que me dieron.
                    // Según la lógica de funcionamiento del programa y de los lineamientos
                    // el reporte de entrega y el número de lote coinciden en los datos
                    // que representan sus nombres de archivo.
                    string nombresLotes = archivosLotes.ToString();
                    if (nombresLotes.Substring(5) == nombreArchivoReporte.Substring(8))
                    {
                        // Dado que coincide, tengo el archivo que quiero para el reporte
                        // que me dieron de todos los que puede haber en el directorio,
                        // ahora quiero buscar las posiciones de los separadores "---"
                        nada = false;
                        string[] contenidoLote = File.ReadAllLines(@"c:\Grupo2\" + nombresLotes);
                        int cont = 0;
                        foreach (var linea in contenidoLote)
                        {
                            if (linea == "---")
                            {
                                listaPosicionesSeparadores.Add(cont);
                            }
                            cont++;

                            // podría agregar chequeo de no encontrar los divisores
                            // si fuese un archivo modificado maliciosamente
                            // luego de que el programa lo hubiera emitido
                        }

                        // Teniendo la posición de cada separador, por cada separador se que en la posición inmediata
                        // siguiente tengo la línea que comienza con el código de referencia
                        foreach (var posicionSeparador in listaPosicionesSeparadores)
                        {
                            // Esto no incluye el último separador dado que se va del rango de la lista de separadores
                            if (contadorSeparador < listaPosicionesSeparadores.Count)
                            {
                                // Me quedo con el primer resultado del split de la línea inmediata siguiente (+1) para saber el código de referencia
                                // si la lista de no entregados contiene este número de referencia, leo las líneas siguientes. sino sigo
                                var nroRefReporte = contenidoLote[posicionSeparador + 1].Split(';')[0];
                                if (listaRefNE.Contains(nroRefReporte))
                                {
                                    // Ahora leo de la posición +1 +1 en adelante hasta el próximo separador
                                    // spliteo cada producto y su cantidad y lo acumulo en un dictionary
                                    while (contenidoLote[(posicionSeparador + 1 + contadorPosicion)] != "---")
                                    {
                                        contenidoSpliteado.Add(contenidoLote[(posicionSeparador + 1 + contadorPosicion)].Split(';')[0], int.Parse(contenidoLote[(posicionSeparador + 1 + contadorPosicion)].Split(';')[1]));
                                        contadorPosicion++;
                                    }

                                    // Ahora que tengo en un dictionary todos los productos y sus cantidades para
                                    // el codigo de referencia, veo que carajo hago por cada uno
                                    foreach (var item in contenidoSpliteado)
                                    {
                                        // Recorro el dgw de stock por la columna de IDproducto y al encontrar
                                        // el producto de este punto del dictionary, voy a su correspondiente
                                        // fila con el stock real y lo sumo
                                        for (int i = 0; i < dgwStock.Rows.Count; i++)
                                        {
                                            if (dgwStock[0, i].Value.ToString() == item.Key.ToString().Substring(1))
                                            {
                                                dgwStock[1, i].Value = (int.Parse(dgwStock[1, i].Value.ToString()) + item.Value);
                                            }
                                        }
                                    }
                                }
                            }

                            // Por cada separador que se revisa y se avanza en el archivo al próximo codref,
                            // borro las referencias anteriores para evitar duplicados.
                            // Marco que modifiqué el stock, entonces debo pisar el stock.txt existente al terminar de iterar
                            contadorSeparador++;
                            contadorPosicion = 1;
                            contenidoSpliteado.Clear();
                            modificado = true;
                        }
                    }
                }

                // ----------------------------------------------------------------------------------------------------------------------------------------------------
                // ------------------------------ terminado proceso de lectura de todos los archivos pertinentes en la carpeta de salida ------------------------------
                // ----------------------------------------------------------------------------------------------------------------------------------------------------

                if (nada)
                {
                    MessageBox.Show($"El reporte {nombreArchivoReporte} no tiene coincidencias " +
                    "con ninguno de los lotes enviados a logística", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiarPantallaReporteEntrega();
                }

                if (modificado)
                {
                    using (StreamWriter objWriter = new StreamWriter("StockReingresado.txt"))
                    {
                        for (int row = 0; row < dgwStock.Rows.Count; row++)
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            for (int col = 0; col < dgwStock.Rows[row].Cells.Count; col++)
                            {
                                if (!String.IsNullOrEmpty(sb.ToString()))
                                    sb.Append(";");
                                sb.Append(dgwStock.Rows[row].Cells[col].Value.ToString());
                            }
                            objWriter.WriteLine(sb.ToString().Trim());
                        }
                    }

                    reportesReingresados.Add(nombreArchivoReporte);
                    File.Delete("Stock.txt");
                    File.Move("StockReingresado.txt", "Stock.txt");
                    MessageBox.Show($"Pedidos no entregados del reporte {nombreArchivoReporte} ingresados nuevamente al stock", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarPantallaReporteEntrega();
                }
            }
        }

        private void limpiezaValidacionesCarga()
        {
            tablaEntregados.Clear();
            tablaNoEntregados.Clear();
            dgwEntregados.Refresh();
            dgwNoEntregados.Refresh();
            btnCargarStockNoEntregados.Enabled = false;
        }

        private void limpiarPantallaReporteEntrega()
        {
            tablaEntregados.Clear();
            tablaNoEntregados.Clear();
            textBoxCodClienteReporte.Clear();
            textBoxCodLoteReporte.Clear();
        }
    }
}