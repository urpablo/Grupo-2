﻿using System;
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
        private List<string> listaEntregados = new List<string>();
        private List<string> listaNoEntregados = new List<string>();
        private DataTable tablaEntregados = new DataTable();
        private DataTable tablaNoEntregados = new DataTable();
        private List<int> listaPosicionesSeparadores = new List<int>();
        private List<string> reportesReingresados = new List<string>();
        private Dictionary<string, int> contenidoSpliteado = new Dictionary<string, int>();
        private string nombreArchivoReporte;
        private bool nada = true;
        private int contadorSeparador = 1;
        private int contadorPosicion = 1;
        private bool modificado = false;
        private bool RefDuplicadas = false;

        // -------------------- cargar reporte, validar nombre de archivo y formato
        private void btnLeerReporteEntrega_Click(object sender, EventArgs e)
        {
            // Abre dialogo de seleccion de archivo
            DialogResult resultado = elegirReporteEntrega.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                nombreArchivoReporte = Path.GetFileName(elegirReporteEntrega.FileName);
                // Valida el nombre del archivo
                if (nombreArchivoReporte.StartsWith("Entrega_")
                    && nombreArchivoReporte.Contains("_C")
                    && nombreArchivoReporte.Contains("_L")
                    && (nombreArchivoReporte.IndexOf("C") < nombreArchivoReporte.IndexOf("L"))
                    && nombreArchivoReporte.Contains(".txt"))
                {
                    // Ahora valida el contenido
                    bool fallar = false;
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
                        MessageBox.Show("Ha elegido un archivo con un formato incorrecto. " +
                        "Debe tener líneas del estilo \n \n 'Rxxx;true' o bien 'Rxxx;false'", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // ...sino, lo carga y separa entre entregados / no entregados. Busca referencias duplicadas
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
                                listaEntregados.Add(rowR[0].ToString().Substring(0, 1));

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
                                listaNoEntregados.Add(rowR[0].ToString().Substring(0,1));
                            }
                        }

                        dgwEntregados.Refresh();
                        dgwNoEntregados.Refresh();

                        // ahora reviso si hay duplicados en los codigos de referencia. Si los hay, error
                        var BuscarDuplicadosEntregados = listaEntregados.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                        if (BuscarDuplicadosEntregados.Count >= 1)
                        {
                            RefDuplicadas = true;
                        }

                        var BuscarDuplicadosNoEntregados = listaNoEntregados.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                        if (BuscarDuplicadosNoEntregados.Count >= 1)
                        {
                            RefDuplicadas = true;
                        }

                        if (RefDuplicadas == true)
                        {
                            MessageBox.Show($"El reporte {nombreArchivoReporte} contiene códigos de referencia duplicados. \n \nSe lo descarta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tablaEntregados.Clear();
                            tablaNoEntregados.Clear();
                            dgwEntregados.Refresh();
                            dgwNoEntregados.Refresh();

                            // reseteo el chequeo de duplicados para una nueva carga
                            RefDuplicadas = false;
                            listaEntregados.Clear();
                            listaNoEntregados.Clear();
                        }
                    }
                }
                // Si el nombre de archivo no es correcto o no es un archivo .txt, falla
                else
                {
                    MessageBox.Show($"{nombreArchivoReporte} tiene un nombre incorrecto, " +
                    "o no es un archivo de texto. Debe ser del estilo \n \n'Entrega_Cxxx_Lxxx.txt'", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // -------------------- boton de carga de pedidos no entregados

        private void limpiarPantallaReporteEntrega()
        {
            tablaEntregados.Clear();
            tablaNoEntregados.Clear();
            textBoxCodClienteReporte.Clear();
            textBoxCodLoteReporte.Clear();
        }

        private void btnCargarStockNoEntregados_Click(object sender, EventArgs e)
        {
            RefrescarStock();

            // Reviso si se ha enviado algún lote, por si se cargo un reporte antes de confirmar un lote. Si es, error y limpio pantalla
            if (codLote == 0)
            {
                MessageBox.Show("Todavía no ha enviado ningún lote a logística, " +
                    "por lo tanto no podemos ingresar stock que no salió", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiarPantallaReporteEntrega();
            }
            else
            {
                // Revisar si este reporte había sido reingresado anteriormente. Si lo fue, error y limpio pantalla
                if (reportesReingresados.Contains(nombreArchivoReporte))
                {
                    MessageBox.Show($"El reporte {nombreArchivoReporte} ya fue reingresado anteriormente. Se descarta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiarPantallaReporteEntrega();
                }
                else
                {
                    // Dado que hubo al menos un envío, este reporte nos puede pertenecer, reviso la salida por los archivos de lotes que se generaron
                    DirectoryInfo d = new DirectoryInfo(@"c:\Grupo2\");
                    FileInfo[] Archivo = d.GetFiles("*.txt");
                    foreach (FileInfo archivosLotes in Archivo)
                    {
                        // Para cada uno que haya en el directorio, reviso que tenga el mismo
                        // codigo de cliente y de lote que el archivo de entrega que me dieron
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

                    // Si ninguno de los archivos del directorio tuvo coincidencias, este reporte no nos pertenece. Se descarta
                    // y se limpia la pantalla
                    if (nada)
                    {
                        MessageBox.Show($"El reporte {nombreArchivoReporte} no tiene coincidencias " +
                        "con ninguno de los lotes enviados a logística", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarPantallaReporteEntrega();
                    }

                    

                    if (modificado)
                    {
                        // Ahora que termine, piso el stock.txt con lo que tiene el datagridview de stock luego de este proceso
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

                        // Guardo el nombre del archivo para no procesarlo nuevamente si se reingresa
                        reportesReingresados.Add(nombreArchivoReporte);

                        // piso el stock actual con el reingresado
                        File.Delete("Stock.txt");
                        File.Move("StockReingresado.txt", "Stock.txt");

                        // Confirmación de que salió todo bien, limpio la pantalla y seteo modificado en false, nada en true como al principio,
                        // para evitar ingresos de reportes de formato válido, no emitidos por el programa 
                        MessageBox.Show($"Pedidos no entregados del reporte de entrega {nombreArchivoReporte} ingresados nuevamente al stock", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        modificado = false;
                        nada = true;
                        limpiarPantallaReporteEntrega();
                    }


                }
            }
        }
    }
}