﻿using System;
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

            // borrar todo rastro de archivos de ejecuciones pasadas
            if (File.Exists("PedidoTemporal.txt"))
            {
                File.Delete("PedidoTemporal.txt");
            }

            if (File.Exists("PedidosAEnviar.txt"))
            {
                File.Delete("PedidosAEnviar.txt");
            }

            if (File.Exists("Pedidos.txt"))
            {
                File.Delete("Pedidos.txt");
            }

            if (File.Exists("Listadereferencias.txt"))
            {
                File.Delete("Listadereferencias.txt");
            }

            if (File.Exists("lineaindividual.txt"))
            {
                File.Delete("lineaindividual.txt");
            }

            if (File.Exists("AReponer.txt"))
            {
                File.Delete("AReponer.txt");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("AReponer.txt")) ;
            }

            using (System.IO.StreamWriter filearepo = new System.IO.StreamWriter("PedidosAEnviar.txt")) ;

                // borrar la salida de la ejecución anterior y recrear el directorio de salida
                if (Directory.Exists(@"C:\Grupo2"))
            {
                Directory.Delete(@"C:\Grupo2", true);
                Directory.CreateDirectory(@"C:\Grupo2");
            }
            else
            {
                Directory.CreateDirectory(@"C:\Grupo2");
            }
            


            //seteo de columnas para las tablas de datos
            tablaStock.Columns.Add("ID",typeof(int));
            tablaStock.Columns.Add("Real", typeof(int));
            tablaStock.Columns.Add("Punto de Reposición", typeof(int));
            tablaStock.Columns.Add("Comprometido", typeof(int));
            tablaStock.Columns.Add("Pendientes", typeof(int));

            tablaEntregas.Columns.Add("ID", typeof(int));
            tablaEntregas.Columns.Add("Cantidad a Reponer", typeof(int));
            tablaEntregas.Columns.Add("Recepción", typeof(bool));

            tablaEntregados.Columns.Add("Código de referencia", typeof(string));
            tablaEntregados.Columns.Add("Entregado", typeof(bool));

            tablaNoEntregados.Columns.Add("Código de referencia", typeof(string));
            tablaNoEntregados.Columns.Add("Entregado", typeof(bool));

            dataGridView1.AllowUserToAddRows = false;



            //últimos ajustes iniciales

            cargarDatosComercio();
            refrescarstock();
            refrescarEntregas();
            if (!File.Exists("AReponer.txt"))
            {
                button1.Enabled = false;
            }

            listLoteClientes.Visible = false;

            panelBienvenido.Location = new Point(186,38);
            panelStock.Location = new Point(186, 38);
            panelPedidoIndustrias.Location = new Point(186, 38);
            panelEnviosClientesOnline.Location = new Point(186, 38);
            panelVentasOnline.Location = new Point(186, 38);
            panelAcuseRecibo.Location = new Point(186, 38);

            buttonAgregarItem.Enabled = false;
            buttonGenerarPedido.Enabled = false;
            //buttonGenerarTXTLote.Enabled = false;
            //buttonPedidoStockIndustrias.Enabled = false;
            btnCargarStockNoEntregados.Enabled = false;


            // hacer ventana borderless movible
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanelLeft_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(LabelTitulo_MouseMove);
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





        // ------------------ controles ventana ------------------
        private void pbSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultadoMSGbox = MessageBox.Show("¿Desea realmente salir?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;

            if (resultadoMSGbox == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

        // ------------------ botonera menú ------------------

        public int botonBotonera;
        private void btnStock_Click(object sender, EventArgs e)
        {
            botonBotonera = 1;
            actualizarPantalla();

            dataGridView1.Refresh();

            string[] lines2 = File.ReadAllLines(@"AReponer.txt");
            string[] values2;

            for (int i = 0; i < lines2.Length; i++)
            {
                values2 = lines2[i].ToString().Split(';');
                string[] row = new string[values2.Length];

                for (int j = 0; j < values2.Length; j++)
                {
                    row[j] = values2[j].Trim();
                }
                tablaEntregas.Rows.Add(row);

            }

            if (!File.Exists("AReponer.txt"))
            {
                button1.Enabled = false;
            }


            tablaStock.Rows.Clear();
            dgwStock.Refresh();

            string[] lines = File.ReadAllLines(@"Stock.txt");
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(';');
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
            if (File.Exists("Pedidos.txt"))
            {
                buttonGenerarTXTLote.Enabled = true;
            }
            else
            {
                buttonGenerarTXTLote.Enabled = false;
            }
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
                    labelAyuda.Text = "Aquí podemos ver la situación de stock actual.";


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

                    LabelTitulo.Text = "Pedido diario de stock a industria";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos confirmar pedidos a las industrias para que nos" +
                        " envíen productos si tenemos stock por debajo del punto de reposición al cargar ventas. \n \n" +
                        "Recuerde que sólo se puede hacer UN solo pedido por día.";

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

                    LabelTitulo.Text = "Confirmar lote diario de ventas a enviar";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver el lote generado por las ventas a clientes en el día, " +
                        " para ser distribuídos por la empresa de logística. \n \n" +
                        "Recuerde que solo se puede enviar UN solo lote por día. \n \n" +
                        "Cargue todas sus ventas antes de confirmarlo.";

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
                        "3) Ingrese de la misma forma hasta completar el pedido del cliente para la venta hecha. \n" +
                        "4) Clic en 'Confirmar Pedido' para ingresarlo al lote diario.";

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
                    labelAyuda.Text = "Pasos para leer y procesar el reporte de entregas: \n \n" +
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

        private void button1_Click(object sender, EventArgs e)
        {
            {
                Dictionary<int, int> PedidosAreponer = new Dictionary<int, int>();
                Dictionary<int, string> InventarioTemporal3 = new Dictionary<int, string>();
                Dictionary<int, int> NuevosPedidosAreponer = new Dictionary<int, int>();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool isCellChecked = (bool)dataGridView1.Rows[i].Cells[2].Value;
                    if (isCellChecked == true)
                    {
                        PedidosAreponer.Add(Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()), Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                    }

                }

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

                foreach (var regStock in lineasstock)
                {
                    int actual = regStock.b2;
                    int pp = regStock.b5;
                    int comp = regStock.b4;
                    int pr = regStock.b3;
                    int IdStock = regStock.b1;
                    string parametrosinv;


                    foreach (KeyValuePair<int, int> item in PedidosAreponer)
                    {


                        int idDic = item.Key;
                        int cantDic = item.Value;
                        if (IdStock == idDic)
                        {
                            parametrosinv = (actual + cantDic) + ";" + pr + ";" + comp + ";" + (pp - cantDic);

                            InventarioTemporal3.Add(IdStock, parametrosinv);

                        }



                    }
                    if (!InventarioTemporal3.ContainsKey(IdStock))
                    {
                        parametrosinv = actual + ";" + pr + ";" + comp + ";" + pp;
                        InventarioTemporal3.Add(IdStock, parametrosinv);
                    }



                }

                using (StreamWriter sw100 = new StreamWriter("stockrepuesto.txt"))
                {
                    foreach (KeyValuePair<int, string> entry in InventarioTemporal3)
                    {
                        sw100.Write(entry.Key);
                        sw100.Write(";");
                        sw100.Write(entry.Value);
                        sw100.Write("\n");
                    }
                }

                File.Delete("Stock.txt");
                File.Move("stockrepuesto.txt", "Stock.txt");

                refrescarstock();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool isCellChecked = (bool)dataGridView1.Rows[i].Cells[2].Value;
                    if (isCellChecked == true)
                    {

                    }
                    else
                    {
                        NuevosPedidosAreponer.Add(Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()), Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                    }

                }

                using (StreamWriter sw101 = new StreamWriter("nuevostockrepuesto.txt"))
                {
                    foreach (KeyValuePair<int, int> entry in NuevosPedidosAreponer)
                    {
                        sw101.Write(entry.Key);
                        sw101.Write(";");
                        sw101.Write(entry.Value);
                        sw101.Write("\n");
                    }
                }

                File.Delete("AReponer.txt");
                File.Move("nuevostockrepuesto.txt", "AReponer.txt");

                refrescarEntregas();

                if (!File.Exists("AReponer.txt"))
                {
                    button1.Enabled = false;
                }


            }
        }
    }
}
