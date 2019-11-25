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


            Directory.CreateDirectory(@"C:\Grupo2");




            tablaStock.Columns.Add("ID",typeof(int));
            tablaStock.Columns.Add("Real", typeof(int));
            tablaStock.Columns.Add("Punto de Reposición", typeof(int));
            tablaStock.Columns.Add("Comprometido", typeof(int));
            tablaStock.Columns.Add("Pendientes", typeof(int));

            tablaEntregados.Columns.Add("Código de referencia", typeof(string));
            tablaEntregados.Columns.Add("Entregado", typeof(bool));

            tablaNoEntregados.Columns.Add("Código de referencia", typeof(string));
            tablaNoEntregados.Columns.Add("Entregado", typeof(bool));

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
            btnCargarStockNoEntregados.Enabled = false;


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

                    LabelTitulo.Text = "Confeccionar pedido a industria";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos confirmar pedidos a las industrias para que nos" +
                        " envíen productos si tenemos stock por debajo del punto de reposición. \n \n" +
                        "Recuerde que sólo se puede hacer UN solo pedido por día. \n \n" +
                        "Recuerde llenar los datos del comercio que hace el pedido.";

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


        // ------------------------------------ Validaciones: evitar entradas espurias en los textbox--------------------------------------------
        private void textBoxDirEnt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void textBoxDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxCant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void textBoxCUIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxCuit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxCdCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxCodComercio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void textBoxRazSoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxRzSoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void textBoxDirDev_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        //private void dgwStock_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (dgwStock.NewRowIndex == e.RowIndex)
        //        {
        //            dgwStock.Rows[e.RowIndex].ReadOnly = false;
        //        }
        //        else if (dgwStock.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
        //        {
        //            dgwStock.Rows[e.RowIndex].ReadOnly = false;
        //        }
        //        else
        //        {
        //            dgwStock.Rows[e.RowIndex].ReadOnly = true;
        //        }
        //    }

        //    catch
        //    {


        //    }


        //}
    }
}
