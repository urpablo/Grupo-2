using System;
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

        

        public Form1()
        {
            InitializeComponent();

            StockLabel.Visible = false;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = false;

            TablaStock.Visible = false;
            TablaReaPed.Visible = false;
            TablaRecPed.Visible = false;
            TablaEnvPed.Visible = false;
            EjLote1.Visible = false;
            ProdAEnviar.Visible = false;
            EjLote2.Visible = false;
            LPed.Visible = false;
            CStock.Visible = false;
            AcPed.Visible = false;
        }


        // hacer que una ventana borderless sea movible

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

        // fin hacer que una ventana borderless sea movible


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



        private void Wrapper_Click(object sender, EventArgs e)
        {
            if (Sidebar.Width == 157)
            {
                Sidebar.Width = 45;
                TopPanelLeft.Width = 45;

                EnvioLabel.Location = new Point(47, 5);
                RecibirLabel.Location = new Point(47, 5);
                RealizarLabel.Location = new Point(47, 5);
                StockLabel.Location = new Point(47, 5);

                TablaStock.Width = 592;
                TablaStock.Location = new Point(78, 58);
                TablaEnvPed.Width = 592;
                TablaEnvPed.Location = new Point(78, 58);
                TablaRecPed.Width = 592;
                TablaRecPed.Location = new Point(78, 58);
                TablaReaPed.Width = 592;
                TablaReaPed.Location = new Point(78, 58);
            }
            else
            {
                Sidebar.Width = 157;
                TopPanelLeft.Width = 157;

                EnvioLabel.Location = new Point(163, 5);
                RecibirLabel.Location = new Point(163, 5);
                RealizarLabel.Location = new Point(163, 5);
                StockLabel.Location = new Point(163, 5);

                TablaStock.Width = 480;
                TablaStock.Location = new Point(190, 58);
                TablaEnvPed.Width = 480;
                TablaEnvPed.Location = new Point(190, 58);
                TablaRecPed.Width = 480;
                TablaRecPed.Location = new Point(190, 58);
                TablaReaPed.Width = 480;
                TablaReaPed.Location = new Point(190, 58);

            }

       
        }

        private void Stock_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = true;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = false;

            TablaStock.Visible = true;
            TablaReaPed.Visible = false;
            TablaRecPed.Visible = false;
            TablaEnvPed.Visible = false;
            EjLote1.Visible = false;
            ProdAEnviar.Visible = false;
            EjLote2.Visible = false;
            LPed.Visible = false;
            CStock.Visible = false;
            AcPed.Visible = false;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;


        }

        private void RealizarPedido_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = true;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = false;

            TablaStock.Visible = false;
            TablaReaPed.Visible = true;
            TablaRecPed.Visible = false;
            TablaEnvPed.Visible = false;
            EjLote1.Visible = true;
            ProdAEnviar.Visible = true;
            EjLote2.Visible = false;
            LPed.Visible = false;
            CStock.Visible = false;
            AcPed.Visible = false;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;
        }

        private void EnviarPedido_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = true;

            TablaStock.Visible = false;
            TablaReaPed.Visible = false;
            TablaRecPed.Visible = false;
            TablaEnvPed.Visible = true;
            EjLote1.Visible = false;
            ProdAEnviar.Visible = false;
            EjLote2.Visible = true;
            LPed.Visible = true;
            CStock.Visible = false;
            AcPed.Visible = false;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;


        }

        private void RecibirPedido_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = true;
            EnvioLabel.Visible = false;

            TablaStock.Visible = false;
            TablaReaPed.Visible = false;
            TablaRecPed.Visible = true;
            TablaEnvPed.Visible = false;
            EjLote1.Visible = false;
            ProdAEnviar.Visible = false;
            EjLote2.Visible = false;
            LPed.Visible = false;
            CStock.Visible = true;
            AcPed.Visible = true;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;

        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        DataTable table2 = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {


            table2.Columns.Add("Codigo", typeof(int));
            table2.Columns.Add("Descripcion", typeof(string));

            TablaStock.DataSource = table2;

            // hacer form borderless movible
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // fin hacer form borderless movible
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = true;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = false;

            TablaStock.Visible = true;
            TablaReaPed.Visible = false;
            TablaRecPed.Visible = false;
            TablaEnvPed.Visible = false;
            EjLote1.Visible = false;
            ProdAEnviar.Visible = false;
            EjLote2.Visible = false;
            LPed.Visible = false;
            CStock.Visible = false;
            AcPed.Visible = false;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = true;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = false;

            TablaStock.Visible = false;
            TablaReaPed.Visible = true;
            TablaRecPed.Visible = false;
            TablaEnvPed.Visible = false;
            EjLote1.Visible = true;
            ProdAEnviar.Visible = true;
            EjLote2.Visible = false;
            LPed.Visible = false;
            CStock.Visible = false;
            AcPed.Visible = false;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = true;

            TablaStock.Visible = false;
            TablaReaPed.Visible = false;
            TablaRecPed.Visible = false;
            TablaEnvPed.Visible = true;
            EjLote1.Visible = false;
            ProdAEnviar.Visible = false;
            EjLote2.Visible = true;
            LPed.Visible = true;
            CStock.Visible = false;
            AcPed.Visible = false;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = true;
            EnvioLabel.Visible = false;

            TablaStock.Visible = false;
            TablaReaPed.Visible = false;
            TablaRecPed.Visible = true;
            TablaEnvPed.Visible = false;
            EjLote1.Visible = false;
            ProdAEnviar.Visible = false;
            EjLote2.Visible = false;
            LPed.Visible = false;
            CStock.Visible = true;
            AcPed.Visible = true;
            labelBienvenido1.Visible = false;
            labelBienvenido2.Visible = false;
        }
    }
}
