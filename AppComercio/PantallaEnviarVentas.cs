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
        // contadores de código de referencia y código de lote
        private int codRef = 0;

        private int codLote = 0;
        private List<int> RNGexistenteLote = new List<int>();
        private int codClienteRNGLote = 0;

        // ------------------ boton generar lote para logística
        private void btnGenerarTXTLote_Click(object sender, EventArgs e)
        {
            int IdStock = 0;
            int KStock = 0;
            int IdPed = 0;
            int KPed = 0;
            int kComp = 0;
            string RefPed;
            string parametrosinv;

            Dictionary<int, string> InventarioTemporalLote = new Dictionary<int, string>();
            Dictionary<string, string> ArmaLote = new Dictionary<string, string>();

            var lineasstock = File
                      .ReadAllLines("Stock.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          b1 = Int32.Parse(record[0]),
                          b2 = Int32.Parse(record[1]),
                          b3 = Int32.Parse(record[2]),
                          b4 = Int32.Parse(record[3]),
                          b5 = Int32.Parse(record[4])
                      }).ToList();

            var lineaspedido = File
                      .ReadAllLines("Pedidos.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          a1 = record[0],
                          a2 = record[1],
                          a3 = Int32.Parse(record[2]),
                          a4 = Int32.Parse(record[3])
                      }).ToList();

            foreach (var registroStock in lineasstock)
            {
                IdStock = registroStock.b1;
                KStock = registroStock.b2;
                kComp = registroStock.b4;

                foreach (var registroPedido in lineaspedido)
                {
                    IdPed = registroPedido.a3;
                    KPed = registroPedido.a4;
                    RefPed = registroPedido.a1;

                    if (IdStock == IdPed)
                    {
                        if (KStock > KPed)
                        {
                            int sumact = KStock - KPed;
                            int sumcomp = kComp - KPed;
                            parametrosinv = sumact + ";" + registroStock.b3 + ";" + sumcomp + ";" + registroStock.b5;

                            if (InventarioTemporalLote.ContainsKey(IdStock))
                            {
                                using (StreamWriter sw7 = new StreamWriter("lineaindividual.txt"))
                                {
                                    string valor = InventarioTemporalLote[IdStock];
                                    sw7.Write(IdStock);
                                    sw7.Write(";");
                                    sw7.Write(valor);
                                    sw7.Write("\n");
                                }
                                var lineaspedido2 = File
                            .ReadAllLines("lineaindividual.txt")
                            .Select(record => record.Split(';'))
                            .Select(record => new
                            {
                                d1 = record[0],
                                d2 = Int32.Parse(record[1]),
                                d3 = Int32.Parse(record[2]),
                                d4 = Int32.Parse(record[3])
                            }).ToList();

                                foreach (var registroLinea in lineaspedido2)
                                {
                                    int actuallinea = registroLinea.d2;
                                    int actuallinea2 = registroLinea.d4;
                                    parametrosinv = (actuallinea - KPed) + ";" + registroStock.b3 + ";" + (actuallinea2 - KPed) + ";" + registroStock.b5;
                                }

                                InventarioTemporalLote.Remove(IdStock);
                            }

                            InventarioTemporalLote.Add(IdStock, parametrosinv);

                            using (StreamWriter sw6 = File.AppendText("PedidosAEnviar.txt"))
                            {
                                sw6.Write(registroPedido.a1 + ";" + registroPedido.a2 + ";" + IdPed + ";" + KPed);
                                sw6.Write("\n");
                            }

                            if (!ArmaLote.ContainsKey(RefPed))
                            {
                                ArmaLote.Add(RefPed, registroPedido.a2);
                            }
                        }
                        else
                        {
                            parametrosinv = registroStock.b2 + ";" + registroStock.b3 + ";" + registroStock.b4 + ";" + registroStock.b5;
                            InventarioTemporalLote.Add(IdStock, parametrosinv);

                            using (StreamWriter sw5 = File.AppendText("PedidosPendientes.txt"))
                            {
                                sw5.Write(registroPedido.a1 + ";" + registroPedido.a2 + ";" + IdPed + ";" + KPed);
                                sw5.Write("\n");
                            }
                        }
                    }
                }
                if (!InventarioTemporalLote.ContainsKey(IdStock))
                {
                    parametrosinv = registroStock.b2 + ";" + registroStock.b3 + ";" + kComp + ";" + registroStock.b5;
                    InventarioTemporalLote.Add(IdStock, parametrosinv);
                }
            }

            using (StreamWriter sw4 = new StreamWriter("StockTemporalSegundo.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporalLote)
                {
                    sw4.Write(entry.Key);
                    sw4.Write(";");
                    sw4.Write(entry.Value);
                    sw4.Write("\n");
                }
            }

            using (StreamWriter sw6 = new StreamWriter("Listadereferencias.txt"))
            {
                foreach (KeyValuePair<string, string> entry in ArmaLote)
                {
                    sw6.Write(entry.Key);
                    sw6.Write(";");
                    sw6.Write(entry.Value);
                    sw6.Write("\n");
                }
            }

            File.Delete("Stock.txt");
            File.Move("StockTemporalSegundo.txt", "Stock.txt");

            var lineasreferencia = File
                      .ReadAllLines("Listadereferencias.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          e1 = record[0],
                          e2 = record[1]
                      }).ToList();

            var lineasPedidosaEnviar = File
                 .ReadAllLines("PedidosAEnviar.txt")
                 .Select(record => record.Split(';'))
                 .Select(record => new
                 {
                     f1 = record[0],
                     f2 = record[1],
                     f3 = record[2],
                     f4 = Int32.Parse(record[3])
                 }).ToList();

            // escribe header con los datos del remitente
            using (StreamWriter sw8 = new StreamWriter("PedidosFinal.txt"))
            {
                sw8.Write(textBoxRZ2.Text + ";" + textBoxCUIT2.Text + ";" + textBoxDirDevComercio.Text);
                sw8.Write("\n");
                sw8.Write("---");
                sw8.Write("\n");
            }

            foreach (var registroRef in lineasreferencia)
            {
                string cdRef = registroRef.e1;
                string cdRef2 = registroRef.e2;
                codRef++;

                using (StreamWriter sw9 = File.AppendText("PedidosFinal.txt"))
                {
                    sw9.Write("R" + codRef.ToString() + ";" + cdRef2);
                    sw9.Write("\n");
                }

                foreach (var registroPedEnv in lineasPedidosaEnviar)
                {
                    string CdPedEnv = registroPedEnv.f3;
                    int CdPedEnv2 = registroPedEnv.f4;
                    string CdPedEnv3 = registroPedEnv.f1;

                    if (cdRef == CdPedEnv3)
                    {
                        using (StreamWriter sw10 = File.AppendText("PedidosFinal.txt"))
                        {
                            sw10.Write("P" + CdPedEnv + ";" + CdPedEnv2);
                            sw10.Write("\n");
                        }
                    }
                }

                // último ---
                using (StreamWriter sw11 = File.AppendText("PedidosFinal.txt"))
                {
                    sw11.Write("---");
                    sw11.Write("\n");
                }
            }

            // Vuelco el lote en el textbox de vista previa sin la primera línea dado que ya la muestro en el textbox de arriba
            textBoxLote.Text = "";
            foreach (var line in File.ReadLines("PedidosFinal.txt").Skip(1))
            {
                textBoxLote.AppendText(line + Environment.NewLine);
            }

            // codLote suma por cada TXT generado
            // Número aleatorio para el código de cliente.
            // Valido que el número aleatorio que se genere no se repita
            Random r = new Random();
            do{codClienteRNGLote = r.Next(0, 999);
            } while (RNGexistenteLote.Contains(codClienteRNGLote));
            RNGexistenteLote.Add(codClienteRNGLote);
            codLote++;

            // Como borro el directorio Grupo2 en la carga del formulario, nunca va a haber una colisión por mismo nombre de archivo
            File.Move("PedidosFinal.txt", @"c:\Grupo2\" + "Lote_C" + codClienteRNGLote + "_L" + codLote + ".txt");
            string LoteGenerado = ("Lote_C" + codClienteRNGLote + "_L" + codLote + ".txt");

            // Aumenta el contador de lote una vez que se generó
            // Notificación de que se completó exitosamente la generación del lote y del archivo de salida
            MessageBox.Show($"¡Lote diario generado! \n \nEl archivo {LoteGenerado} se encuentra " +
                "en la carpeta Grupo2 en la raíz del disco C. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnGenerarTXTLote.Enabled = false;

            // Limpiar archivos de este lote para poder comenzar un nuevo ciclo
            File.Delete("Pedidos.txt");
            File.Delete("PedidosAEnviar.txt");

            RefrescarEntregasStockIndustrias();
            RefrescarStock();
        }
    }
}