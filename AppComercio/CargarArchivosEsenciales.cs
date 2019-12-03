using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading;
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

            // primero reviso si el archivo no está vacío
            if (new FileInfo(@"DatosComercio.txt").Length == 0)
            {
                MessageBox.Show($"El archivo 'DatosComercio.txt' que contiene los datos del comercio está vacío. \n \n" +
                                "No se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                // dado que no está vacío me quedo con la primera y única linea del archivo
                string lineaReporte = File.ReadLines(@"DatosComercio.txt").First();

                // reviso si la línea contiene al menos un delimitador de split
                if (lineaReporte.Contains(';'))
                {
                    string[] lineaSpliteada = lineaReporte.Split(';');
                    // Reviso la cantidad de líneas del archivo. Debe tener solo una.
                    if (File.ReadLines(@"DatosComercio.txt").Count() > 1)
                    {
                        lineas = true;
                    }

                    // Reviso la cantidad de elementos resultantes del split. Deben ser 5.
                    // Indirectamente es un chequeo por los separadores ; del split
                    if (lineaSpliteada.Length != 5)
                    {
                        largosplit = true;
                    }

                    // Reviso si el código de comercio comienza con C y es un número, es válido
                    if (lineaSpliteada[0].Substring(0, 1) != "C" && !int.TryParse(lineaSpliteada[0].Substring(1), out int codComercio))
                    {
                        comercio = true;

                    }

                    // si el cuit es parseable y tiene un largo de 11, es válido para nuestros propósitos
                    if (!long.TryParse(lineaSpliteada[2].ToString(), out long cuitParseado))
                    {
                        // si no lo pudo parsear, vamos mal
                        cuit = true;
                    }
                    else
                    {
                        // si lo pudo parsear, reviso el largo del número
                        if (Digits_IfChain(cuitParseado) == 11)
                        {
                            cuit = false;
                        }
                        else
                        {
                            cuit = true;
                        }
                    }

                    // si alguno es verdadero, cerrar el programa
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
                // como no tenía ni un delimitador ; el archivo no sirve
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

            // Primero reviso si el archivo está vacío
            if (new FileInfo(@"CantidadesReposicionStock.txt").Length == 0)
            {
                MessageBox.Show($"El archivo 'CantidadesReposicionStock.txt' que contiene las cantidades de reposición por producto por stock bajo" +
                                $" está vacío. \n \nNo se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                // Dado que no está vacío,
                // Reviso la cantidad de líneas del archivo. Debe tener diez al igual que el de stock.
                if (File.ReadLines(@"CantidadesReposicionStock.txt").Count() != 10)
                {
                    lineas = true;
                }

                // Reviso la cantidad de elementos resultantes del split. Deben ser 2
                // trato de parsear ambos
                foreach (var producto in LineasAReponer)
                {
                    // pero primero veo si no me modificaron el separador para el split
                    if (producto.Contains(';'))
                    {
                        var productoSpliteado = producto.Split(';');
                        if (productoSpliteado.Length != 2)
                        {
                            largosplit = true;
                        }

                        bool a = int.TryParse(productoSpliteado[0].ToString(), out int IDSpliteado);
                        bool b = int.TryParse(productoSpliteado[1].ToString(), out int cantSpliteada);
                        IDDuplicado.Add(IDSpliteado);

                        if (a == false || b == false)
                        {
                            noparseable = true;
                        }
                    }
                    else
                    {
                        splitmalvado = true;
                    }

                }

                // ahora busco IDs duplicados
                var BuscarDuplicados = IDDuplicado.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                if (BuscarDuplicados.Count > 0)
                {
                    IDDuplicados = true;
                }


                // si alguno de los 5 es verdadero, el archivo fue alterado y no sirve, se cierra el programa
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

                    lineas = false;
                    largosplit = false;
                    noparseable = false;
                    splitmalvado = false;
                    IDDuplicados = false;
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

            // Primero, reviso si el archivo está vacío
            if (new FileInfo(@"Stock.txt").Length == 0)
            {
                MessageBox.Show($"El archivo 'Stock.txt' que contiene el stock inicial está vacío. \n \n" +
                                "No se puede continuar. El programa se cerrará.", "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                // Si no está vacío, me fijo la cantidad de líneas que tiene. Deben ser 10
                if (File.ReadLines(@"CantidadesReposicionStock.txt").Count() != 10)
                {
                    lineas = true;
                }
            }

            // Dado que no esta vacío quiero intentar splitear y parsear todas las líneas
            foreach (var producto in LineasStock)
            {
                if(producto.Contains(';'))  // Al menos tengo un delimitador
                {
                    var productoSpliteado = producto.Split(';');

                    if(productoSpliteado.Count() != 5)
                    {
                        largosplit = true;
                    }

                    foreach(var item in productoSpliteado)
                    {
                        if(!int.TryParse(item.ToString(), out int itemParseado))
                        {
                            noparseable = true;
                        }
                    }

                    if (noparseable == false)
                    {
                        int.TryParse(productoSpliteado[0].ToString(), out int IDParseado);
                        IDDuplicado.Add(IDParseado);
                    }
                }
                else
                {
                    nosplit = true;
                }
            }


            // ahora busco IDs duplicados como último check
            var BuscarDuplicados = IDDuplicado.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            if (BuscarDuplicados.Count > 0)
            {
                IDDuplicados = true;
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
    }
}