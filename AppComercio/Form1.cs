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
        // ------------------ carga del formulario -------------------------------------------------
        DataTable table = new DataTable();
        string ultimoPedidoGuardado;
        int codPedidoInd;
        int codRefPedido;
        int codLote;
        private void Form1_Load(object sender, EventArgs e)
        {
            table.Columns.Add("ID",typeof(int));
            table.Columns.Add("Real", typeof(int));
            table.Columns.Add("Comprometido", typeof(int));
            table.Columns.Add("Punto de Reposición", typeof(int));
            table.Columns.Add("Diferencia", typeof(int));


            dataGridView1.DataSource = table;

            refrescarstock();
            listLoteClientes.Visible = false;

            panelBienvenido.Location = new Point(186,38);
            PanelStock.Location = new Point(186, 38);
            panelPedidoIndustrias.Location = new Point(186, 38);
            panelEnviosClientesOnline.Location = new Point(186, 38);
            panelVentasOnline.Location = new Point(186, 38);
            panelAcuseRecibo.Location = new Point(186, 38);

            // hacer form borderless movible
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanelLeft_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(LabelTitulo_MouseMove);
            // hacer form borderless movible
        }
        public Form1()
        {
            InitializeComponent();
            botonBotonera = 0;
            actualizarPantalla();
        }
       
        // ------------------ hacer que una ventana borderless sea movible ------------------

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void TopPanelLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void LabelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ------------------ hacer que una ventana borderless sea movible ------------------
        // ------------------ carga del formulario -------------------------------------------------



        // ------------------ controles ventana ------------------
        private void Salir_Click(object sender, EventArgs e)
        {

            DialogResult resultadoMSGbox = MessageBox.Show("¿Desea realmente salir?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;

            if (resultadoMSGbox == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }
        // ------------------ controles ventana -----------------


        // ------------------ botonera menú ------------------

        public int botonBotonera;
        private void btnStock_Click(object sender, EventArgs e)
        {
            botonBotonera = 1;
            actualizarPantalla();
        }

        private void btnPedidoIndustrias_Click(object sender, EventArgs e)
        {
            botonBotonera = 2;
            actualizarPantalla();
        }

        private void btnEnviarPedido_Click(object sender, EventArgs e)
        {
            botonBotonera = 3;
            actualizarPantalla();
        }

        private void btnRecibirPedidoOnline_Click(object sender, EventArgs e)
        {
            botonBotonera = 4;
            actualizarPantalla();
        }

        private void btnAcuseRecibo_Click(object sender, EventArgs e)
        {
            botonBotonera = 5;
            actualizarPantalla();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            botonBotonera = 1;
            actualizarPantalla();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            botonBotonera = 2;
            actualizarPantalla();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            botonBotonera = 3;
            actualizarPantalla();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            botonBotonera = 4;
            actualizarPantalla();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            botonBotonera = 5;
            actualizarPantalla();
        }

        private void actualizarPantalla()
        {
            switch (botonBotonera)
            {
                case 1:
                    panelBienvenido.Visible = false;
                    PanelStock.Visible = true;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;

                    btnStock.BackColor = Color.FromArgb(52, 78, 103);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 31);

                    LabelTitulo.Text = "Control de Stock";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver el nivel de stock actual de los productos. \n \n" +
                        "Si desea agregar un nuevo producto al listado, ingréselo en la fila disponible al final de la lista.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 2:
                    panelBienvenido.Visible = false;
                    PanelStock.Visible = false;
                    panelPedidoIndustrias.Visible = true;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;

                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(52, 78, 103);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 31);

                    LabelTitulo.Text = "Confeccionar pedido a industria";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos confirmar pedidos a las industrias para que nos " +
                        " envíen productos si tenemos stock por debajo del punto de reposición. \n \n" +
                        "Recuerde que sólo se puede hacer UN solo pedido por día.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 3:
                    panelBienvenido.Visible = false;
                    PanelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = true;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;

                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(52, 78, 103);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 31);

                    LabelTitulo.Text = "Confeccionar el lote de bultos a enviar";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver los lotes de bultos para envíos a clientes, " +
                        " para ser distribuídos por la empresa de logística. \n \n" +
                        "Recuerde que solo se puede enviar UN solo lote por día. \n \n" +
                        "Recuerde llenar los datos del remitente para despachar el lote.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 4:
                    panelBienvenido.Visible = false;
                    PanelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = true;
                    panelAcuseRecibo.Visible = false;

                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(52, 78, 103);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 31);

                    LabelTitulo.Text = "Ingresar pedidos de ventas Online";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Pasos para ingresar un pedido: \n \n" +
                        "1) Ingrese los datos del cliente. \n" +
                        "2) Ingrese código de producto y cantidad, y agregue este producto al pedido. \n" +
                        "3) Ingrese de la misma forma hasta completar el pedido del cliente. \n" +
                        "4) Clic en 'Confirmar Pedido' para ingresarlo al sistema.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;

                    listLoteClientes.Visible = false;

                    break;

                case 5:
                    panelBienvenido.Visible = false;
                    PanelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = true;

                    btnStock.BackColor =  Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(52, 78, 103);

                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "Reporte de Entregas";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Pasos para leer y processar el reporte de entregas: \n \n" +
                        "1) Haga clic en 'Leer Reporte...' y elija el archivo \n" +
                        "2) Se visualizará el reporte. Luego haga clic en 'Cargar Stock...' \n" +
                        "3) Los pedidos que no hayan sido entregados habrán sido reingresados al stock";

                    break;

                case 0:
                    panelBienvenido.Visible = true;
                    PanelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;

                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 31);

                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "CAI - Comercio";
                    labelAyuda.Text = "CAI - Grupo 2";

                    break;
            }
        }

        // ------------------ botonera menú ------------------


        // ------------------ LOGICA PANTALLA STOCK Y CONFIRMAR PEDIDO INDUSTRIAS -------------------------

        private void buttonLimpiarPedStockInd_Click(object sender, EventArgs e)
        {
            textBoxCodComercio.Clear();
            textBoxRazSoc.Clear();
            textBoxCUIT.Clear();
            textBoxDireccion.Clear();
        }

        private void buttonPedidoStockIndustrias_Click(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;

        }




        //--------------------- leer stock.txt -----------------------------------

        private void refrescarstock()
        {
            table.Rows.Clear();
            dataGridView1.Refresh();

            string[] lines = File.ReadAllLines(@"Stock.txt");
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(',');
                string[] row = new string[values.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    row[j] = values[j].Trim();
                }
                table.Rows.Add(row);

            }
        }

        //--------------------- leer stock.txt -----------------------------------

        // ------------------ LOGICA PANTALLA STOCK Y CONFIRMAR PEDIDO INDUSTRIAS -------------------------




        // ------------------ LOGICA PANTALLAS VENTAS ONLINE Y GENERAR LOTES/ENVIOS ---------------

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





        // ------------------ interactividad textboxes remitente con textbox header del archivo de lotes -------------

        private void textBoxRzSoc_TextChanged(object sender, EventArgs e)
        {
            textBoxRemitente.Text = textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text;
        }

        private void textBoxCuit2_TextChanged(object sender, EventArgs e)
        {
            textBoxRemitente.Text = textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text;
        }

        private void textBoxDirDev_TextChanged(object sender, EventArgs e)
        {
            textBoxRemitente.Text = textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text;
        }


        // ------------------ interactividad textboxes remitente con textbox header del archivo de lotes -------------


        // ------------------ interactividad textboxes de datos de comercio con textbox header del archivo de pedido a industrias -------------

        private void textBoxCodComercio_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
        }

        private void textBoxRazSoc_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
        }

        private void textBoxCUIT_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
        }

        private void textBoxDireccion_TextChanged(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;
        }


        // ------------------ interactividad textboxes de datos de comercio con textbox header del archivo de pedido a industrias -------------




        // ------------------ LOGICA PANTALLAS VENTAS ONLINE Y GENERAR LOTES/ENVIOS ---------------


    }
}
