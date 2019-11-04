﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            table2.Columns.Add("Código de Producto", typeof(int));
            table2.Columns.Add("Cantidad en Stock", typeof(string));
            table2.Columns.Add("Punto de Reposición", typeof(string));
            table2.Columns.Add("Diferencia", typeof(string));

            TablaStock.DataSource = table2;

            // hacer form borderless movible
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // fin hacer form borderless movible
        }

        public Form1()
        {
            InitializeComponent();
            botonBotonera = 5;
            actualizarPantalla();
        }

        // ------------------ hacer que una ventana borderless sea movible ------------------

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // ------------------ hacer que una ventana borderless sea movible ------------------


        // ------------------ controles ventana ------------------
        private void Salir_Click(object sender, EventArgs e)
        {

            DialogResult resultadoMSGbox = MessageBox.Show("¿Desea realmente salir?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;

            if (resultadoMSGbox == DialogResult.Yes){
                Application.Exit();
            }

            
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }
        // ------------------ controles ventana ------------------

        // ------------------ menú retraible ------------------
        private void Wrapper_Click(object sender, EventArgs e)
        {
            if (Sidebar.Width == 180)
            {
                panelAyuda.Visible = false;


                Sidebar.Width = 45;
                TopPanelLeft.Width = 45;

                LabelTitulo.Location = new Point(55, 5);

                TablaStock.Width = 635;
                TablaStock.Location = new Point(65, 55);
                TablaEnvPed.Width = 635;
                TablaEnvPed.Location = new Point(65, 55);
                TablaRecPed.Width = 635;
                TablaRecPed.Location = new Point(65, 55);
                TablaReaPed.Width = 635;
                TablaReaPed.Location = new Point(65, 55);
            }
            else
            {
                panelAyuda.Visible = true;

                Sidebar.Width = 180;
                TopPanelLeft.Width = 180;

                LabelTitulo.Location = new Point(190, 5);

                TablaStock.Width = 500;
                TablaStock.Location = new Point(200, 55);
                TablaEnvPed.Width = 500;
                TablaEnvPed.Location = new Point(200, 55);
                TablaRecPed.Width = 500;
                TablaRecPed.Location = new Point(200, 55);
                TablaReaPed.Width = 500;
                TablaReaPed.Location = new Point(200, 55);

            }

       
        }

        // ------------------ menú retraible ------------------


        // ------------------ botonera menú ------------------

        public int botonBotonera;
        private void Stock_Click(object sender, EventArgs e)
        {
            botonBotonera = 1;
            actualizarPantalla();
        }

        private void RealizarPedido_Click(object sender, EventArgs e)
        {
            botonBotonera = 2;
            actualizarPantalla();
        }

        private void EnviarPedido_Click(object sender, EventArgs e)
        {
            botonBotonera = 3;
            actualizarPantalla();
        }

        private void RecibirPedido_Click(object sender, EventArgs e)
        {
            botonBotonera = 4;
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


        private void actualizarPantalla()
        {
            switch (botonBotonera)
            {
                case 1:
                    LabelTitulo.Text = "Control de Stock";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver el nivel de stock actual de los productos," +
                        " junto al punto por donde se hará un pedido para reponer y mantener el nivel de stock" +
                        " deseado";

                    TablaStock.Visible = true;
                    TablaReaPed.Visible = false;
                    TablaRecPed.Visible = false;
                    TablaEnvPed.Visible = false;
                    BotonAcuseRecibo.Visible = false;
                    BotonEjecutarLote1.Visible = false;
                    botonProdAEnviar.Visible = false;
                    botonEjecutarLote2.Visible = false;
                    botonGenerarPedido.Visible = false;
                    botonCargarStock.Visible = false;
                    botonActualizarPedidos.Visible = false;

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 2:
                    LabelTitulo.Text = "Confeccionar pedidos a industrias";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos realizar pedidos a las industrias para que nos " +
                        " envíen productos si tenemos stock por debajo del punto de reposición.";

                    TablaStock.Visible = false;
                    TablaReaPed.Visible = true;
                    TablaRecPed.Visible = false;
                    TablaEnvPed.Visible = false;
                    BotonAcuseRecibo.Visible = false;
                    BotonEjecutarLote1.Visible = true;
                    botonProdAEnviar.Visible = true;
                    botonEjecutarLote2.Visible = false;
                    botonGenerarPedido.Visible = false;
                    botonCargarStock.Visible = false;
                    botonActualizarPedidos.Visible = false;

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 3:
                    LabelTitulo.Text = "Confeccionar lotes de bultos a enviar";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos confeccionar los bultos de lotes para envíos, " +
                        " dirigidos a la empresa de logística.";

                    TablaStock.Visible = false;
                    TablaReaPed.Visible = false;
                    TablaRecPed.Visible = false;
                    TablaEnvPed.Visible = true;
                    BotonAcuseRecibo.Visible = true;
                    BotonEjecutarLote1.Visible = false;
                    botonProdAEnviar.Visible = false;
                    botonEjecutarLote2.Visible = true;
                    botonGenerarPedido.Visible = true;
                    botonCargarStock.Visible = false;
                    botonActualizarPedidos.Visible = false;

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 4:
                    LabelTitulo.Text = "Recibir pedidos de ventas Online";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos recibir los pedidos resultantes de ventas Online " +
                        " y revisar contra nuestro stock disponible.";

                    TablaStock.Visible = false;
                    TablaReaPed.Visible = false;
                    TablaRecPed.Visible = true;
                    TablaEnvPed.Visible = false;

                    BotonAcuseRecibo.Visible = false;
                    BotonEjecutarLote1.Visible = false;
                    botonProdAEnviar.Visible = false;
                    botonEjecutarLote2.Visible = false;
                    botonGenerarPedido.Visible = false;
                    botonCargarStock.Visible = true;
                    botonActualizarPedidos.Visible = true;

                    labelBienvenido1.Visible = false;
                    labelBienvenido2.Visible = false;
                    break;

                case 5:
                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "CAI - Comercio";
                    labelAyuda.Text = "CAI, Grupo 2";

                    TablaStock.Visible = false;
                    TablaReaPed.Visible = false;
                    TablaRecPed.Visible = false;
                    TablaEnvPed.Visible = false;

                    BotonAcuseRecibo.Visible = false;
                    BotonEjecutarLote1.Visible = false;
                    botonProdAEnviar.Visible = false;
                    botonEjecutarLote2.Visible = false;
                    botonGenerarPedido.Visible = false;
                    botonCargarStock.Visible = false;
                    botonActualizarPedidos.Visible = false;
                    break;
            }
        }
        // ------------------ botonera menú ------------------
    }
}
