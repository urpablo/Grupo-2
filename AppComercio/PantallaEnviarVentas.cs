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
        private int codRef = 0;
        private int codLote = 1;
        private List<int> RNGexistenteLote = new List<int>();
        private int codClienteRNGLote = 0;
        private bool almenosunlotedespachado = false;

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

            // levanta a memoria el stock actual
            var lineasstock = File
                      .ReadAllLines("Stock.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          b1 = int.Parse(record[0]),
                          b2 = int.Parse(record[1]),
                          b3 = int.Parse(record[2]),
                          b4 = int.Parse(record[3]),
                          b5 = int.Parse(record[4])
                      }).ToList();

            // levanta a memoria los pedidos que se hayan hecho para este lote
            var lineaspedidoArchivo = File
                      .ReadAllLines("Pedidos.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          a1 = record[0],
                          a2 = record[1],
                          a3 = int.Parse(record[2]),
                          a4 = int.Parse(record[3])
                      }).ToList();

            // levanta cualquier pedido pendiente de lotes anteriores por falta de stock
            var lineaspendientesArchivo = File
                     .ReadAllLines("PedidosPendientes.txt")
                     .Select(record => record.Split(';'))
                     .Select(record => new
                     {
                         a1 = record[0],
                         a2 = record[1],
                         a3 = int.Parse(record[2]),
                         a4 = int.Parse(record[3])
                     }).ToList();

            // los combino
            var lineaspedido = lineaspedidoArchivo.Concat(lineaspendientesArchivo).ToArray();

            // y vacío los pendientes para escribir luego el archivo con los pendientes de este lote
            File.WriteAllText("PedidosPendientes.txt", String.Empty);

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
                                d2 = int.Parse(record[1]),
                                d3 = int.Parse(record[2]),
                                d4 = int.Parse(record[3])
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
                     f4 = int.Parse(record[3])
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

            // codLote suma por cada TXT generado
            // Número aleatorio para el código de cliente.
            // Valido que el número aleatorio que se genere no se repita
            Random r = new Random();
            do
            {
                codClienteRNGLote = r.Next(0, 999);
            } while (RNGexistenteLote.Contains(codClienteRNGLote));
            RNGexistenteLote.Add(codClienteRNGLote);

            // formaeto de la vista previa
            if (almenosunlotedespachado == false) textBoxLote.AppendText("---" + Environment.NewLine);

            // Vuelco el lote en el textbox de vista previa sin la primera línea dado que ya la muestro en el textbox de arriba
            // Genero el nombre de archivo para este lote
            // El codigo de lote me sirve para avanzar el día, dado que es un lote diario
            string LoteGenerado = ("Lote_C" + codClienteRNGLote + "_L" + codLote + ".txt");
            textBoxLote.AppendText($"*** Día {codLote}: {LoteGenerado} ***" + Environment.NewLine);
            foreach (var line in File.ReadLines("PedidosFinal.txt").Skip(1))
            {
                textBoxLote.AppendText(line + Environment.NewLine);
            }
            codLote++;

            // Como borro el directorio Grupo2 en la carga del formulario, nunca va a haber una colisión por mismo nombre de archivo, aparte de tener chequeo por el RNG
            File.Move("PedidosFinal.txt", @"c:\Grupo2\" + LoteGenerado);

            // Limpiar archivos de este lote para poder comenzar un nuevo ciclo
            File.WriteAllText("Pedidos.txt", String.Empty);
            File.WriteAllText("PedidosAEnviar.txt", String.Empty);

            RefrescarStock();

            // como despache este lote, no tengo ventas cargadas pendientes, lo dejo en 0 y actualizo el label de estado
            cantidadVentasCargadas = 0;
          

            // para funcionalidad de chequeo por carga de reporte sin despacho anterior en reportes de entregas
            almenosunlotedespachado = true;

            // Notificación de que se completó exitosamente la generación del lote y del archivo de salida
            MessageBox.Show($"¡Lote diario generado! \n \nEl archivo {LoteGenerado} se encuentra " +
                "en la carpeta Grupo2 en la raíz del disco C. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnGenerarTXTLote.Enabled = false;
        }

        private void LabelEstadoLotes()
        {
            cantidadVentasPendientes = File.ReadAllLines("PedidosPendientes.txt").Count();

            labelEstadoLotes.Text = $"Ventas en lote diario: {cantidadVentasCargadas}\nVentas pendientes de stock: {cantidadVentasPendientes}";
            labelEstadoLotes.Refresh();
        }
    }
}