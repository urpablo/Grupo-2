using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AppComercio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // borrar todo rastro de archivos de ejecuciones pasadas
            // no hace falta el test de existencia previo dado que si el archivo no existe, no tira excepción
            LimpiezaArchivos();

            // Crear archivos varios necesarios
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("EntregasStockIndustrias.txt")) ;
            using (System.IO.StreamWriter filearepo = new System.IO.StreamWriter("PedidosAEnviar.txt")) ;
            File.WriteAllText("PedidosPendientes.txt", String.Empty);

            // borrar la salida de la ejecución anterior y recrear el directorio de salida si no existe
            if (Directory.Exists(@"C:\Grupo2"))
            {
                Directory.Delete(@"C:\Grupo2", true);
                Directory.CreateDirectory(@"C:\Grupo2");
            }
            else
            {
                Directory.CreateDirectory(@"C:\Grupo2");
            }

            //seteo de columnas para las tablas de datos
            tablaStock.Columns.Add("ID", typeof(int));
            tablaStock.Columns.Add("Real", typeof(int));
            tablaStock.Columns.Add("Punto de Reposición", typeof(int));
            tablaStock.Columns.Add("Comprometido", typeof(int));
            tablaStock.Columns.Add("Pendientes", typeof(int));
            tablaCantARep.Columns.Add("ID", typeof(int));
            tablaCantARep.Columns.Add("Cantidad reposición", typeof(int));
            tablaEntregas.Columns.Add("ID", typeof(int));
            tablaEntregas.Columns.Add("Cantidad a Reponer", typeof(int));
            tablaEntregas.Columns.Add("Recepción", typeof(bool));
            tablaEntregas.PrimaryKey = new System.Data.DataColumn[] { tablaEntregas.Columns["ID"] };
            tablaEntregados.Columns.Add("Código de referencia", typeof(string));
            tablaEntregados.Columns.Add("Entregado", typeof(bool));
            tablaNoEntregados.Columns.Add("Código de referencia", typeof(string));
            tablaNoEntregados.Columns.Add("Entregado", typeof(bool));

            // asocio tablas a dgws y al combobox de ID de producto
            dgwStock.DataSource = tablaStock;
            comboBoxCodProducto.DataSource = tablaStock;
            comboBoxCodProducto.DisplayMember = "ID";
            dgwEntregasFabrica.DataSource = tablaEntregas;
            dgwCantidadesAReponer.DataSource = tablaCantARep;
            dgwEntregados.DataSource = tablaEntregados;
            dgwNoEntregados.DataSource = tablaNoEntregados;

            // últimos ajustes iniciales de interfaz
            panelBienvenido.Location = new Point(186, 38);
            panelStock.Location = new Point(186, 38);
            panelPedidoIndustrias.Location = new Point(186, 38);
            panelEnviosClientesOnline.Location = new Point(186, 38);
            panelVentasOnline.Location = new Point(186, 38);
            panelReportesEntrega.Location = new Point(186, 38);

            btnAgregarItemPedido.Enabled = false;
            btnGenerarTXTLote.Enabled = false;
            btnConfirmarPedidoVentas.Enabled = false;
            btnGenerarTXTPedidoStockIndustrias.Enabled = false;
            btnCargarStockNoEntregados.Enabled = false;

            dgwStock.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgwStock.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgwEntregasFabrica.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgwCantidadesAReponer.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        // ------------------ carga del formulario -----------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            // hacer ventana borderless movible
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanelLeft_MouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(LabelTitulo_MouseMove);

            // cargar los tres archivos escenciales y validar IDs
            CargarDatosComercio();
            CargarStockInicial();
            CargarCantidadesAReponer();
            RevisarIDs();

            // iniciar interfaz en la bienvenida
            botonBotonera = 0;
            ActualizarPantalla();

            // borrar la salida de la ejecución anterior y recrear el directorio de salida si no existe
            // Intento nuevamente por si el usuario tenía la carpeta abierta en el explorador impidiendo su creación
            // y un fallo posterior al no tener carpeta de salida
            if (Directory.Exists(@"C:\Grupo2"))
            {
                Directory.Delete(@"C:\Grupo2", true);
                Directory.CreateDirectory(@"C:\Grupo2");
            }
            else
            {
                Directory.CreateDirectory(@"C:\Grupo2");
            }
        }

        // ------------------ hacer que una ventana borderless sea movible ------------------

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void TopPanelLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void LabelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // ------------------ controles ventana ------------------
        private void pbSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultadoMSGbox = MessageBox.Show("¿Desea realmente salir?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
            if (resultadoMSGbox == DialogResult.Yes)
            {
                LimpiezaArchivos();
                Application.Exit();
            }
        }

        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // ------------------ botonera menú ------------------

        public int botonBotonera;

        private void btnStock_Click(object sender, EventArgs e)
        {
            botonBotonera = 1;
            ActualizarPantalla();
        }

        private void btnPedidoIndustrias_Click(object sender, EventArgs e)
        {
            botonBotonera = 2;
            ActualizarPantalla();
        }

        private void btnEnviarPedido_Click(object sender, EventArgs e)
        {
            botonBotonera = 3;
            ActualizarPantalla();
        }

        private void btnRecibirPedidoOnline_Click(object sender, EventArgs e)
        {
            botonBotonera = 4;
            ActualizarPantalla();
        }

        private void btnAcuseRecibo_Click(object sender, EventArgs e)
        {
            botonBotonera = 5;
            ActualizarPantalla();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            botonBotonera = 1;
            ActualizarPantalla();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            botonBotonera = 2;
            ActualizarPantalla();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            botonBotonera = 3;
            ActualizarPantalla();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            botonBotonera = 4;
            ActualizarPantalla();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            botonBotonera = 5;
            ActualizarPantalla();
        }

        private void ActualizarPantalla()
        {
            switch (botonBotonera)
            {
                case 1:
                    btnControlStock.BackColor = Color.FromArgb(52, 78, 103);
                    btnPedidoStockIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarLoteLogistica.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirVentasOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnReportesEntrega.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Text = "Control de stock";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Aquí podemos ver la situación de stock actual y confirmar la entrada de los pedidos de stock, previamente habiendo hecho uno." +
                                      "\n\nAtención:" +
                                      "\nSi ve un producto cuyo stock real está marcado en negrita, esto indica stock bajo. Haga el encargo a industrias." +
                                      "\n\nRecuerde:" +
                                      "\nLas cantidades de reposición por stock bajo pueden ser modificadas." +
                                      "\nDebe recepcionar las entregas de stock de industrias antes de poder contar con dicho stock.";

                    panelBienvenido.Visible = false;
                    panelStock.Visible = true;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelReportesEntrega.Visible = false;

                    RefrescarStock();
                    RefrescarPedidosPendientesIndustriasyEstado();

                    dgwCantidadesAReponer.ReadOnly = false;
                    dgwCantidadesAReponer.Columns["ID"].ReadOnly = true;
                    dgwCantidadesAReponer.Columns["Cantidad reposición"].ReadOnly = false;
                    ((DataGridViewTextBoxColumn)dgwCantidadesAReponer.Columns["Cantidad reposición"]).MaxInputLength = 5;

                    dgwEntregasFabrica.ReadOnly = false;
                    dgwEntregasFabrica.Columns["ID"].ReadOnly = true;
                    dgwEntregasFabrica.Columns["Cantidad a Reponer"].ReadOnly = true;

                    break;

                case 2:
                    btnControlStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoStockIndustrias.BackColor = Color.FromArgb(52, 78, 103);
                    btnEnviarLoteLogistica.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirVentasOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnReportesEntrega.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Text = "Pedido de stock a industrias";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Puede hacer el pedido por stock bajo a industrias desde aquí." +
                        "\n \nRecuerde luego recibir el stock nuevo y cargarlo al sistema." +
                        "\n \nRecuerde que sólo se puede hacer un solo pedido por día." +
                        "\n \nEl historial de pedidos realizados está para su comodidad." +
                        "\n \nLos archivos diarios se encuentran en la carpeta de salida del programa.";

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = true;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelReportesEntrega.Visible = false;

                    RefrescarStock();
                    RefrescarPedidosPendientesIndustriasyEstado();

                    break;

                case 3:
                    btnControlStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoStockIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarLoteLogistica.BackColor = Color.FromArgb(52, 78, 103);
                    btnRecibirVentasOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnReportesEntrega.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Text = "Envíos a logística";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "En cuanto cargue las ventas del día, despache el lote a logística." +
                        "\n \nRecuerde que se hace un sólo envío diario." +
                        "\n \nEl historial de lotes enviados a logística está para su comodidad." +
                        "\n \nLos archivos diarios se encuentran en la carpeta de salida del programa.";

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = true;
                    panelVentasOnline.Visible = false;
                    panelReportesEntrega.Visible = false;

                    RefrescarPedidosPendientesIndustriasyEstado();
                    break;

                case 4:
                    btnControlStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoStockIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarLoteLogistica.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirVentasOnline.BackColor = Color.FromArgb(52, 78, 103);
                    btnReportesEntrega.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Text = "Ingresar pedidos de ventas online";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Pasos para ingresar un pedido:" +
                        "\n \n1) Ingrese los datos del cliente." +
                        "\n \n2) Ingrese código de producto y cantidad, y agregue este producto al pedido." +
                        "\n \n3) Ingrese nuevamente hasta completar el pedido del cliente para la venta hecha." +
                        "\n \n4) Clic en 'Confirmar Pedido' para ingresarlo al lote diario.";

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = true;
                    panelReportesEntrega.Visible = false;
                    break;

                case 5:
                    btnControlStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoStockIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarLoteLogistica.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirVentasOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnReportesEntrega.BackColor = Color.FromArgb(52, 78, 103);

                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "Reportes de entrega";
                    labelAyuda.MaximumSize = new Size(140, 0);
                    labelAyuda.AutoSize = true;
                    labelAyuda.Text = "Pasos para leer y procesar el reporte de entregas:" +
                        "\n \n1) Haga clic en 'Leer Reporte...' y elija el archivo." +
                        "\n \n2) Se visualizará el reporte. Luego haga clic en 'Cargar Stock...'" +
                        "\n \n3) Los pedidos que no hayan sido entregados habrán sido reingresados al stock.";

                    panelBienvenido.Visible = false;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelReportesEntrega.Visible = true;
                    break;

                case 0:
                    btnControlStock.BackColor = Color.FromArgb(41, 57, 128);
                    btnPedidoStockIndustrias.BackColor = Color.FromArgb(41, 57, 128);
                    btnEnviarLoteLogistica.BackColor = Color.FromArgb(41, 57, 71);
                    btnRecibirVentasOnline.BackColor = Color.FromArgb(41, 57, 71);
                    btnReportesEntrega.BackColor = Color.FromArgb(41, 47, 71);

                    LabelTitulo.Visible = true;
                    LabelTitulo.Text = "CAI - Comercio";
                    labelAyuda.Text = "CAI - Grupo 2";

                    panelBienvenido.Visible = true;
                    panelStock.Visible = false;
                    panelPedidoIndustrias.Visible = false;
                    panelEnviosClientesOnline.Visible = false;
                    panelVentasOnline.Visible = false;
                    panelReportesEntrega.Visible = false;
                    break;
            }
        }

        private void LimpiezaArchivos()
        {
            File.Delete("PedidoTemporal.txt");
            File.Delete("PedidosAEnviar.txt");
            File.Delete("Pedidos.txt");
            File.Delete("Listadereferencias.txt");
            File.Delete("lineaindividual.txt");
            File.Delete("EntregasStockIndustrias.txt");
            File.Delete("PedidosPendientes.txt");
        }

        // ------------------ QoL: combobox selección -> foco a textbox cantidad -> enter. 2 clicks menos
        private void comboBoxCodProducto_TextChanged(object sender, EventArgs e)
        {
            textBoxCantidadItem.Focus();
        }

        private void textBoxCantidadItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAgregarItemPedido.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void textBoxCantidadItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}