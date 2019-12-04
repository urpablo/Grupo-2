using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        private List<int> IDStock = new List<int>();
        private List<int> IDReposicion = new List<int>();

        // ------------------ validar y cargar datos del comercio
        private void CargarDatosComercio()
        {
            bool existe = true;
            bool vacio = false;
            bool lineas = false;
            bool delimitador = false;
            bool largosplit = false;
            bool comercio = false;
            bool cuit = false;

            if (new FileInfo(@"DatosComercio.txt").Exists != true) existe = false;
            if (existe == true && new FileInfo(@"DatosComercio.txt").Length == 0) vacio = true;

            if (existe == true && vacio == false)
            {
                if (File.ReadAllLines(@"DatosComercio.txt").Count() > 1) lineas = true;
                if (!File.ReadAllLines(@"DatosComercio.txt").First().Contains(';')) delimitador = true;
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false)
            {
                string lineaDatosComercio = File.ReadAllLines(@"DatosComercio.txt").First();
                string[] lineaSpliteada = lineaDatosComercio.Split(';');
                if (lineaSpliteada.Length != 5) largosplit = true;
                if (lineaSpliteada[0].Substring(0, 1) == "C" && int.TryParse(lineaSpliteada[0].Substring(1), out int codComercio))
                {
                    if (codComercio.ToString().Length > 5)
                    {
                        comercio = true;
                    }
                }
                else
                {
                    comercio = true;
                }

                if (!long.TryParse(lineaSpliteada[2].ToString(), out long cuitParseado)) cuit = true;
                else if (Digits_IfChain(cuitParseado) != 11) cuit = true;
            }

            if (existe == false)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio no existe. Revise el archivo." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == true)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio está vacío. Revise el archivo." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == true)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio tiene más de una línea. " +
                                $"Debe contener toda la información en una sola. Revise el archivo." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == true)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio no tiene un formato correcto. " +
                                $"El separador de la información debe ser el caracter ';'. Revise el archivo." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == true)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio no tiene un formato correcto. " +
                                $"Debe contener cinco elementos de datos. Revise el archivo." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && comercio == true && cuit == false)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio no posee un código de comercio correcto. " +
                                $"Debe comenzar con la letra C + un número menor a 5 dígitos." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && comercio == false && cuit == true)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio no posee un número de CUIT correcto. " +
                                $"Debe tener 11 dígitos y no tener guiones." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && comercio == true && cuit == true)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio " +
                                $"no posee un número de CUIT correcto ni un código de comercio correcto. " +
                                $"El CUIT Debe tener 11 dígitos y no tener guiones. " +
                                $"El código de comercio debe comenzar con la letra C + un número menor a 5 dígitos." +
                                "\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && comercio == false && cuit == false)
            {
                string lineaDatosComercio = File.ReadAllLines(@"DatosComercio.txt").First(); //es válido
                string[] lineaSpliteada = lineaDatosComercio.Split(';');

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

        // -------------------- validar + leer las cantidades a reponer de CantidadesReposicionStock.txt, cargarlas en dgwCantidadesAReponer en pantalla de stock
        private void CargarCantidadesAReponer()
        {
            bool existe = true;
            bool vacio = false;
            bool lineas = false;
            bool delimitador = false;
            bool largosplit = false;
            bool IDDuplicados = false;
            bool noparseable = false;
            List<int> IDDuplicado = new List<int>();

            if (new FileInfo(@"CantidadesReposicionStock.txt").Exists != true) existe = false;
            if (existe == true && new FileInfo(@"CantidadesReposicionStock.txt").Length == 0) vacio = true;

            if (existe == true && vacio == false)
            {
                string[] LineasAReponer = File.ReadAllLines(@"CantidadesReposicionStock.txt");
                if (LineasAReponer.Count() != 10) lineas = true;
                foreach (var prdCant in LineasAReponer)
                {
                    if (!prdCant.Contains(';')) delimitador = true;
                }
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false)
            {
                string[] LineasAReponer = File.ReadAllLines(@"CantidadesReposicionStock.txt");
                foreach (var prdCant in LineasAReponer)
                {
                    string[] productoSpliteado = prdCant.Split(';');
                    if (productoSpliteado.Length != 2) largosplit = true;

                    bool a = int.TryParse(productoSpliteado[0].ToString(), out int IDSpliteado);
                    bool b = int.TryParse(productoSpliteado[1].ToString(), out int cantSpliteada);
                    if (a == false || b == false) noparseable = true;
                    IDDuplicado.Add(IDSpliteado);
                }
                var BuscarDuplicados = IDDuplicado.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                if (BuscarDuplicados.Count > 0)
                {
                    IDDuplicados = true;
                }
                else
                {
                    IDStock = IDDuplicado;
                }
            }

            if (existe == false)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo no existe. " +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == true)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo está vacío. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == true)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo " +
                                $"debe contener diez líneas, cada una indicando un producto. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == true)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo " +
                                $"no contiene el delimitador de datos correcto ';' en alguna de sus líneas, o bien contiene líneas vacías. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == true)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo " +
                                $"no contiene datos correctos. Cada línea debe contener dos números enteros separados por el delimitador ';'. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && noparseable == true)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo " +
                                $"no contiene datos correctos. Cada línea debe contener dos números enteros separados por el delimitador ';'. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && noparseable == false && IDDuplicados == true)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo " +
                                $"contiene líneas que describen cantidades para el mismo ID de producto. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && noparseable == false && IDDuplicados == false)
            {
                string[] LineasAReponer = File.ReadAllLines(@"CantidadesReposicionStock.txt");// es válido
                string[] ValoresAReponer;
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

        // -------------------- validar + cargar stock.txt inicialmente. Las proximas iteraciones sobre este archivo van a ser
        // -------------------- sin validar dado que asumo que nadie va a ir a tocar el archivo en el medio de la operatoria del programa
        // -------------------- con arrancar con datos validados alcanza para nuestros propósitos

        private void CargarStockInicial()
        {
            bool existe = true;
            bool vacio = false;
            bool lineas = false;
            bool delimitador = false;
            bool largosplit = false;
            bool IDDuplicados = false;
            bool noparseable = false;
            List<int> IDDuplicado = new List<int>();

            if (new FileInfo(@"Stock.txt").Exists != true) existe = false;
            if (existe == true && new FileInfo(@"Stock.txt").Length == 0) vacio = true;

            if (existe == true && vacio == false)
            {
                string[] LineasStock = File.ReadAllLines(@"Stock.txt");
                if (LineasStock.Count() != 10) lineas = true;
                foreach (var productoStock in LineasStock)
                {
                    if (!productoStock.Contains(';')) delimitador = true;
                }
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false)
            {
                string[] LineasStock = File.ReadAllLines(@"Stock.txt");
                foreach (var productoStock in LineasStock)
                {
                    string[] productoSpliteado = productoStock.Split(';');
                    if (productoSpliteado.Length != 5) largosplit = true;
                    foreach (var prd in productoSpliteado)
                    {
                        bool a = int.TryParse(prd.ToString(), out int prdSpliteado);
                        if (a == false) noparseable = true;
                    }
                    int.TryParse(productoSpliteado[0].ToString(), out int IDParseado);
                    IDDuplicado.Add(IDParseado);
                }
                var BuscarDuplicados = IDDuplicado.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                if (BuscarDuplicados.Count > 0)
                {
                    IDDuplicados = true;
                }
                else
                {
                    IDReposicion = IDDuplicado;
                }
            }

            if (existe == false)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial no existe." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == true)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial está vacío. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == true)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial " +
                                $"debe contener diez líneas, cada una indicando un producto. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == true)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial " +
                                $"no contiene el delimitador de datos correcto ';' en alguna de sus líneas, o bien contiene líneas vacías. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == true)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial " +
                                $"no contiene datos correctos. Debe contener cinco datos separados por ';' por línea. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && noparseable == true)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial " +
                                $"no contiene datos correctos. Cada línea debe contener números enteros separados por el delimitador ';'. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && noparseable == false && IDDuplicados == true)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial " +
                                $"contiene líneas con ID de producto duplicado. Revise el archivo." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (existe == true && vacio == false && lineas == false && delimitador == false && largosplit == false && noparseable == false && IDDuplicados == false) // es válido
            {
                RefrescarStock();
                RefrescarPedidosPendientesIndustriasyEstado();
            }
        }

        private void RevisarIDs()
        {
            if (!IDStock.SequenceEqual(IDReposicion))
            {
                MessageBox.Show($"Los archivos 'Stock.txt' y 'CantidadesReposicionStock.txt' no coinciden en los IDs de producto o en el orden en que los describen. Revise los archivos." +
                                $"\n\nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
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