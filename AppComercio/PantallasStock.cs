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
        DataTable tablaEntregas = new DataTable();



        // ----------------------------------- cargar datos del comercio -------------------------

        private void cargarDatosComercio()
        {
            string[] dComercio = File.ReadAllLines(@"DatosComercio.txt");
            textBoxCodComercio.Text = dComercio[0];
            textBoxRazSoc.Text = dComercio[1];
            textBoxRzSoc.Text = dComercio[1];
            textBoxCUIT.Text = dComercio[2];
            textBoxCUIT2.Text = dComercio[2];
            textBoxDirEntComercio.Text = dComercio[3];
            textBoxDirDevComercio.Text = dComercio[4];
            textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDirEntComercio.Text;
            textBoxRemitente.Text = textBoxRzSoc.Text + ";" + textBoxCUIT2.Text + ";" + textBoxDirDevComercio.Text;
        }


        //---------------------  leer stock.txt al cargar el programa y preparar datagridviews y combobox -----------------------------------

        private void refrescarstock()
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

        }
        //--------------------- leer AReponer.txt al cargar el programa y preparar datagridviews -----------------------------------
        private void refrescarEntregas()
        {

            tablaEntregas.Rows.Clear();
            dataGridView1.Refresh();

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

            dataGridView1.DataSource = tablaEntregas;
            habilitarBotonPedidosPendientes();

        }


      

// -------------------- hacer pedido de stock a industrias -----------------

private void buttonPedidoStockIndustrias_Click(object sender, EventArgs e)
        {

            //levanta en memoria el stock actual
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

            Dictionary<int, string> InventarioTemporal = new Dictionary<int, string>();
            foreach (var regStock in lineasstock)
            {
                int actual = regStock.b2;
                int pp = regStock.b5;
                int comp = regStock.b4;
                int pr = regStock.b3;
                int IdStock = regStock.b1;
                string parametrosinv;
                int cantarepo = 0;

                

                if (((actual + pp) - comp) < pr)
                {
                    if (IdStock == 1)
                    {
                        cantarepo = 10000;

                    }
                    else if (IdStock == 2)
                    {
                        cantarepo = 10500;
                    }
                    else if (IdStock == 3)
                    {
                        cantarepo = 11000;
                    }
                    else if (IdStock == 4)
                    {
                        cantarepo = 11500;
                    }
                    else if (IdStock == 5)
                    {
                        cantarepo = 12000;
                    }
                    else if (IdStock == 6)
                    {
                        cantarepo = 12500;
                    }
                    else if (IdStock == 7)
                    {
                        cantarepo = 13000;
                    }
                    else if (IdStock == 8)
                    {
                        cantarepo = 13500;
                    }
                    else if (IdStock == 9)
                    {
                        cantarepo = 14000;
                    }
                    else if (IdStock == 10)
                    {
                        cantarepo = 14500;
                    }


                    InventarioTemporal.Add(IdStock, cantarepo.ToString());

                    
                }

                
            }

            using (StreamWriter sw12 = new StreamWriter("AReponer.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporal)
                {
                    sw12.Write(entry.Key);
                    sw12.Write(";");
                    sw12.Write(entry.Value);
                    sw12.Write("\n");
                }
            }

            using (StreamWriter sw12 = new StreamWriter("AReponer.txt"))
                    {
                        foreach (KeyValuePair<int, string> entry in InventarioTemporal)
                        {
                            sw12.Write(entry.Key);
                            sw12.Write(";");
                            sw12.Write(entry.Value);
                            sw12.Write("\n");
                        }
                    }
            var lineasrepone = File
                      .ReadAllLines("AReponer.txt")
                      .Select(record => record.Split(';'))
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
                        parametrosinv = actual + ";" + pr + ";" + comp + ";" + cantRepo;

                        InventarioTemporal2.Add(IdStock, parametrosinv);


                    }


                }

                if (!InventarioTemporal2.ContainsKey(IdStock))
                {
                    parametrosinv = actual + ";" + pr + ";" + comp + ";" + pp;
                    InventarioTemporal2.Add(IdStock, parametrosinv);
                }
            }

            using (StreamWriter sw13 = new StreamWriter("stockconpp.txt"))
            {
                foreach (KeyValuePair<int, string> entry in InventarioTemporal2)
                {

                    sw13.Write(entry.Key);
                    sw13.Write(";");
                    sw13.Write(entry.Value);
                    sw13.Write("\n");
                }
            }

            File.Delete("Stock.txt");
            File.Move("stockconpp.txt", "Stock.txt");

            //DateTime date = DateTime.Now;
            //long n = long.Parse(date.ToString("yyyyMMddHHmmss"));

            Random r = new Random();
            int q = r.Next(0, 999);

            using (StreamWriter sw14 = new StreamWriter("Pedido_A" + q + ".txt"))
            {
                sw14.Write(textBoxCodComercio.Text + ";" + textBoxRazSoc.Text + ";" + textBoxCUIT.Text + ";" + textBoxDirEntComercio.Text);
                sw14.Write("\n");
                sw14.Write("---");
                sw14.Write("\n");

            }

            using (StreamWriter sw15 = File.AppendText("Pedido_A" + q + ".txt"))
            {
                string[] readText = File.ReadAllLines("AReponer.txt");
                foreach (string s in readText)
                {
                    sw15.Write("P" + s);
                    sw15.Write("\n");
                }


            }

            //vuelco el pedido al textbox de vista previa
            textBoxPedidoIndustria.Text = "";
            foreach (var line in File.ReadLines("Pedido_A" + q + ".txt").Skip(1))
            {
                textBoxPedidoIndustria.AppendText(line + Environment.NewLine);
            }


            // como borro el directorio Grupo2 en la carga del formulario, nunca va a haber una colisión por mismo nombre de archivo 
            File.Move("Pedido_A" + q + ".txt", @"c:\Grupo2\" + "Pedido_A" + q + ".txt");
            MessageBox.Show("¡Pedido a industrias diario generado! \n \n El archivo se encuentra en la carpeta Grupo2 en la raíz del disco C. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            buttonPedidoStockIndustrias.Enabled = false;
            habilitarBotonPedidosPendientes();

        }


        private void buttonPedidosPendientes_Click(object sender, EventArgs e)
        {
         
                Dictionary<int, int> PedidosAreponer = new Dictionary<int, int>();
                Dictionary<int, string> InventarioTemporal3 = new Dictionary<int, string>();
                Dictionary<int, int> NuevosPedidosAreponer = new Dictionary<int, int>();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool isCellChecked = (bool)dataGridView1.Rows[i].Cells[2].Value;
                    if (isCellChecked == true)
                    {
                        PedidosAreponer.Add(Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()), Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
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

                refrescarstock();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool isCellChecked = (bool)dataGridView1.Rows[i].Cells[2].Value;
                    if (isCellChecked == true)
                    {

                    }
                    else
                    {
                        NuevosPedidosAreponer.Add(Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()), Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
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

                refrescarEntregas();

                if (!File.Exists("AReponer.txt"))
                {
                    buttonPedidosPendientes.Enabled = false;
                }

            habilitarBotonPedidosIndustrias();
            CARGAMESTOCK = false;

   

        }
    }

}