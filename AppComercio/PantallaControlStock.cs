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
        private bool CARGAMESTOCK = false;

        // --------------------  actualizar desde stock.txt, preparar datagridviews y combobox,
        // --------------------  revisar stock real < punto rep, habilitar boton de pedido a industrias

        private void RefrescarStock()
        {
            tablaStock.Clear();

            string[] LineasStock = File.ReadAllLines(@"Stock.txt");
            string[] ValoresStock;

            for (int i = 0; i < LineasStock.Length; i++)
            {
                ValoresStock = LineasStock[i].ToString().Split(';');
                string[] row = new string[ValoresStock.Length];

                for (int j = 0; j < ValoresStock.Length; j++)
                {
                    row[j] = ValoresStock[j].Trim();
                }
                tablaStock.Rows.Add(row);
            }

            dgwStock.DataSource = tablaStock;
            dgwStock.Refresh();

            comboBoxCodProducto.DataSource = tablaStock;
            comboBoxCodProducto.DisplayMember = "ID";

            for (int i = 0; i < dgwStock.Rows.Count; i++)
            {
                int.TryParse(dgwStock[1, i].Value.ToString(), out int StockReal);
                int.TryParse(dgwStock[4, i].Value.ToString(), out int StockPendiente);
                int.TryParse(dgwStock[3, i].Value.ToString(), out int StockComprometido);
                int.TryParse(dgwStock[2, i].Value.ToString(), out int PuntoReposicion);

                if ((StockReal + StockPendiente - StockComprometido) < PuntoReposicion)
                {
                    System.Windows.Forms.DataGridViewCellStyle Negrita = new System.Windows.Forms.DataGridViewCellStyle();
                    Negrita.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                    dgwStock[1, i].Style = Negrita;
                    CARGAMESTOCK = true;
                }
                else
                {
                    System.Windows.Forms.DataGridViewCellStyle Normal = new System.Windows.Forms.DataGridViewCellStyle();
                    Normal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
                    dgwStock[1, i].Style = Normal;
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
            tablaEntregas.Clear();

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
            dgwEntregasFabrica.Refresh();
            HabilitarBotonPedidosPendientesStockIndustrias();
        }

        // -------------------- leer las cantidades a reponer y cargarlas en el dgw correspondiente
        private void CargarCantidadesAReponer()
        {
            tablaCantARep.Clear();
       
            string[] LineasAReponer = File.ReadAllLines(@"CantidadesAReponer.txt");
            string[] ValoresAReponer;

            for (int i = 0; i < LineasAReponer.Length; i++)
            {
                ValoresAReponer = LineasAReponer[i].ToString().Split(';');
                string[] row = new string[ValoresAReponer.Length];

                for (int j = 0; j < ValoresAReponer.Length; j++)
                {
                    row[j] = ValoresAReponer[j].Trim();
                }
                tablaCantARep.Rows.Add(row);
            }

            dgwCantidadesAReponer.DataSource = tablaCantARep;
            dgwCantidadesAReponer.Refresh();
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
                    PedidosAreponer.Add(int.Parse(dgwEntregasFabrica.Rows[i].Cells[0].Value.ToString()), int.Parse(dgwEntregasFabrica.Rows[i].Cells[1].Value.ToString()));
                }
            }

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
                    NuevosPedidosAreponer.Add(int.Parse(dgwEntregasFabrica.Rows[i].Cells[0].Value.ToString()), int.Parse(dgwEntregasFabrica.Rows[i].Cells[1].Value.ToString()));
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