using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }


        private void Restaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Restaurar.Visible = false;
            Maximizar.Visible = true;
        }

        private void Maximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Restaurar.Visible = true;
            Maximizar.Visible = false;

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
            }
            else
            {
                Sidebar.Width = 157;
                TopPanelLeft.Width = 157;
                EnvioLabel.Location = new Point(163, 5);
                RecibirLabel.Location = new Point(163, 5);
                RealizarLabel.Location = new Point(163, 5);
                StockLabel.Location = new Point(163, 5);


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


        }
    }
}
