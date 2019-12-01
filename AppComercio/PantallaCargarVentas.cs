using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        // -------------------- boton agregar item de cargar ventas
        private void btnAgregarItemPedido_Click(object sender, EventArgs e)
        {
            bool actualizo = false;

            // Reviso todos los items del LVI para ver si el código de producto ya fue ingresado
            // si lo fue, sumo al valor existente el ingresado
            foreach (ListViewItem itemLVI in listviewPedidos.Items)
            {
                if (itemLVI.SubItems[0].Text == comboBoxCodProducto.Text)
                {
                    int.TryParse(itemLVI.SubItems[1].Text, out int original);
                    int.TryParse(textBoxCantidadItem.Text, out int sumar);

                    itemLVI.SubItems[1].Text = "" + (original + sumar);

                    actualizo = true;
                    textBoxCantidadItem.Clear();
                }
            }

            // Sino, lo agrego a la lista
            if (actualizo == false)
            {
                ListViewItem itemLVI = new ListViewItem(comboBoxCodProducto.Text);
                itemLVI.SubItems.Add(textBoxCantidadItem.Text);
                listviewPedidos.Items.Add(itemLVI);
                textBoxCantidadItem.Clear();
            }

            habilitarBotonConfirmarPedido();
        }

        // -------------------- boton confirmar pedido de cargar ventas
        private void btnConfirmarPedidoVentas_Click(object sender, EventArgs e)
        {
            int IdStock = 0;
            int KStock = 0;
            int IdPed = 0;
            int KPed = 0;
            //int kComp = 0;
            string parametrosinv;

            Dictionary<int, string> InventarioTemporal = new Dictionary<int, string>();

            // Escribe el pedido que se acaba de cargar en el listview a txt
            using (StreamWriter sw = new StreamWriter("PedidoTemporal.txt"))
            {
                foreach (ListViewItem item in listviewPedidos.Items)
                {
                    sw.Write(textBoxCodClientePedido.Text + ";" + textBoxDireccionEntregaPedido.Text + ";");
                    sw.Write(item.Text);
                    for (int i = 1; i < item.SubItems.Count; i++)
                        sw.Write(";" + item.SubItems[i].Text);
                    sw.Write("\n");
                }
            }

            // Levanta en memoria el pedido temporal
            var lineaspedido = File
                      .ReadAllLines("PedidoTemporal.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          a1 = record[0],
                          a2 = record[1],
                          a3 = int.Parse(record[2]),
                          a4 = int.Parse(record[3])
                      }).ToList();

            // Levanta en memoria el stock actual
            var lineasstock = File
                      .ReadAllLines("Stock.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          b1 = int.Parse(record[0]),
                          b2 = int.Parse(record[1]),
                          b3 = int.Parse(record[2]),
                          b4 = int.Parse(record[3]),
                          b5 = int.Parse(record[4])
                      }).ToList();

            // Agrega comprometido a stock temporal
            foreach (var registroStock in lineasstock)
            {
                IdStock = registroStock.b1;
                KStock = registroStock.b4;

                foreach (var registroPedido in lineaspedido)
                {
                    IdPed = registroPedido.a3;
                    KPed = registroPedido.a4;

                    if (IdStock == IdPed)
                    {
                        int sumcomprometido = KStock + KPed;
                        parametrosinv = registroStock.b2 + ";" + registroStock.b3 + ";" + sumcomprometido + ";" + registroStock.b5;

                        InventarioTemporal.Add(IdStock, parametrosinv);
                    }
                }
                if (!InventarioTemporal.ContainsKey(IdStock))
                {
                    parametrosinv = registroStock.b2 + ";" + registroStock.b3 + ";" + KStock + ";" + registroStock.b5;
                    InventarioTemporal.Add(IdStock, parametrosinv);
                }
            }

            // Pisa el stock.txt anterior con el actualizado de estas operaciones
            using (StreamWriter sw2 = new StreamWriter("StockTemporal.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporal)
                {
                    sw2.Write(entry.Key);
                    sw2.Write(";");
                    sw2.Write(entry.Value);
                    sw2.Write("\n");
                }
            }

            File.Delete("Stock.txt");
            File.Move("StockTemporal.txt", "Stock.txt");

            // Guarda los pedidos de esta venta cargada
            using (StreamWriter sw3 = File.AppendText("Pedidos.txt"))
            {
                string[] leertexto = File.ReadAllLines("PedidoTemporal.txt");
                foreach (string s in leertexto)
                {
                    sw3.Write(s);
                    sw3.Write("\n");
                }
            }

            // Muestra el aviso de que se completó la carga del pedido con exito, habilita el botón de generar lote
            // dado que tenemos al menos un pedido para despachar, y limpia la pantalla
            MessageBox.Show("¡Pedido agregado exitosamente al lote actual! \n \n " +
                "Cuando termine de agregar ventas, puede generar el lote final diario " +
                "para logística desde la sección enviar ventas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnGenerarTXTLote.Enabled = true;
            limpiarPantallaCargarVentas();
        }

        // ----------------- boton limpiar pedidos de cargar ventas
        private void btnLimpiarPantallaVentas_Click(object sender, EventArgs e)
        {
            limpiarPantallaCargarVentas();
        }

        private void limpiarPantallaCargarVentas()
        {
            listviewPedidos.Items.Clear();
            textBoxDireccionEntregaPedido.Clear();
            textBoxCodClientePedido.Clear();
            textBoxCantidadItem.Clear();
        }
    }
}