using System;
using System.IO;
using System.Windows.Forms;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        

        // ---------------------------- Validaciones dgw cantidades a reponer (solo dígitos, no ceros, no se puede dejar vacío)
        private void dgwCantARep_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dgwCantidadesAReponer.CurrentCell.ColumnIndex == 1)
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgwCantARep_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgwCantidadesAReponer.Columns[e.ColumnIndex].Name == "Cantidad reposición")
            {
                if (String.IsNullOrEmpty(e.FormattedValue.ToString()) || e.FormattedValue.ToString() == "0")
                {
                    dgwCantidadesAReponer.Rows[e.RowIndex].ErrorText = "La celda no puede quedar vacía o ser 0";
                    e.Cancel = true;
                }
                else
                {
                    dgwCantidadesAReponer.Rows[e.RowIndex].ErrorText = "";
                }
            }
        }

        // ------------------ si este archivo tiene un largo de 0 (es decir, vacío) no se habiilta el botón de pedidos pendientes

        private void HabilitarBotonPedidosPendientesStockIndustrias()
        {
            if (new FileInfo("AReponer.txt").Length == 0)
            {
                btnCargarPedidosStockPendientesIndustrias.Enabled = false;
            }
            else
            {
                btnCargarPedidosStockPendientesIndustrias.Enabled = true;
            }
        }

        // ------------------ Habilitar botón de agregar item en ventas solo si los dos textboxes tienen contenido

        private void textBoxCantidadItem_TextChanged(object sender, EventArgs e)
        {
            habilitarBotonAgregarItem();
        }

        private void habilitarBotonAgregarItem()
        {
            if (!string.IsNullOrWhiteSpace(textBoxCantidadItem.Text)
                && !string.IsNullOrWhiteSpace(comboBoxCodProducto.Text))
            {
                btnAgregarItemPedido.Enabled = true;
            }
            else
            {
                btnAgregarItemPedido.Enabled = false;
            }
        }

        // ------------------ Habilitar botón de confirmar pedido en ventas solo si los dos textboxes tienen contenido y el listview no esta vacío

        private void textBoxCodClientePedido_TextChanged(object sender, EventArgs e)
        {
            habilitarBotonConfirmarPedido();
        }

        private void textBoxDireccionEntregaPedido_TextChanged(object sender, EventArgs e)
        {
            habilitarBotonConfirmarPedido();
        }

        private void habilitarBotonConfirmarPedido()
        {
            if (!string.IsNullOrWhiteSpace(textBoxDireccionEntregaPedido.Text)
                && !string.IsNullOrWhiteSpace(textBoxCodClientePedido.Text)
                && listviewPedidos.Items.Count != 0)
            {
                btnConfirmarPedidoVentas.Enabled = true;
            }
            else
            {
                btnConfirmarPedidoVentas.Enabled = false;
            }
        }

        // ------------------ Habilitar botón de cargar pedidos no entregados en reportes de entrega solo si se ha cargado un reporte

        private void textBoxCodClienteReporte_TextChanged(object sender, EventArgs e)
        {
            habilitarBotonCargarNoEntregadosIndustrias();
        }

        private void textBoxCodLoteReporte_TextChanged(object sender, EventArgs e)
        {
            habilitarBotonCargarNoEntregadosIndustrias();
        }

        private void habilitarBotonCargarNoEntregadosIndustrias()
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

        // ------------------ Validaciones: evitar entradas espurias en los textbox
        private void textBoxDireccionEntregaPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxCodClientePedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}