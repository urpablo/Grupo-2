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
        DataTable tablaStock = new DataTable();

        //--------------------- leer stock.txt al cargar el programa y preparar datagridviews y combobox -----------------------------------

        private void refrescarstock()
        {
            tablaStock.Rows.Clear();
            dgwStock.Refresh();

            string[] lines = File.ReadAllLines(@"Stock.txt");
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(',');
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
            
        }
        //--------------------- leer stock.txt al cargar el programa y preparar datagridviews y combobox -----------------------------------

        //--------------------- evitar modificar el datagridview de stock si la fila ingresada no esta vacia



        private void buttonPedidoStockIndustrias_Click(object sender, EventArgs e)
        {



            //levanta en memoria el stock actual
            var lineasstock = File
                      .ReadAllLines("Stock.txt")
                      .Select(record => record.Split(','))
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

                Dictionary<int, string> InventarioTemporal = new Dictionary<int, string>();

                if (((actual + pp) - comp) < pr)
                {

                    InventarioTemporal.Add(IdStock, comp.ToString());

                    using (StreamWriter sw12 = new StreamWriter("AReponer.txt"))
                    {
                        foreach (KeyValuePair<int, string> entry in InventarioTemporal)
                        {
                            sw12.Write(entry.Key);
                            sw12.Write(",");
                            sw12.Write(entry.Value);
                            sw12.Write("\n");
                        }
                    }
                }
            }

            var lineasrepone = File
                      .ReadAllLines("AReponer.txt")
                      .Select(record => record.Split(','))
                      .Select(record => new
                      {
                          c1 = record[0],
                          c2 = Int32.Parse(record[1]),
                      }).ToList();

            Dictionary<int, string> InventarioTemporal2 = new Dictionary<int, string>();

            foreach (var regStock in lineasstock)
            {
                int actual = regStock.b2;
                int pp = regStock.b5;
                int comp = regStock.b4;
                int pr = regStock.b3;
                int IdStock = regStock.b1;
                string parametrosinv;



                foreach (var regPed in lineasrepone)
                {
                    string cdRepo = regPed.c1;
                    int cantRepo = regPed.c2;

                    if (IdStock.ToString() == cdRepo)
                    {
                        parametrosinv = actual + "," + pr + "," + comp + "," + cantRepo;

                        InventarioTemporal2.Add(IdStock, parametrosinv);


                    }


                }

                if (!InventarioTemporal2.ContainsKey(IdStock))
                {
                    parametrosinv = actual + "," + pr + "," + comp + "," + pp;
                    InventarioTemporal2.Add(IdStock, parametrosinv);
                }
            }

            using (StreamWriter sw13 = new StreamWriter("stockconpp.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporal2)
                {
                    sw13.Write(entry.Key);
                    sw13.Write(",");
                    sw13.Write(entry.Value);
                    sw13.Write("\n");
                }
            }

            File.Delete("Stock.txt");
            File.Move("stockconpp.txt", "Stock.txt");

            DateTime date = DateTime.Now;
            long n = long.Parse(date.ToString("yyyyMMddHHmmss"));

            using (StreamWriter sw14 = new StreamWriter("Lote_" + n + ".txt"))
            {
                sw14.Write(textBoxCodComercio.Text + "," + textBoxRazSoc.Text + "," + textBoxCUIT.Text + "," + textBoxDireccion.Text);
                sw14.Write("\n");
                sw14.Write("---");
                sw14.Write("\n");

            }

            using (StreamWriter sw15 = File.AppendText("Lote_" + n + ".txt"))
            {
                string[] readText = File.ReadAllLines("AReponer.txt");
                foreach (string s in readText)
                {
                    sw15.Write(s);
                    sw15.Write("\n");
                }


            }


        }

    }

}