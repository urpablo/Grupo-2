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




        string ultimoPedidoGuardado;
        int codPedidoInd;
        int codRefPedido;
        int codLote;
        bool actualizo;

        // -------------------- boton agregar item de cargar ventas ------------------------------------
        public void buttonAgregarItem_Click(object sender, EventArgs e)
        {

            

            actualizo = false;
            if (int.TryParse(textBoxCant.Text, out int sumar1) == false || sumar1 < 0)
            {
                DialogResult resultadoMSGbox = MessageBox.Show("No se puede ingresar una cantidad negativa " +
                    "o algo que no sea un número, intente nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                foreach (ListViewItem lvi in listPedidos.Items)
                {
                    if (lvi.SubItems[0].Text == comboBoxCodProducto.Text)
                    {
                        int.TryParse(lvi.SubItems[1].Text, out int original);
                        int.TryParse(textBoxCant.Text, out int sumar);

                        lvi.SubItems[1].Text = "" + (original + sumar);

                        actualizo = true;
                        textBoxCant.Clear();
                    }

                }

                if (actualizo == false)
                {
                    ListViewItem lstPedido = new ListViewItem(comboBoxCodProducto.Text);
                    lstPedido.SubItems.Add(textBoxCant.Text);
                    listPedidos.Items.Add(lstPedido);
                    textBoxCant.Clear();

                }

            }

            HabilitarConfirmarPedido();

        }
        

        // ---------------- boton confirmar pedido de cargar ventas ---------------------------------------
        private void buttonGenerarPedido_Click(object sender, EventArgs e)
        {
            int IdStock = 0;
            int KStock = 0;
            int IdPed = 0;
            int KPed = 0;
            int kComp = 0;
            string parametrosinv;

            Dictionary<int, string> InventarioTemporal = new Dictionary<int, string>();

            using (StreamWriter sw = new StreamWriter("PedidoTemporal.txt"))
            {
                foreach (ListViewItem item in listPedidos.Items)
                {
                    sw.Write(textBoxCdCli.Text + ";" + textBoxDirEnt.Text + ";");
                    sw.Write(item.Text);
                    for (int i = 1; i < item.SubItems.Count; i++)
                        sw.Write(";" + item.SubItems[i].Text);
                    sw.Write("\n");
                }
            }

            //levanta en memoria el pedido temporal
            var lineaspedido = File
                      .ReadAllLines("PedidoTemporal.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          a1 = record[0],
                          a2 = record[1],
                          a3 = Int32.Parse(record[2]),
                          a4 = Int32.Parse(record[3])
                      }).ToList();

            //levanta en memoria el stock actual
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

            //agrega comprometido a stock temporal
            foreach (var registroStock in lineasstock)
            {
                IdStock = registroStock.b1;
                KStock = registroStock.b4;

                foreach (var registroPedido in lineaspedido)
                {
                    IdPed = registroPedido.a3;
                    KPed = registroPedido.a4;

                    if (IdStock == IdPed)
                    {
                        int sumcomprometido = KStock + KPed;
                        parametrosinv = registroStock.b2 + ";" + registroStock.b3 + ";" + sumcomprometido + ";" + registroStock.b5;

                        InventarioTemporal.Add(IdStock, parametrosinv);

                    }
                }
                if (!InventarioTemporal.ContainsKey(IdStock))
                {
                    parametrosinv = registroStock.b2 + ";" + registroStock.b3 + ";" + KStock + ";" + registroStock.b5;
                    InventarioTemporal.Add(IdStock, parametrosinv);
                }


            }

            //Crea el txt de stock temporal
            using (StreamWriter sw2 = new StreamWriter("StockTemporal.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporal)
                {
                    sw2.Write(entry.Key);
                    sw2.Write(";");
                    sw2.Write(entry.Value);
                    sw2.Write("\n");
                }
            }

            File.Delete("Stock.txt");
            File.Move("StockTemporal.txt", "Stock.txt");

            

            using (StreamWriter sw3 = File.AppendText("Pedidos.txt"))
            {

                string[] leertexto = File.ReadAllLines("PedidoTemporal.txt");
                foreach (string s in leertexto)
                {
                    sw3.Write(s);
                    sw3.Write("\n");

                }
            }

            MessageBox.Show("¡Pedido agregado exitosamente al lote actual! \n \n Cuando termine de agregar ventas, puede generar el lote final diario para logística desde la sección enviar ventas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            limpiarlistapedidos();
        }


        // ----------------- boton limpiar pedidos de cargar ventas -------------------------------------------
        private void buttonLimpiarListaPedidos_Click(object sender, EventArgs e)
        {
            limpiarlistapedidos();
        }

        private void limpiarlistapedidos()
        {
            listPedidos.Items.Clear();
            textBoxDirEnt.Clear();
            textBoxCdCli.Clear();
            textBoxCant.Clear();
        }


        private void buttonGenerarTXTLote_Click(object sender, EventArgs e)
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

            using (StreamWriter sw8 = new StreamWriter("PedidosFinal.txt"))
            {
                sw8.Write(textBoxRzSoc.Text + ";" + textBoxCUIT2.Text + ";" + textBoxDirDevComercio.Text);
                sw8.Write("\n");
                sw8.Write("---");
                sw8.Write("\n");

            }

            int i = 0;
            int j = 0;
            int z = 0;
            foreach (var registroRef in lineasreferencia)
            {
                string cdRef = registroRef.e1;
                string cdRef2 = registroRef.e2;

                

                using (StreamWriter sw9 = File.AppendText("PedidosFinal.txt"))
                {

                    sw9.Write("R"+i.ToString()+j.ToString()+z.ToString()+ ";" + cdRef2);
                    sw9.Write("\n");
                    sw9.Write("---");
                    sw9.Write("\n");

                }

                i = i+1;
                j = j+1;
                z = z+1;

                foreach (var registroPedEnv in lineasPedidosaEnviar)
                {
                    string CdPedEnv = registroPedEnv.f3;
                    int CdPedEnv2 = registroPedEnv.f4;
                    string CdPedEnv3 = registroPedEnv.f1;


                    if (cdRef == CdPedEnv3)
                    {
                        using (StreamWriter sw10 = File.AppendText("PedidosFinal.txt"))
                        {

                            sw10.Write(CdPedEnv + ";" + CdPedEnv2);
                            sw10.Write("\n");

                        }



                    }

                }

                using (StreamWriter sw11 = File.AppendText("PedidosFinal.txt"))
                {

                    sw11.Write("---");
                    sw11.Write("\n");

                }


            }

            Random r = new Random();

            int q = r.Next(0, 999);
           







            File.Move("PedidosFinal.txt", @"c:\Grupo2\" + "Lote_C" + q + "_" + "L" + q + ".txt");
            MessageBox.Show("¡Lote diario generado! \n \n El archivo se encuentra en la carpeta Grupo2 en la raíz del disco C. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            buttonGenerarTXTLote.Enabled = false;

            foreach (var line in File.ReadLines(@"c:\Grupo2\" + "Lote_C" + q + "_" + "L" + q + ".txt"))
            {

                textBoxLote.AppendText(line + Environment.NewLine);
            }

        }








 
    }
}