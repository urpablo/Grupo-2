using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        // ------------------ validar y cargar datos del comercio
        private void CargarDatosComercio()
        {
            bool lineas = false;
            bool largosplit = false;
            bool comercio = false;
            bool cuit = false;

            if (new FileInfo(@"DatosComercio.txt").Length == 0)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio está vacío. \n \n" +
                                "No se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                string lineaReporte = File.ReadLines(@"DatosComercio.txt").First();
                if (lineaReporte.Contains(';'))
                {
                    string[] lineaSpliteada = lineaReporte.Split(';');
                    if (File.ReadLines(@"DatosComercio.txt").Count() > 1) lineas = true;
                    if (lineaSpliteada.Length != 5) largosplit = true;
                    if (lineaSpliteada[0].Substring(0, 1) != "C" && !int.TryParse(lineaSpliteada[0].Substring(1), out int codComercio)) comercio = true;

                    if (!long.TryParse(lineaSpliteada[2].ToString(), out long cuitParseado)) cuit = true;
                    else if (Digits_IfChain(cuitParseado) == 11) cuit = false; else cuit = true;

                    if (lineas || largosplit || comercio || cuit)
                    {
                        MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio no coincide con lo esperado. \n \n" +
                                    "No se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                    else
                    {
                        textBoxCodComercio.Text = lineaSpliteada[0];
                        textBoxRZ1.Text = lineaSpliteada[1];
                        textBoxRZ2.Text = lineaSpliteada[1];
                        textBoxCUIT.Text = lineaSpliteada[2];
                        textBoxCUIT2.Text = lineaSpliteada[2];
                        textBoxDirEntComercio.Text = lineaSpliteada[3];
                        textBoxDirDevComercio.Text = lineaSpliteada[4];
                        textBoxDatosComercio.Text = textBoxCodComercio.Text + ";" + textBoxRZ1.Text + ";" + textBoxCUIT.Text + ";" + textBoxDirEntComercio.Text;
                        textBoxRemitente.Text = textBoxRZ2.Text + ";" + textBoxCUIT2.Text + ";" + textBoxDirDevComercio.Text;
                    }
                }
                else
                {
                    MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio no coincide con lo esperado. \n \n" +
                                    "No se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        // -------------------- validar + leer las cantidades a reponer de CantidadesReposicionStock.txt, cargarlas en dgwCantidadesAReponer en pantalla de stock
        private void CargarCantidadesAReponer()
        {
            string[] LineasAReponer = File.ReadAllLines(@"CantidadesReposicionStock.txt");
            string[] ValoresAReponer;

            bool lineas = false;
            bool largosplit = false;
            bool noparseable = false;
            bool splitmalvado = false;
            bool IDDuplicados = false;
            List<int> IDDuplicado = new List<int>();

            if (new FileInfo(@"CantidadesReposicionStock.txt").Length == 0)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo" +
                                $" está vacío. \n \nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                if (File.ReadLines(@"CantidadesReposicionStock.txt").Count() != 10) lineas = true;

                foreach (var producto in LineasAReponer)
                {
                    if (producto.Contains(';'))
                    {
                        var productoSpliteado = producto.Split(';');

                        if (productoSpliteado.Length != 2) largosplit = true;

                        bool a = int.TryParse(productoSpliteado[0].ToString(), out int IDSpliteado);
                        bool b = int.TryParse(productoSpliteado[1].ToString(), out int cantSpliteada);
                        if (a == false || b == false) noparseable = true;
                        IDDuplicado.Add(IDSpliteado);
                    }
                    else splitmalvado = true;
                }

                var BuscarDuplicados = IDDuplicado.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                if (BuscarDuplicados.Count > 0) IDDuplicados = true;

                if (lineas || largosplit || noparseable || splitmalvado || IDDuplicados)
                {
                    MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo" +
                                    $" no es un archivo que corresponda al formato esperado. \n \nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    tablaCantARep.Clear();
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

                    dgwCantidadesAReponer.Refresh();
                }
            }
        }

        // -------------------- validar + cargar stock.txt inicialmente. Las proximas iteraciones sobre este archivo van a ser
        // -------------------- sin validar dado que asumo que nadie va a ir a tocar el archivo en el medio de la operatoria del programa
        // -------------------- con arrancar con datos validados alcanza para nuestros propósitos

        private void CargarStockInicial()
        {
            bool lineas = false;
            bool largosplit = false;
            bool noparseable = false;
            bool nosplit = false;
            bool IDDuplicados = false;
            List<int> IDDuplicado = new List<int>();
            string[] LineasStock = File.ReadAllLines(@"Stock.txt");

            if (new FileInfo(@"Stock.txt").Length == 0)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial está vacío. \n \n" +
                                "No se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                if (File.ReadLines(@"CantidadesReposicionStock.txt").Count() != 10) lineas = true;

                foreach (var producto in LineasStock)
                {
                    if (producto.Contains(';'))
                    {
                        var productoSpliteado = producto.Split(';');

                        if (productoSpliteado.Count() != 5) largosplit = true;

                        foreach (var item in productoSpliteado)
                        {
                            if (!int.TryParse(item.ToString(), out int itemParseado)) noparseable = true;
                        }

                        if (noparseable == false)
                        {
                            int.TryParse(productoSpliteado[0].ToString(), out int IDParseado);
                            IDDuplicado.Add(IDParseado);
                        }
                    }
                    else nosplit = true;
                }

                var BuscarDuplicados = IDDuplicado.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                if (BuscarDuplicados.Count > 0) IDDuplicados = true;
            }

            if (lineas || largosplit || noparseable || nosplit || IDDuplicados)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial para operar no coincide con lo esperado. \n \n" +
                                "No se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                RefrescarStock();
                RefrescarEntregasStockIndustrias();
            }
        }

        // no logro que cuitParseado.ToString().Length devuelva la cantidad de caracteres por algún motivo (un long rompe esa funcionalidad?)
        // pero este pedazo de codigo lo soluciona
        private int Digits_IfChain(long n)
        {
            n = System.Math.Abs(n);
            if (n < 10L) return 1;
            if (n < 100L) return 2;
            if (n < 1000L) return 3;
            if (n < 10000L) return 4;
            if (n < 100000L) return 5;
            if (n < 1000000L) return 6;
            if (n < 10000000L) return 7;
            if (n < 100000000L) return 8;
            if (n < 1000000000L) return 9;
            if (n < 10000000000L) return 10;
            if (n < 100000000000L) return 11;
            if (n < 1000000000000L) return 12;
            if (n < 10000000000000L) return 13;
            if (n < 100000000000000L) return 14;
            if (n < 1000000000000000L) return 15;
            if (n < 10000000000000000L) return 16;
            if (n < 100000000000000000L) return 17;
            if (n < 1000000000000000000L) return 18;
            return 19;
        }
    }
}