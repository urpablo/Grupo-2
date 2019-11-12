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
        // ------------------ carga del formulario ------------------
        DataTable table = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Id",typeof(int));
            table.Columns.Add("Cantidad", typeof(int));
            table.Columns.Add("Punto de Reposición", typeof(int));

            dataGridView1.DataSource = table;


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
        // ------------------ carga del formulario ------------------



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
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 71);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 71);

                    LabelTitulo.Text = "Control de Stock";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver el nivel de stock actual de los productos," +
                        " y hacer el pedido de los que están debajo del punto de reposición";

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

                    btnStock.BackColor = Color.FromArgb(41, 57, 71);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(52, 78, 103);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 71);

                    LabelTitulo.Text = "Confeccionar pedido a industria";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos realizar pedidos a las industrias para que nos " +
                        " envíen productos si tenemos stock por debajo del punto de reposición.";

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

                    btnStock.BackColor = Color.FromArgb(41, 57, 71);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 71);
                    btnEnviarPedido.BackColor = Color.FromArgb(52, 78, 103);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 71);

                    LabelTitulo.Text = "Confeccionar el lote de bultos a enviar";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver los lotes de bultos para envíos a clientes, " +
                        " para ser distribuídos por la empresa de logística.";

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

                    btnStock.BackColor = Color.FromArgb(41, 57, 71);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 71);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(52, 78, 103);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 71);

                    LabelTitulo.Text = "Recibir pedidos de ventas Online";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos recibir los pedidos resultantes de ventas Online.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 5:
                    panelBienvenido.Visible = false;
                    PanelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = true;

                    btnStock.BackColor =  Color.FromArgb(41, 57, 71);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 71);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(52, 78, 103);

                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "Ver acuse de recibo de Envíos";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver si los envíos fueron hechos correctamente.";

                    break;

                case 0:
                    panelBienvenido.Visible = true;
                    PanelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;

                    btnStock.BackColor = Color.FromArgb(41, 57, 71);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 71);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 57, 71);

                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "CAI - Comercio";
                    labelAyuda.Text = "CAI - Grupo 2";

                    break;
            }
        }

        private void groupBoxDatosClienteOnline_Enter(object sender, EventArgs e)
        {

        }

        

        public void buttonLimpiarVentaOnline_Click(object sender, EventArgs e)
        {
            ListViewItem lstPedido = new ListViewItem(textBoxCdProd.Text);
            lstPedido.SubItems.Add(textBoxCant.Text);
            listPedidos.Items.Add(lstPedido);

        }

        private void buttonRecibirPedidoOnline_Click(object sender, EventArgs e)
        {
            bool flag = true;
            int IdStock = 0;
            int KStock = 0;
            int IdPed = 0;
            int KPed = 0;

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

                using (StreamWriter sw = new StreamWriter(n))
                {

                    sw.Write(textBoxCdRef.Text + "," + textBoxDirEnt.Text);
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listPedidos.Items.Clear();
        }

        private void buttonEnviarClientesOnline_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string var11;
                string var22;

                string file = openFileDialog1.FileName;
                var records3 = File
                      .ReadAllLines(file)
                      .Select(record => record.Split(','))
                      .Select(record => new
                      {
                          a11 = record[0],
                          a22 = record[1],
                      }).ToList();

                foreach (var recordc in records3)
                {
                    var11 = recordc.a11;
                    var22 = recordc.a22;
                    //var33 = recordc.a33;

                    ListViewItem lstPedido2 = new ListViewItem(var11.ToString());

                    lstPedido2.SubItems.Add(var22);
                    //lstPedido2.SubItems.Add(var33.ToString());
                    listLoteClientes.Items.Add(lstPedido2);
                }

                listLoteClientes.Items.Add("---");
            }
        }

        private void buttonLimpiarClientesOnline_Click(object sender, EventArgs e)
        {
            listLoteClientes.Items.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            using (StreamWriter sw = new StreamWriter("Lote_"+textBoxCdCli.Text+"_"+textBoxCdLote.Text+".txt"))
            {
                sw.Write(textBoxRzSoc.Text + "," + textBoxCuit2.Text + "," + textBoxDirDev.Text);
                sw.Write("\n");
                sw.Write("---");
                sw.Write("\n");

                foreach (ListViewItem item in listLoteClientes.Items)
                {
                    sw.Write(item.Text);
                    for (int i = 1; i < item.SubItems.Count; i++)
                        sw.Write("," + item.SubItems[i].Text);
                    sw.Write("\n");
                }

            }

        }

        private void PanelStock_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();
            dataGridView1.Refresh();

            string[] lines = File.ReadAllLines(@"Stock.txt");
            string[] values;

            for (int i=0;i<lines.Length;i++)
            {
                values = lines[i].ToString().Split(',');
                string[] row = new string[values.Length];

                for (int j=0;j<values.Length;j++)
                {
                    row[j] = values[j].Trim();
                }
                table.Rows.Add(row);

            }
        }

        private void buttonLimpiarPedStockInd_Click(object sender, EventArgs e)
        {

        }




















        // ------------------ botonera menú ------------------
    }
}
