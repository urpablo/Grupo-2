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
        public Form1()
        {
            InitializeComponent();
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
        }

        private void RealizarPedido_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = true;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = false;
        }

        private void EnviarPedido_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = false;
            EnvioLabel.Visible = true;

        }

        private void RecibirPedido_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarLabel.Visible = false;
            RecibirLabel.Visible = true;
            EnvioLabel.Visible = false;

        }

      
    }
}
