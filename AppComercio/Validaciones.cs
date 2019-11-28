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
        bool CARGAMESTOCK = false;

        // ------------------- si este archivo tiene un largo de 0 (es decir, vacío) no se habiilta el botón de pedidos pendientes

        private void habilitarBotonPedidosPendientes()
        {
            if (new FileInfo("AReponer.txt").Length == 0)
            {
                buttonPedidosPendientes.Enabled = false;
            }
            else
            {
                buttonPedidosPendientes.Enabled = true;
            }

        }

        // -------------------- revisar el dgw de stock y si el valor real esta por debajo del punto de reposición, marcarlo -----------------
        private void habilitarBotonPedidosIndustrias()
        {
            foreach (DataGridViewRow dr in dgwStock.Rows)
            {
                if (int.Parse(dr.Cells[tablaStock.Columns.IndexOf("Real")].Value.ToString()) < int.Parse(dr.Cells[tablaStock.Columns.IndexOf("Punto de Reposición")].Value.ToString()))
                {
                    System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    boldStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                    dr.Cells[tablaStock.Columns.IndexOf("Real")].Style = boldStyle;
                    CARGAMESTOCK = true;
                }
                else
                {
                    System.Windows.Forms.DataGridViewCellStyle norStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    norStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
                    dr.Cells[tablaStock.Columns.IndexOf("Real")].Style = norStyle;
                }

                if (CARGAMESTOCK == true)
                {
                    buttonPedidoStockIndustrias.Enabled = true;
                }
                else
                {
                    buttonPedidoStockIndustrias.Enabled = false;
                }
            }
        }

        // ------------------ Habilitar botón de agregar item en ventas solo si los dos textboxes tienen contenido -------------

        private void textBoxCant_TextChanged(object sender, EventArgs e)
        {
            HabilitarAgregarItem();
        }

        private void HabilitarAgregarItem()
        {

            if (!string.IsNullOrWhiteSpace(textBoxCant.Text)
                && !string.IsNullOrWhiteSpace(comboBoxCodProducto.Text))
            {
                buttonAgregarItem.Enabled = true;
            }
            else
            {
                buttonAgregarItem.Enabled = false;
            }
        }

        // ------------------ Habilitar botón de confirmar pedido en ventas solo si los dos textboxes tienen contenido y el listview no esta vacío -------------

        private void textBoxCdCli_TextChanged(object sender, EventArgs e)
        {
            HabilitarConfirmarPedido();
        }

        private void textBoxDirEnt_TextChanged(object sender, EventArgs e)
        {
            HabilitarConfirmarPedido();
        }

        private void HabilitarConfirmarPedido()
        {
            if (!string.IsNullOrWhiteSpace(textBoxDirEnt.Text)
                && !string.IsNullOrWhiteSpace(textBoxCdCli.Text)
                && listPedidos.Items.Count != 0)
            {
                buttonGenerarPedido.Enabled = true;
            }
            else
            {
                buttonGenerarPedido.Enabled = false;
            }
        }

        //------------------------------- Habilitar botón de cargar pedidos no entregados en reportes de entrega solo si se ha cargado un reporte ------------------

        private void textBoxCodClienteReporte_TextChanged(object sender, EventArgs e)
        {
            habilitarCargarNoEntregados();
        }

        private void textBoxCodLoteReporte_TextChanged(object sender, EventArgs e)
        {
            habilitarCargarNoEntregados();
        }

        private void habilitarCargarNoEntregados()
        {

            if (!string.IsNullOrWhiteSpace(textBoxCodClienteReporte.Text)
               && !string.IsNullOrWhiteSpace(textBoxCodLoteReporte.Text))
            {
                btnCargarStockNoEntregados.Enabled = true;
            }
            else
            {
                btnCargarStockNoEntregados.Enabled = false;
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

        private void textBoxCant_KeyPress(object sender, KeyPressEventArgs e)
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


    }

}

        