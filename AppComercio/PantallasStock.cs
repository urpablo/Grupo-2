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


        //--------------------- leer stock.txt al cargar el programa -----------------------------------

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

        }



        //--------------------- leer stock.txt al cargar el programa -----------------------------------


       

    }

}