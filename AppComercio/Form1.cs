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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

      

   

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

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
            }
            else
            {
                Sidebar.Width = 157;
                TopPanelLeft.Width = 157;

            }
        }

      

        private void StockLabel_Click(object sender, EventArgs e)
        {

        }

        private void RecibirPedido_Click(object sender, EventArgs e)
        {

            StockLabel.Visible = false;
            RealizarPedidoLabel.Visible = false;
            EnviarPedidoLabel.Visible = false;
            RecibirPedidosLabel.Visible = true;
        }

        private void RealizarPedido_Click(object sender, EventArgs e)
        {

            StockLabel.Visible = false;
            RealizarPedidoLabel.Visible = true;
            EnviarPedidoLabel.Visible = false;
            RecibirPedidosLabel.Visible = false;

        }

        private void EnviarPedido_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = false;
            RealizarPedidoLabel.Visible = false;
            EnviarPedidoLabel.Visible = true;
            RecibirPedidosLabel.Visible = false;

        }

        private void Stock_Click(object sender, EventArgs e)
        {
            StockLabel.Visible = true;
            RealizarPedidoLabel.Visible = false;
            EnviarPedidoLabel.Visible = false;
            RecibirPedidosLabel.Visible = false;
        }
    }
}
