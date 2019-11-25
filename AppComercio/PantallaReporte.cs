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

        DataTable tablaEntregados = new DataTable();
        DataTable tablaNoEntregados = new DataTable();


        // ----------------------------------------------- cargar reporte, validar nombre de archivo y formato --------------------------
        private void buttonLeerReporteEntrega_Click(object sender, EventArgs e)
        {

            // abre dialogo de seleccion de archivo
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
                {

                string nombreArchivo = Path.GetFileName(openFileDialog1.FileName);

                // valida el nombre del archivo
                if (nombreArchivo.StartsWith("Entrega_") 
                    && nombreArchivo.Contains("_C") 
                    && nombreArchivo.Contains("_L")
                    &&  (nombreArchivo.IndexOf("C") < nombreArchivo.IndexOf("L"))
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

                        string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                        string[] values;

                        for (int i = 0; i < lines.Length; i++)
                        {
                            values = lines[i].ToString().Split(';');
                            string[] row = new string[values.Length];

                            if (values.Contains("true"))
                            {
                                for (int j = 0; j < values.Length; j++)
                                {
                                    row[j] = values[j].Trim();
                                }
                                tablaEntregados.Rows.Add(row);

                            }
                            else
                            {
                                for (int j = 0; j < values.Length; j++)
                                {
                                    row[j] = values[j].Trim();
                                }
                                tablaNoEntregados.Rows.Add(row);
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

        //------------------------------- Habilitar botón de cargar pedidos no entregados si se ha cargado un reporte ------------------

        private void textBoxCodClienteReporte_TextChanged(object sender, EventArgs e)
        {
            habilitarCargarNoEntregados();
        }

        private void textBoxCodLoteReporte_TextChanged(object sender, EventArgs e)
        {
            habilitarCargarNoEntregados();
        }

        private void habilitarCargarNoEntregados()
        {

            if (!string.IsNullOrWhiteSpace(textBoxCodClienteReporte.Text)
               && !string.IsNullOrWhiteSpace(textBoxCodLoteReporte.Text))
            {
                btnCargarStockNoEntregados.Enabled = true;
            }
            else
            {
                btnCargarStockNoEntregados.Enabled = false;
            }
        }
    }


    }