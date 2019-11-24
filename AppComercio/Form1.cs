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

    
        // ------------------ carga del formulario ----------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {


            dgwStock.DataSource = tablaStock;
            dgwReporteEntrega.DataSource = tablaReporte;

            tablaStock.Columns.Add("ID",typeof(int));
            tablaStock.Columns.Add("Real", typeof(int));
            tablaStock.Columns.Add("Punto de Reposición", typeof(int));
            tablaStock.Columns.Add("Comprometido", typeof(int));
            tablaStock.Columns.Add("Pendientes", typeof(int));

            tablaReporte.Columns.Add("Código de referencia", typeof(string));
            tablaReporte.Columns.Add("Entregado", typeof(bool));

            refrescarstock();


            listLoteClientes.Visible = false;

            panelBienvenido.Location = new Point(186,38);
            panelStock.Location = new Point(186, 38);
            panelPedidoIndustrias.Location = new Point(186, 38);
            panelEnviosClientesOnline.Location = new Point(186, 38);
            panelVentasOnline.Location = new Point(186, 38);
            panelAcuseRecibo.Location = new Point(186, 38);

            buttonAgregarItem.Enabled = false;
            buttonGenerarPedido.Enabled = false;
            buttonGenerarTXTLote.Enabled = false;
            buttonPedidoStockIndustrias.Enabled = false;


            // hacer ventana borderless movible
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanelLeft_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(LabelTitulo_MouseMove);
            // hacer ventana borderless movible
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
        // ------------------ carga del formulario ----------------------------------------------------------------------------




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

          
            tablaStock.Rows.Clear();
            dgwStock.Refresh();

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
                tablaStock.Rows.Add(row);

            }
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
                    btnStock.BackColor = Color.FromArgb(52, 78, 103);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Text = "Control de Stock";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver la situación de stock actual. \n \n" +
                        "Si desea agregar un nuevo producto al listado, ingréselo en la fila disponible al final de la lista.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;

                    panelBienvenido.Visible = false;
                    panelStock.Visible = true;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;
                    break;

                case 2:
                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(52, 78, 103);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Text = "Confeccionar pedido a industria";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos confirmar pedidos a las industrias para que nos" +
                        " envíen productos si tenemos stock por debajo del punto de reposición. \n \n" +
                        "Recuerde que sólo se puede hacer UN solo pedido por día. \n \n" +
                        "Recuerde llenar los datos del comercio que hace el pedido.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = true;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;
                    break;

                case 3:
                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(52, 78, 103);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Text = "Confeccionar el lote de bultos a enviar";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver los lotes de bultos para envíos a clientes, " +
                        " para ser distribuídos por la empresa de logística. \n \n" +
                        "Recuerde que solo se puede enviar UN solo lote por día. \n \n" +
                        "Recuerde llenar los datos del remitente para despachar el lote.";

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = true;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;
                    break;

                case 4:
                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(52, 78, 103);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 47, 71);

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

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = true;
                    panelAcuseRecibo.Visible = false;
                    break;

                case 5:
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

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = true;
                    break;

                case 0:
                    btnStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarPedido.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirPedidoOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnAcuseRecibo.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "CAI - Comercio";
                    labelAyuda.Text = "CAI - Grupo 2";

                    panelBienvenido.Visible = true;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelAcuseRecibo.Visible = false;
                    break;
            }
        }

        private void panelBienvenido_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void buttonPedidoStockIndustrias_Click(object sender, EventArgs e)
        {
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDireccion.Text;


            //levanta en memoria el stock actual
            var lineasstock = File
                      .ReadAllLines("Stock.txt")
                      .Select(record => record.Split(','))
                      .Select(record => new
                      {
                          b1 = Int32.Parse(record[0]),
                          b2 = Int32.Parse(record[1]),
                          b3 = Int32.Parse(record[2]),
                          b4 = Int32.Parse(record[3]),
                          b5 = Int32.Parse(record[4])

                      }).ToList();

            foreach (var regStock in lineasstock)
            {
                int actual = regStock.b2;
                int pp = regStock.b5;
                int comp = regStock.b4;
                int pr = regStock.b3;
                int IdStock = regStock.b1;
                string parametrosinv;

                Dictionary<int, string> InventarioTemporal = new Dictionary<int, string>();

                if (((actual + pp) - comp) < pr)
                {

                    InventarioTemporal.Add(IdStock, comp.ToString());

                    using (StreamWriter sw12 = new StreamWriter("AReponer.txt"))
                    {
                        foreach (KeyValuePair<int, string> entry in InventarioTemporal)
                        {
                            sw12.Write(entry.Key);
                            sw12.Write(",");
                            sw12.Write(entry.Value);
                            sw12.Write("\n");
                        }
                    }
                }
            }

            var lineasrepone = File
                      .ReadAllLines("AReponer.txt")
                      .Select(record => record.Split(','))
                      .Select(record => new
                      {
                          c1 = record[0],
                          c2 = Int32.Parse(record[1]),
                      }).ToList();

            Dictionary<int, string> InventarioTemporal2 = new Dictionary<int, string>();

            foreach (var regStock in lineasstock)
            {
                int actual = regStock.b2;
                int pp = regStock.b5;
                int comp = regStock.b4;
                int pr = regStock.b3;
                int IdStock = regStock.b1;
                string parametrosinv;



                foreach (var regPed in lineasrepone)
                {
                    string cdRepo = regPed.c1;
                    int cantRepo = regPed.c2;

                    if (IdStock.ToString() == cdRepo)
                    {
                        parametrosinv = actual + "," + pr + "," + comp + "," + cantRepo;

                        InventarioTemporal2.Add(IdStock, parametrosinv);


                    }


                }

                if (!InventarioTemporal2.ContainsKey(IdStock))
                {
                    parametrosinv = actual + "," + pr + "," + comp + "," + pp;
                    InventarioTemporal2.Add(IdStock, parametrosinv);
                }
            }

            using (StreamWriter sw13 = new StreamWriter("stockconpp.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporal2)
                {
                    sw13.Write(entry.Key);
                    sw13.Write(",");
                    sw13.Write(entry.Value);
                    sw13.Write("\n");
                }
            }

            File.Delete("Stock.txt");
            File.Move("stockconpp.txt", "Stock.txt");

            DateTime date = DateTime.Now;
            long n = long.Parse(date.ToString("yyyyMMddHHmmss"));

            using (StreamWriter sw14 = new StreamWriter("Lote_" + n + ".txt"))
            {
                sw14.Write(textBoxCodComercio.Text + "," + textBoxRazSoc.Text + "," + textBoxCUIT.Text + "," + textBoxDireccion.Text);
                sw14.Write("\n");
                sw14.Write("---");
                sw14.Write("\n");

            }

            using (StreamWriter sw15 = File.AppendText("Lote_" + n + ".txt"))
            {
                string[] readText = File.ReadAllLines("AReponer.txt");
                foreach (string s in readText)
                {
                    sw15.Write(s);
                    sw15.Write("\n");
                }


            }
        }






        // ------------------ botonera menú ------------------






    }
}
