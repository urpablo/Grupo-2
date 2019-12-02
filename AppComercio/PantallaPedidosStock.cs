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
        private List<int> RNGexistentePedido = new List<int>();
        private int codPedidoRNG = 0;


        // -------------------- hacer pedido de stock a industrias

        private void btnGenerarTXTPedidoStockIndustrias_Click(object sender, EventArgs e)
        {
            //levanta en memoria el stock actual
    
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

            
    
           

            Dictionary<int, string> InventarioTemporal = new Dictionary<int, string>();
            foreach (var regStock in lineasstock)
            {
                int actual = regStock.b2;
                int pp = regStock.b5;
                int comp = regStock.b4;
                int pr = regStock.b3;
                int IdStock = regStock.b1;
                // string parametrosinv;
                int cantarepo = 0;

                if (((actual + pp) - comp) < pr)
                {
                    foreach (DataGridViewRow dr in dgwCantidadesAReponer.Rows)
                    {
                        if (IdStock == int.Parse(dr.Cells["ID"].Value.ToString()))
                        {
                            cantarepo = int.Parse(dr.Cells["Cantidad reposición"].Value.ToString());
                        }
                    }

                    InventarioTemporal.Add(IdStock, cantarepo.ToString());
                }
            }

            using (StreamWriter sw12 = new StreamWriter("EntregasStockIndustrias.txt"))
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
                      .ReadAllLines("EntregasStockIndustrias.txt")
                      .Select(record => record.Split(';'))
                      .Select(record => new
                      {
                          c1 = record[0],
                          c2 = int.Parse(record[1]),
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

            // Número aleatorio para el número de pedido.
            // Valido que el número aleatorio que se genere no se repita
            Random r2 = new Random();
            do
            {
                codPedidoRNG = r2.Next(0, 999);
            } while (RNGexistentePedido.Contains(codPedidoRNG));
            RNGexistentePedido.Add(codPedidoRNG);

            using (StreamWriter sw14 = new StreamWriter("Pedido_A" + codPedidoRNG + ".txt"))
            {
                sw14.Write(textBoxCodComercio.Text + ";" + textBoxRZ1.Text + ";" + textBoxCUIT.Text + ";" + textBoxDirEntComercio.Text);
                sw14.Write("\n");
                sw14.Write("---");
                sw14.Write("\n");
            }

            using (StreamWriter sw15 = File.AppendText("Pedido_A" + codPedidoRNG + ".txt"))
            {
                string[] readText = File.ReadAllLines("EntregasStockIndustrias.txt");
                foreach (string s in readText)
                {
                    sw15.Write("P" + s);
                    sw15.Write("\n");
                }
            }

            // Vuelco el pedido al textbox de vista previa, armando el historial visual para esta sesión.
            // Genero el nombre del archivo de pedido para esta tanda
            // Uso el contador de lote para avanzar los días, que solo avanza con cada lote enviado
            // La lógica es que el stock se pide causado por ventas que están atadas a los lotes, no son pedidos independientes
            string PedidoGenerado = ("Pedido_A" + codPedidoRNG + ".txt");
            textBoxPedidoIndustria.AppendText($"*** Día {codLote}: {PedidoGenerado} ***" + Environment.NewLine);
            foreach (var line in File.ReadLines("Pedido_A" + codPedidoRNG + ".txt").Skip(1))
            {
                textBoxPedidoIndustria.AppendText(line + Environment.NewLine);
            }

           // Como borro el directorio Grupo2 en la carga del formulario, nunca va a haber una colisión por mismo nombre de archivo ni por el chequeo del RNG anterior
            File.Move("Pedido_A" + codPedidoRNG + ".txt", @"c:\Grupo2\" + PedidoGenerado);
            MessageBox.Show($"¡Pedido a industrias diario generado! \n \n El archivo {PedidoGenerado} se encuentra en la carpeta Grupo2 en la raíz del disco C. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Deshabilito botón de pedir stock a industrias, y veo si de este pedido de stock tengo que rellenar y habilitar el de pendientes
            btnGenerarTXTPedidoStockIndustrias.Enabled = false;
            RefrescarEntregasStockIndustrias();
        }
    }
}