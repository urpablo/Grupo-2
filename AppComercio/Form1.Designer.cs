namespace AppComercio
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.LabelTitulo = new System.Windows.Forms.Label();
            this.TopPanelLeft = new System.Windows.Forms.Panel();
            this.Wrapper = new System.Windows.Forms.PictureBox();
            this.Minimizar = new System.Windows.Forms.PictureBox();
            this.Salir = new System.Windows.Forms.PictureBox();
            this.Sidebar = new System.Windows.Forms.Panel();
            this.panelAyuda = new System.Windows.Forms.Panel();
            this.labelAyuda = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RecibirPedido = new System.Windows.Forms.Button();
            this.EnviarPedido = new System.Windows.Forms.Button();
            this.RealizarPedido = new System.Windows.Forms.Button();
            this.Stock = new System.Windows.Forms.Button();
            this.BotonEjecutarLote1 = new System.Windows.Forms.Button();
            this.botonProdAEnviar = new System.Windows.Forms.Button();
            this.TablaStock = new System.Windows.Forms.DataGridView();
            this.TablaReaPed = new System.Windows.Forms.DataGridView();
            this.TablaEnvPed = new System.Windows.Forms.DataGridView();
            this.TablaRecPed = new System.Windows.Forms.DataGridView();
            this.botonLevantarPedido = new System.Windows.Forms.Button();
            this.botonEjecutarLote2 = new System.Windows.Forms.Button();
            this.botonActualizarPedidos = new System.Windows.Forms.Button();
            this.botonCargarStock = new System.Windows.Forms.Button();
            this.labelBienvenido1 = new System.Windows.Forms.Label();
            this.labelBienvenido2 = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            this.TopPanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Wrapper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salir)).BeginInit();
            this.Sidebar.SuspendLayout();
            this.panelAyuda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaReaPed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaEnvPed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaRecPed)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(224)))), ((int)(((byte)(227)))));
            this.TopPanel.Controls.Add(this.LabelTitulo);
            this.TopPanel.Controls.Add(this.TopPanelLeft);
            this.TopPanel.Controls.Add(this.Minimizar);
            this.TopPanel.Controls.Add(this.Salir);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(720, 32);
            this.TopPanel.TabIndex = 1;
            // 
            // LabelTitulo
            // 
            this.LabelTitulo.AutoSize = true;
            this.LabelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitulo.Location = new System.Drawing.Point(190, 3);
            this.LabelTitulo.Name = "LabelTitulo";
            this.LabelTitulo.Size = new System.Drawing.Size(338, 25);
            this.LabelTitulo.TabIndex = 7;
            this.LabelTitulo.Text = "Confeccionar pedidos a industrias";
            this.LabelTitulo.Visible = false;
            // 
            // TopPanelLeft
            // 
            this.TopPanelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(247)))));
            this.TopPanelLeft.Controls.Add(this.Wrapper);
            this.TopPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.TopPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.TopPanelLeft.Name = "TopPanelLeft";
            this.TopPanelLeft.Size = new System.Drawing.Size(180, 32);
            this.TopPanelLeft.TabIndex = 2;
            // 
            // Wrapper
            // 
            this.Wrapper.Image = ((System.Drawing.Image)(resources.GetObject("Wrapper.Image")));
            this.Wrapper.Location = new System.Drawing.Point(6, 1);
            this.Wrapper.Name = "Wrapper";
            this.Wrapper.Size = new System.Drawing.Size(35, 31);
            this.Wrapper.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Wrapper.TabIndex = 5;
            this.Wrapper.TabStop = false;
            this.Wrapper.Click += new System.EventHandler(this.Wrapper_Click);
            // 
            // Minimizar
            // 
            this.Minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Minimizar.Image = ((System.Drawing.Image)(resources.GetObject("Minimizar.Image")));
            this.Minimizar.Location = new System.Drawing.Point(670, 3);
            this.Minimizar.Name = "Minimizar";
            this.Minimizar.Size = new System.Drawing.Size(20, 20);
            this.Minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Minimizar.TabIndex = 2;
            this.Minimizar.TabStop = false;
            this.Minimizar.Click += new System.EventHandler(this.Minimizar_Click);
            // 
            // Salir
            // 
            this.Salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Salir.BackColor = System.Drawing.Color.Red;
            this.Salir.Image = ((System.Drawing.Image)(resources.GetObject("Salir.Image")));
            this.Salir.Location = new System.Drawing.Point(697, 3);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(20, 20);
            this.Salir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Salir.TabIndex = 0;
            this.Salir.TabStop = false;
            this.Salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // Sidebar
            // 
            this.Sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(206)))));
            this.Sidebar.Controls.Add(this.panelAyuda);
            this.Sidebar.Controls.Add(this.pictureBox4);
            this.Sidebar.Controls.Add(this.pictureBox3);
            this.Sidebar.Controls.Add(this.pictureBox2);
            this.Sidebar.Controls.Add(this.pictureBox1);
            this.Sidebar.Controls.Add(this.RecibirPedido);
            this.Sidebar.Controls.Add(this.EnviarPedido);
            this.Sidebar.Controls.Add(this.RealizarPedido);
            this.Sidebar.Controls.Add(this.Stock);
            this.Sidebar.Location = new System.Drawing.Point(0, 32);
            this.Sidebar.Name = "Sidebar";
            this.Sidebar.Size = new System.Drawing.Size(180, 520);
            this.Sidebar.TabIndex = 2;
            // 
            // panelAyuda
            // 
            this.panelAyuda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(170)))), ((int)(((byte)(227)))));
            this.panelAyuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAyuda.Controls.Add(this.labelAyuda);
            this.panelAyuda.Location = new System.Drawing.Point(9, 187);
            this.panelAyuda.Name = "panelAyuda";
            this.panelAyuda.Size = new System.Drawing.Size(160, 320);
            this.panelAyuda.TabIndex = 16;
            // 
            // labelAyuda
            // 
            this.labelAyuda.AutoEllipsis = true;
            this.labelAyuda.AutoSize = true;
            this.labelAyuda.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAyuda.ForeColor = System.Drawing.SystemColors.Control;
            this.labelAyuda.Location = new System.Drawing.Point(10, 10);
            this.labelAyuda.Name = "labelAyuda";
            this.labelAyuda.Size = new System.Drawing.Size(47, 16);
            this.labelAyuda.TabIndex = 0;
            this.labelAyuda.Text = "Ayuda";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(0, 132);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(47, 44);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 15;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 88);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(47, 44);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 44);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(47, 44);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // RecibirPedido
            // 
            this.RecibirPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.RecibirPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.RecibirPedido.FlatAppearance.BorderSize = 0;
            this.RecibirPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RecibirPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecibirPedido.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecibirPedido.ForeColor = System.Drawing.Color.White;
            this.RecibirPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RecibirPedido.Location = new System.Drawing.Point(0, 132);
            this.RecibirPedido.Name = "RecibirPedido";
            this.RecibirPedido.Size = new System.Drawing.Size(180, 44);
            this.RecibirPedido.TabIndex = 12;
            this.RecibirPedido.Text = "Recibir Pedido";
            this.RecibirPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RecibirPedido.UseVisualStyleBackColor = false;
            this.RecibirPedido.Click += new System.EventHandler(this.RecibirPedido_Click);
            // 
            // EnviarPedido
            // 
            this.EnviarPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.EnviarPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.EnviarPedido.FlatAppearance.BorderSize = 0;
            this.EnviarPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.EnviarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnviarPedido.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarPedido.ForeColor = System.Drawing.Color.White;
            this.EnviarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EnviarPedido.Location = new System.Drawing.Point(0, 88);
            this.EnviarPedido.Name = "EnviarPedido";
            this.EnviarPedido.Size = new System.Drawing.Size(180, 44);
            this.EnviarPedido.TabIndex = 11;
            this.EnviarPedido.Text = "Enviar Pedido";
            this.EnviarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EnviarPedido.UseVisualStyleBackColor = false;
            this.EnviarPedido.Click += new System.EventHandler(this.EnviarPedido_Click);
            // 
            // RealizarPedido
            // 
            this.RealizarPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.RealizarPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.RealizarPedido.FlatAppearance.BorderSize = 0;
            this.RealizarPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RealizarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RealizarPedido.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealizarPedido.ForeColor = System.Drawing.Color.White;
            this.RealizarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RealizarPedido.Location = new System.Drawing.Point(0, 44);
            this.RealizarPedido.Name = "RealizarPedido";
            this.RealizarPedido.Size = new System.Drawing.Size(180, 44);
            this.RealizarPedido.TabIndex = 10;
            this.RealizarPedido.Text = "Realizar Pedido";
            this.RealizarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RealizarPedido.UseVisualStyleBackColor = false;
            this.RealizarPedido.Click += new System.EventHandler(this.RealizarPedido_Click);
            // 
            // Stock
            // 
            this.Stock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.Stock.Dock = System.Windows.Forms.DockStyle.Top;
            this.Stock.FlatAppearance.BorderSize = 0;
            this.Stock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.Stock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stock.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stock.ForeColor = System.Drawing.Color.White;
            this.Stock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Stock.Location = new System.Drawing.Point(0, 0);
            this.Stock.Name = "Stock";
            this.Stock.Size = new System.Drawing.Size(180, 44);
            this.Stock.TabIndex = 9;
            this.Stock.Text = "Stock actual";
            this.Stock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Stock.UseVisualStyleBackColor = false;
            this.Stock.Click += new System.EventHandler(this.Stock_Click);
            // 
            // BotonEjecutarLote1
            // 
            this.BotonEjecutarLote1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonEjecutarLote1.Location = new System.Drawing.Point(572, 506);
            this.BotonEjecutarLote1.Name = "BotonEjecutarLote1";
            this.BotonEjecutarLote1.Size = new System.Drawing.Size(128, 32);
            this.BotonEjecutarLote1.TabIndex = 4;
            this.BotonEjecutarLote1.Text = "Ejecutar lote";
            this.BotonEjecutarLote1.UseVisualStyleBackColor = true;
            // 
            // botonProdAEnviar
            // 
            this.botonProdAEnviar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonProdAEnviar.Location = new System.Drawing.Point(392, 506);
            this.botonProdAEnviar.Name = "botonProdAEnviar";
            this.botonProdAEnviar.Size = new System.Drawing.Size(164, 32);
            this.botonProdAEnviar.TabIndex = 5;
            this.botonProdAEnviar.Text = "Ver productos a enviar";
            this.botonProdAEnviar.UseVisualStyleBackColor = true;
            // 
            // TablaStock
            // 
            this.TablaStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaStock.Location = new System.Drawing.Point(200, 55);
            this.TablaStock.Name = "TablaStock";
            this.TablaStock.Size = new System.Drawing.Size(500, 430);
            this.TablaStock.TabIndex = 3;
            // 
            // TablaReaPed
            // 
            this.TablaReaPed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaReaPed.Location = new System.Drawing.Point(200, 55);
            this.TablaReaPed.Name = "TablaReaPed";
            this.TablaReaPed.Size = new System.Drawing.Size(500, 430);
            this.TablaReaPed.TabIndex = 6;
            // 
            // TablaEnvPed
            // 
            this.TablaEnvPed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaEnvPed.Location = new System.Drawing.Point(200, 55);
            this.TablaEnvPed.Name = "TablaEnvPed";
            this.TablaEnvPed.Size = new System.Drawing.Size(500, 430);
            this.TablaEnvPed.TabIndex = 7;
            // 
            // TablaRecPed
            // 
            this.TablaRecPed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaRecPed.Location = new System.Drawing.Point(200, 55);
            this.TablaRecPed.Name = "TablaRecPed";
            this.TablaRecPed.Size = new System.Drawing.Size(500, 430);
            this.TablaRecPed.TabIndex = 8;
            // 
            // botonLevantarPedido
            // 
            this.botonLevantarPedido.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonLevantarPedido.Location = new System.Drawing.Point(412, 506);
            this.botonLevantarPedido.Name = "botonLevantarPedido";
            this.botonLevantarPedido.Size = new System.Drawing.Size(128, 32);
            this.botonLevantarPedido.TabIndex = 9;
            this.botonLevantarPedido.Text = "Levantar pedido";
            this.botonLevantarPedido.UseVisualStyleBackColor = true;
            // 
            // botonEjecutarLote2
            // 
            this.botonEjecutarLote2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonEjecutarLote2.Location = new System.Drawing.Point(572, 506);
            this.botonEjecutarLote2.Name = "botonEjecutarLote2";
            this.botonEjecutarLote2.Size = new System.Drawing.Size(128, 32);
            this.botonEjecutarLote2.TabIndex = 10;
            this.botonEjecutarLote2.Text = "Ejecutar lote";
            this.botonEjecutarLote2.UseVisualStyleBackColor = true;
            // 
            // botonActualizarPedidos
            // 
            this.botonActualizarPedidos.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonActualizarPedidos.Location = new System.Drawing.Point(412, 506);
            this.botonActualizarPedidos.Name = "botonActualizarPedidos";
            this.botonActualizarPedidos.Size = new System.Drawing.Size(128, 32);
            this.botonActualizarPedidos.TabIndex = 11;
            this.botonActualizarPedidos.Text = "Actualizar pedidos";
            this.botonActualizarPedidos.UseVisualStyleBackColor = true;
            // 
            // botonCargarStock
            // 
            this.botonCargarStock.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonCargarStock.Location = new System.Drawing.Point(572, 506);
            this.botonCargarStock.Name = "botonCargarStock";
            this.botonCargarStock.Size = new System.Drawing.Size(128, 32);
            this.botonCargarStock.TabIndex = 12;
            this.botonCargarStock.Text = "Cargar a stock";
            this.botonCargarStock.UseVisualStyleBackColor = true;
            // 
            // labelBienvenido1
            // 
            this.labelBienvenido1.AutoSize = true;
            this.labelBienvenido1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBienvenido1.Location = new System.Drawing.Point(371, 211);
            this.labelBienvenido1.Name = "labelBienvenido1";
            this.labelBienvenido1.Size = new System.Drawing.Size(164, 32);
            this.labelBienvenido1.TabIndex = 13;
            this.labelBienvenido1.Text = "¡Bienvenido!";
            // 
            // labelBienvenido2
            // 
            this.labelBienvenido2.AutoSize = true;
            this.labelBienvenido2.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBienvenido2.Location = new System.Drawing.Point(213, 287);
            this.labelBienvenido2.Name = "labelBienvenido2";
            this.labelBienvenido2.Size = new System.Drawing.Size(477, 32);
            this.labelBienvenido2.TabIndex = 14;
            this.labelBienvenido2.Text = "Seleccione una opción para comenzar";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 550);
            this.Controls.Add(this.labelBienvenido2);
            this.Controls.Add(this.labelBienvenido1);
            this.Controls.Add(this.botonCargarStock);
            this.Controls.Add(this.botonActualizarPedidos);
            this.Controls.Add(this.botonEjecutarLote2);
            this.Controls.Add(this.botonLevantarPedido);
            this.Controls.Add(this.TablaRecPed);
            this.Controls.Add(this.TablaEnvPed);
            this.Controls.Add(this.TablaReaPed);
            this.Controls.Add(this.botonProdAEnviar);
            this.Controls.Add(this.BotonEjecutarLote1);
            this.Controls.Add(this.TablaStock);
            this.Controls.Add(this.Sidebar);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CAI - Comercio";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.TopPanelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Wrapper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salir)).EndInit();
            this.Sidebar.ResumeLayout(false);
            this.panelAyuda.ResumeLayout(false);
            this.panelAyuda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaReaPed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaEnvPed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaRecPed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.PictureBox Minimizar;
        private System.Windows.Forms.PictureBox Salir;
        private System.Windows.Forms.Panel TopPanelLeft;
        private System.Windows.Forms.Panel Sidebar;
        private System.Windows.Forms.PictureBox Wrapper;
        private System.Windows.Forms.Button Stock;
        private System.Windows.Forms.Button RecibirPedido;
        private System.Windows.Forms.Button EnviarPedido;
        private System.Windows.Forms.Button RealizarPedido;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label LabelTitulo;
        private System.Windows.Forms.Button BotonEjecutarLote1;
        private System.Windows.Forms.Button botonProdAEnviar;
        private System.Windows.Forms.DataGridView TablaStock;
        private System.Windows.Forms.DataGridView TablaReaPed;
        private System.Windows.Forms.DataGridView TablaEnvPed;
        private System.Windows.Forms.DataGridView TablaRecPed;
        private System.Windows.Forms.Button botonLevantarPedido;
        private System.Windows.Forms.Button botonEjecutarLote2;
        private System.Windows.Forms.Button botonActualizarPedidos;
        private System.Windows.Forms.Button botonCargarStock;
        private System.Windows.Forms.Label labelBienvenido1;
        private System.Windows.Forms.Label labelBienvenido2;
        private System.Windows.Forms.Panel panelAyuda;
        private System.Windows.Forms.Label labelAyuda;
    }
}

