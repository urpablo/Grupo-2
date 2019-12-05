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
        private int cantidadProductosStockBajo = 0;
        private int cantidadProductosSobrecomprometidos = 0;
        private bool CARGAMESTOCK = false;

        private void RefrescarStock()
        {
            tablaStock.Clear();
            int PendienteSuma = 0;
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

            cantidadProductosStockBajo = 0;
            cantidadProductosSobrecomprometidos = 0;
            for (int i = 0; i < dgwStock.Rows.Count; i++)
            {
                int.TryParse(dgwStock[1, i].Value.ToString(), out int StockReal);
                int.TryParse(dgwStock[4, i].Value.ToString(), out int StockPendiente);
                int.TryParse(dgwStock[3, i].Value.ToString(), out int StockComprometido);
                int.TryParse(dgwStock[2, i].Value.ToString(), out int PuntoReposicion);

                if ((StockReal + StockPendiente - StockComprometido) < PuntoReposicion)
                {
                    DataGridViewCellStyle Negrita = new DataGridViewCellStyle();
                    Negrita.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
                    dgwStock[1, i].Style = Negrita;
                    CARGAMESTOCK = true;
                    cantidadProductosStockBajo++;
                }
                else
                {
                    DataGridViewCellStyle Normal = new DataGridViewCellStyle();
                    Normal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
                    dgwStock[1, i].Style = Normal;
                }

                if (StockComprometido > StockReal)
                {
                    cantidadProductosSobrecomprometidos++;
                }

                PendienteSuma = PendienteSuma + StockPendiente;
                if (CARGAMESTOCK == true && PendienteSuma == 0)
                {
                    btnGenerarTXTPedidoStockIndustrias.Enabled = true;
                }
                else
                {
                    btnGenerarTXTPedidoStockIndustrias.Enabled = false;
                }

                   
            }

            dgwStock.Refresh();
        }

        private void RefrescarPedidosPendientesIndustriasyEstado()
        {
            if (tablaEntregas.Rows.Count > 0) btnCargarPedidosStockPendientesIndustrias.Enabled = true;
            else btnCargarPedidosStockPendientesIndustrias.Enabled = false;

            LabelEstadoPedidos();
            LabelEstadoLotes();
        }

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

            File.Delete("EntregasStockIndustrias.txt");
            File.Move("nuevostockrepuesto.txt", "EntregasStockIndustrias.txt");

            //recargo el archivo con menos líneas y actualizo el dgw, eventualmente se vaciará
            tablaEntregas.Clear();
            string[] linesASD = File.ReadAllLines(@"EntregasStockIndustrias.txt");
            string[] valuesASD;

            for (int i = 0; i < linesASD.Length; i++)
            {
                valuesASD = linesASD[i].ToString().Split(';');
                string[] row = new string[valuesASD.Length];

                for (int j = 0; j < valuesASD.Length; j++)
                {
                    row[j] = valuesASD[j].Trim();
                }
                tablaEntregas.Rows.Add(row);
            }

            foreach (DataGridViewRow dr1 in dgwEntregasFabrica.Rows)
            {
                dr1.Cells["Recepción"].Value = Convert.ToBoolean(0);
            }

            RefrescarPedidosPendientesIndustriasyEstado();
            dgwEntregasFabrica.Refresh();
            dgwStock.Refresh();
        }
    }
}