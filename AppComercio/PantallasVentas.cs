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
        

        public void buttonAgregarItem_Click(object sender, EventArgs e)
        {
            ListViewItem lstPedido = new ListViewItem(textBoxCdProd.Text);
            lstPedido.SubItems.Add(textBoxCant.Text);
            listPedidos.Items.Add(lstPedido);
            textBoxCant.Clear();
            textBoxCdProd.Clear();

        }

        private void buttonGenerarPedido_Click(object sender, EventArgs e)
        {
            bool flag = true;
            int IdStock = 0;
            int KStock = 0;
            int IdPed = 0;
            int KPed = 0;
            codRefPedido = codRefPedido + 1;

            Dictionary<int, int> InventarioTemporal = new Dictionary<int, int>();
            List<string> lista = new List<string>();

            //crea pedido temporal hasta ser validado
            using (StreamWriter sw = new StreamWriter("PedidoTemporal.txt"))
            {
                foreach (ListViewItem item in listPedidos.Items)
                {
                    sw.Write(item.Text);
                    for (int i = 1; i < item.SubItems.Count; i++)
                        sw.Write("," + item.SubItems[i].Text);
                    sw.Write("\n");
                }
            }

            //levanta en memoria el pedido temporal
            var lineaspedido = File
                      .ReadAllLines("PedidoTemporal.txt")
                      .Select(record => record.Split(','))
                      .Select(record => new
                      {
                          a1 = Int32.Parse(record[0]),
                          a2 = Int32.Parse(record[1]),
                      }).ToList();

            //levanta en memoria el stock actual
            var lineasstock = File
                      .ReadAllLines("Stock.txt")
                      .Select(record => record.Split(','))
                      .Select(record => new
                      {
                          b1 = Int32.Parse(record[0]),
                          b2 = Int32.Parse(record[1]),
                          b3 = Int32.Parse(record[2])
                      }).ToList();

            //chequea si hay stock disponible y guarda en memoria como queda el nuevo stock
            foreach (var registroStock in lineasstock)
            {
                IdStock = registroStock.b1;
                KStock = registroStock.b2;

                foreach (var registroPedido in lineaspedido)
                {
                    IdPed = registroPedido.a1;
                    KPed = registroPedido.a2;

                    if (IdStock == IdPed)
                    {
                        if (KStock < KPed)
                        {
                            MessageBox.Show("El producto " + IdStock + " no se encuentra en stock");
                            flag = false;
                        }
                        else
                        {
                            KStock = KStock - KPed;
                            InventarioTemporal.Add(IdStock, KStock);
                        }
                    }
                }
                if (!InventarioTemporal.ContainsKey(IdStock))
                {
                    InventarioTemporal.Add(IdStock, KStock);
                }
            }

            if (flag == false)
            {
                MessageBox.Show("Cargue productos con stock suficiente");
            }
            else
            {
                using (StreamWriter sw2 = new StreamWriter("StockTemporal.txt"))
                {
                    foreach (KeyValuePair<int, int> entry in InventarioTemporal)
                    {
                        sw2.Write(entry.Key);
                        sw2.Write(",");
                        sw2.Write(entry.Value);
                        sw2.Write("\n");
                    }
                }

                var lineastemporal = File
                          .ReadAllLines("StockTemporal.txt")
                          .Select(record => record.Split(','))
                          .Select(record => new
                          {
                              c1 = Int32.Parse(record[0]),
                              c2 = Int32.Parse(record[1]),
                          }).ToList();

                foreach (var recordc in lineastemporal)
                {
                    int idTemp = recordc.c1;
                    int kTemp = recordc.c2;

                    foreach (var recordd in lineasstock)
                    {
                        int variable3 = recordd.b1;
                        int kPtoR = recordd.b3;

                        if (idTemp == variable3)
                        {
                            lista.Add(idTemp.ToString() + "," + kTemp.ToString() + "," + kPtoR.ToString());
                        }
                    }
                }

                using (StreamWriter sw3 = new StreamWriter("StockTemporalPtoRepo.txt"))
                {
                    foreach (string entry in lista)
                    {
                        sw3.Write(entry);
                        sw3.Write("\n");
                    }
                }

                File.Delete("StockTemporal.txt");
                File.Delete("Stock.txt");
                File.Move("StockTemporalPtoRepo.txt", "Stock.txt");

                string n = string.Format("Pedido-{0:yyyy-MM-dd_hh-mm}.txt", DateTime.Now);
                ultimoPedidoGuardado = n;


                using (StreamWriter sw = new StreamWriter(n))
                {

                    sw.Write("R" + codRefPedido + "," + textBoxDirEnt.Text);
                    sw.Write("\n");

                    foreach (ListViewItem item in listPedidos.Items)
                    {
                        sw.Write(item.Text);
                        for (int i = 1; i < item.SubItems.Count; i++)
                            sw.Write("," + item.SubItems[i].Text);
                        sw.Write("\n");
                    }
                }
                File.Delete("PedidoTemporal.txt");
            }

            limpiarlistapedidos();

            if (ultimoPedidoGuardado != "")
            {
                textBoxLote.AppendText("---" + Environment.NewLine);
                foreach (var line in File.ReadLines(ultimoPedidoGuardado))
                {

                    textBoxLote.AppendText(line + Environment.NewLine);
                }
                ultimoPedidoGuardado = "";
            }


            //DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            //if (result == DialogResult.OK) // Test result.
            //    {
            //        string var11;
            //        string var22;

            //        string file = ultimoPedidoGuardado;
            //        var records3 = File
            //              .ReadAllLines(file)
            //              .Select(record => record.Split(','))
            //              .Select(record => new
            //              {
            //                  a11 = record[0],
            //                  a22 = record[1],
            //              }).ToList();

            //        foreach (var recordc in records3)
            //        {
            //            var11 = recordc.a11;
            //            var22 = recordc.a22;
            //            //var33 = recordc.a33;

            //            ListViewItem lstPedido2 = new ListViewItem(var11.ToString());

            //            lstPedido2.SubItems.Add(var22);
            //            //lstPedido2.SubItems.Add(var33.ToString());
            //            listLoteClientes.Items.Add(lstPedido2);
            //        }

            //        listLoteClientes.Items.Add("---");
            //    }
        }




        private void buttonLimpiarListaPedidos_Click(object sender, EventArgs e)
        {
            limpiarlistapedidos();
        }

        private void limpiarlistapedidos()
        {
            listPedidos.Items.Clear();
            textBoxDirEnt.Clear();
            textBoxCdCli.Clear();
            textBoxCdProd.Clear();
            textBoxCant.Clear();
        }


        private void buttonGenerarTXTLote_Click(object sender, EventArgs e)
        {
            codLote = codLote + 1;
            using (StreamWriter sw = new StreamWriter("Lote_C" + textBoxCdCli.Text + "_L" + codLote + ".txt"))
            {
                sw.Write(textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text);
                sw.Write("\n");
                //sw.Write("---");
                //sw.Write("\n");

                foreach (var line in textBoxLote.Text)
                {
                    sw.Write(line);
                }

                //foreach (ListViewItem item in listLoteClientes.Items)
                //{
                //    sw.Write(item.Text);
                //    for (int i = 1; i < item.SubItems.Count; i++)
                //        sw.Write("," + item.SubItems[i].Text);
                //    sw.Write("\n");
                //}

            }
        }







        // ------------------ interactividad textboxes remitente con textbox header del archivo de lotes  + habilitar/desabilitar botones-------------

        private void textBoxRzSoc_TextChanged(object sender, EventArgs e)
        {
            textBoxRemitente.Text = textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text;
            HabilitarBotonGenerarLote();
        }

        private void textBoxCuit2_TextChanged(object sender, EventArgs e)
        {
            textBoxRemitente.Text = textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text;
            HabilitarBotonGenerarLote();
        }

        private void textBoxDirDev_TextChanged(object sender, EventArgs e)
        {
            textBoxRemitente.Text = textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text;
            HabilitarBotonGenerarLote();
        }

        private void HabilitarBotonGenerarLote()
        {
            if (!string.IsNullOrWhiteSpace(textBoxRzSoc.Text) && !string.IsNullOrWhiteSpace(textBoxCuit2.Text) && !string.IsNullOrWhiteSpace(textBoxDirDev.Text))
            {
                buttonGenerarTXTLote.Enabled = true;
            }
            else
            {
                buttonGenerarTXTLote.Enabled = false;
            }
        }

        // ------------------ interactividad textboxes remitente con textbox header del archivo de lotes + habilitar/desabilitar botones -------------





        // ------------------ interactividad textboxes de datos de comercio con textbox header del archivo de pedido a industrias +  habilitar/desabilitar botones -------------

        private void textBoxCodComercio_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
            HabilitarBotonPedidoStock();
        }

        private void textBoxRazSoc_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
            HabilitarBotonPedidoStock();
        }

        private void textBoxCUIT_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
            HabilitarBotonPedidoStock();
        }

        private void textBoxDireccion_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
            HabilitarBotonPedidoStock();
        }

        private void HabilitarBotonPedidoStock()
        {
            if (!string.IsNullOrWhiteSpace(textBoxCodComercio.Text) 
                && !string.IsNullOrWhiteSpace(textBoxRazSoc.Text) 
                && !string.IsNullOrWhiteSpace(textBoxCUIT.Text) 
                && !string.IsNullOrWhiteSpace(textBoxDireccion.Text) 
                && !string.IsNullOrWhiteSpace(textBoxPedidoIndustria.Text))
            {
                buttonPedidoStockIndustrias.Enabled = true;
            }
            else
            {
                buttonPedidoStockIndustrias.Enabled = false;
            }

        }

        // ------------------ interactividad textboxes de datos de comercio con textbox header del archivo de pedido a industrias + habilitar/desabilitar botones  -------------




        // ------------------ Habilitar botón de agregar item solo si los dos textboxes tienen contenido -------------
        private void textBoxCdProd_TextChanged(object sender, EventArgs e)
        {
            HabilitarAgregarItem();
        }
        private void textBoxCant_TextChanged(object sender, EventArgs e)
        {
            HabilitarAgregarItem();
        }

        private void HabilitarAgregarItem()
        {

            if (!string.IsNullOrWhiteSpace(textBoxCant.Text) && !string.IsNullOrWhiteSpace(textBoxCdProd.Text))
            {
                buttonAgregarItem.Enabled = true;
            }
            else
            {
                buttonAgregarItem.Enabled = false;
            }
        }

        // ------------------ Habilitar botón de agregar item solo si los dos textboxes tienen contenido -------------



        // ------------------ Habilitar botón de confirmar pedido solo si los dos textboxes tienen contenido y el listview no esta vacío -------------


        private void textBoxCdCli_TextChanged(object sender, EventArgs e)
        {
            HabilitarConfirmarPedido();
        }

        private void textBoxDirEnt_TextChanged(object sender, EventArgs e)
        {
            HabilitarConfirmarPedido();
        }

        private void HabilitarConfirmarPedido()
        {
            if (!string.IsNullOrWhiteSpace(textBoxDirEnt.Text) && !string.IsNullOrWhiteSpace(textBoxCdCli.Text))
            {
                buttonGenerarPedido.Enabled = true;
            }
            else
            {
                buttonGenerarPedido.Enabled = false;
            }
        }

        // ------------------ Habilitar botón de confirmar pedido solo si los dos textboxes tienen contenido y el listview no esta vacío -------------
    }
}