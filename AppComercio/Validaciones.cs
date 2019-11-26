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


        // ------------------  habilitar/desabilitar boton generar lote en envios de ventas si la vista previa está vacía------------
        private void HabilitarBotonGenerarLote()
        {
            if (textBoxLote.Text == "")
            {
                buttonGenerarTXTLote.Enabled = true;
            }
            else
            {
                buttonGenerarTXTLote.Enabled = false;
            }
        }

        // ------------------ habilitar/desabilitar boton pedido stock en pedidos a industrias si la vista previa está vacía -------------
        private void HabilitarBotonPedidoStock()
        {
            if (textBoxPedidoIndustria.Text == "")
            {
                buttonPedidoStockIndustrias.Enabled = false;
            }
            else
            {
                buttonPedidoStockIndustrias.Enabled = true;
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

        