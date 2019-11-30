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
        private DataTable tablaStock = new DataTable();
        private DataTable tablaEntregas = new DataTable();
        private DataTable tablaCantARep = new DataTable();

        // --------------------  actualizar desde stock.txt, preparar datagridviews y combobox,
        // --------------------  revisar stock real < punto rep, habilitar boton de pedido a industrias

        private void RefrescarStock()
        {
            tablaStock.Rows.Clear();
            dgwStock.Refresh();

            string[] lines = File.ReadAllLines(@"Stock.txt");
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(';');
                string[] row = new string[values.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    row[j] = values[j].Trim();
                }
                tablaStock.Rows.Add(row);
            }

            dgwStock.DataSource = tablaStock;
            comboBoxCodProducto.DataSource = tablaStock;
            comboBoxCodProducto.DisplayMember = "ID";

            foreach (DataGridViewRow dr in dgwStock.Rows)
            {
                int real2 = int.Parse(dr.Cells[tablaStock.Columns.IndexOf("Real")].Value.ToString());
                int pend2 = int.Parse(dr.Cells[tablaStock.Columns.IndexOf("Pendientes")].Value.ToString());
                int comp2 = int.Parse(dr.Cells[tablaStock.Columns.IndexOf("Comprometido")].Value.ToString());
                int ptorep = int.Parse(dr.Cells[tablaStock.Columns.IndexOf("Punto de Reposición")].Value.ToString());

                if (((real2 + pend2) - comp2) < ptorep)
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
            }

            if (CARGAMESTOCK == true)
            {
                btnGenerarTXTPedidoStockIndustrias.Enabled = true;
            }
            else
            {
                btnGenerarTXTPedidoStockIndustrias.Enabled = false;
            }
        }

        // -------------------- leer AReponer.txt y preparar datagridviews
        private void RefrescarEntregasStockIndustrias()
        {
            tablaEntregas.Rows.Clear();
            dgwEntregasFabrica.Refresh();

            string[] lines = File.ReadAllLines(@"AReponer.txt");
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(';');
                string[] row = new string[values.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    row[j] = values[j].Trim();
                }
                tablaEntregas.Rows.Add(row);
            }

            foreach (DataGridViewRow dr1 in dgwEntregasFabrica.Rows)
            {
                dr1.Cells["Recepción"].Value = Convert.ToBoolean(0);
            }

            dgwEntregasFabrica.DataSource = tablaEntregas;
            HabilitarBotonPedidosPendientesStockIndustrias();
        }

        // -------------------- leer las cantidades a reponer y cargarlas en el dgw correspondiente
        private void CargarCantidadesAReponer()
        {
            tablaCantARep.Rows.Clear();
            dgwCantidadesAReponer.Refresh();

            string[] lines = File.ReadAllLines(@"CantidadesAReponer.txt");
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(';');
                string[] row = new string[values.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    row[j] = values[j].Trim();
                }
                tablaCantARep.Rows.Add(row);
            }

            dgwCantidadesAReponer.DataSource = tablaCantARep;
        }

        // -------------------- boton aceptar pedidos pendientes de industrias/stock
        private void btnCargarPedidosStockPendientesIndustrias_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> PedidosAreponer = new Dictionary<int, int>();
            Dictionary<int, string> InventarioTemporal3 = new Dictionary<int, string>();
            Dictionary<int, int> NuevosPedidosAreponer = new Dictionary<int, int>();

            for (int i = 0; i < dgwEntregasFabrica.Rows.Count; i++)
            {
                if (dgwEntregasFabrica.Rows[i].Cells[2].Value == null)
                {
                    dgwEntregasFabrica.Rows[i].Cells[2].Value = false;
                }

                bool isCellChecked = (bool)dgwEntregasFabrica.Rows[i].Cells[2].Value;
                if (isCellChecked == true)
                {
                    PedidosAreponer.Add(Int32.Parse(dgwEntregasFabrica.Rows[i].Cells[0].Value.ToString()), Int32.Parse(dgwEntregasFabrica.Rows[i].Cells[1].Value.ToString()));
                }
            }

            var lineasstock = File
                      .ReadAllLines("Stock.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          b1 = Int32.Parse(record[0]),
                          b2 = Int32.Parse(record[1]),
                          b3 = Int32.Parse(record[2]),
                          b4 = Int32.Parse(record[3]),
                          b5 = Int32.Parse(record[4])
                      }).ToList();

            foreach (var regStock in lineasstock)
            {
                int actual = regStock.b2;
                int pp = regStock.b5;
                int comp = regStock.b4;
                int pr = regStock.b3;
                int IdStock = regStock.b1;
                string parametrosinv;

                foreach (KeyValuePair<int, int> item in PedidosAreponer)
                {
                    int idDic = item.Key;
                    int cantDic = item.Value;
                    if (IdStock == idDic)
                    {
                        parametrosinv = (actual + cantDic) + ";" + pr + ";" + comp + ";" + (pp - cantDic);

                        InventarioTemporal3.Add(IdStock, parametrosinv);
                    }
                }
                if (!InventarioTemporal3.ContainsKey(IdStock))
                {
                    parametrosinv = actual + ";" + pr + ";" + comp + ";" + pp;
                    InventarioTemporal3.Add(IdStock, parametrosinv);
                }
            }

            using (StreamWriter sw100 = new StreamWriter("stockrepuesto.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporal3)
                {
                    sw100.Write(entry.Key);
                    sw100.Write(";");
                    sw100.Write(entry.Value);
                    sw100.Write("\n");
                }
            }

            File.Delete("Stock.txt");
            File.Move("stockrepuesto.txt", "Stock.txt");

            RefrescarStock();

            for (int i = 0; i < dgwEntregasFabrica.Rows.Count; i++)
            {
                bool isCellChecked = (bool)dgwEntregasFabrica.Rows[i].Cells[2].Value;
                if (isCellChecked == true)
                {
                }
                else
                {
                    NuevosPedidosAreponer.Add(Int32.Parse(dgwEntregasFabrica.Rows[i].Cells[0].Value.ToString()), Int32.Parse(dgwEntregasFabrica.Rows[i].Cells[1].Value.ToString()));
                }
            }

            using (StreamWriter sw101 = new StreamWriter("nuevostockrepuesto.txt"))
            {
                foreach (KeyValuePair<int, int> entry in NuevosPedidosAreponer)
                {
                    sw101.Write(entry.Key);
                    sw101.Write(";");
                    sw101.Write(entry.Value);
                    sw101.Write("\n");
                }
            }

            File.Delete("AReponer.txt");
            File.Move("nuevostockrepuesto.txt", "AReponer.txt");

            RefrescarEntregasStockIndustrias();

            if (!File.Exists("AReponer.txt"))
            {
                btnCargarPedidosStockPendientesIndustrias.Enabled = false;
            }

            CARGAMESTOCK = false;
        }
    }
}