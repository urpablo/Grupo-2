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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.Restaurar = new System.Windows.Forms.PictureBox();
            this.Maximizar = new System.Windows.Forms.PictureBox();
            this.TopPanelLeft = new System.Windows.Forms.Panel();
            this.Wrapper = new System.Windows.Forms.PictureBox();
            this.Minimizar = new System.Windows.Forms.PictureBox();
            this.Salir = new System.Windows.Forms.PictureBox();
            this.StockLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.Sidebar = new System.Windows.Forms.Panel();
            this.RecibirPedido = new Bunifu.Framework.UI.BunifuFlatButton();
            this.EnviarPedido = new Bunifu.Framework.UI.BunifuFlatButton();
            this.RealizarPedido = new Bunifu.Framework.UI.BunifuFlatButton();
            this.Stock = new Bunifu.Framework.UI.BunifuFlatButton();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.RecibirPedidosLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.RealizarPedidoLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.EnviarPedidoLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Restaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximizar)).BeginInit();
            this.TopPanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Wrapper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salir)).BeginInit();
            this.Sidebar.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(224)))), ((int)(((byte)(227)))));
            this.TopPanel.Controls.Add(this.Restaurar);
            this.TopPanel.Controls.Add(this.Maximizar);
            this.TopPanel.Controls.Add(this.TopPanelLeft);
            this.TopPanel.Controls.Add(this.Minimizar);
            this.TopPanel.Controls.Add(this.Salir);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(695, 32);
            this.TopPanel.TabIndex = 1;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
            // 
            // Restaurar
            // 
            this.Restaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Restaurar.Image = ((System.Drawing.Image)(resources.GetObject("Restaurar.Image")));
            this.Restaurar.Location = new System.Drawing.Point(650, 3);
            this.Restaurar.Name = "Restaurar";
            this.Restaurar.Size = new System.Drawing.Size(20, 20);
            this.Restaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Restaurar.TabIndex = 4;
            this.Restaurar.TabStop = false;
            this.Restaurar.Visible = false;
            this.Restaurar.Click += new System.EventHandler(this.Restaurar_Click);
            // 
            // Maximizar
            // 
            this.Maximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Maximizar.Image = ((System.Drawing.Image)(resources.GetObject("Maximizar.Image")));
            this.Maximizar.Location = new System.Drawing.Point(650, 3);
            this.Maximizar.Name = "Maximizar";
            this.Maximizar.Size = new System.Drawing.Size(20, 20);
            this.Maximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Maximizar.TabIndex = 3;
            this.Maximizar.TabStop = false;
            this.Maximizar.Click += new System.EventHandler(this.Maximizar_Click);
            // 
            // TopPanelLeft
            // 
            this.TopPanelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(247)))));
            this.TopPanelLeft.Controls.Add(this.Wrapper);
            this.TopPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.TopPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.TopPanelLeft.Name = "TopPanelLeft";
            this.TopPanelLeft.Size = new System.Drawing.Size(157, 32);
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
            this.Minimizar.Location = new System.Drawing.Point(628, 3);
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
            this.Salir.Image = ((System.Drawing.Image)(resources.GetObject("Salir.Image")));
            this.Salir.Location = new System.Drawing.Point(672, 3);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(20, 20);
            this.Salir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Salir.TabIndex = 0;
            this.Salir.TabStop = false;
            this.Salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // StockLabel
            // 
            this.StockLabel.AutoSize = true;
            this.StockLabel.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StockLabel.Location = new System.Drawing.Point(1, 1);
            this.StockLabel.Name = "StockLabel";
            this.StockLabel.Size = new System.Drawing.Size(166, 22);
            this.StockLabel.TabIndex = 5;
            this.StockLabel.Text = "Control de stock";
            this.StockLabel.Visible = false;
            // 
            // Sidebar
            // 
            this.Sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(206)))));
            this.Sidebar.Controls.Add(this.RecibirPedido);
            this.Sidebar.Controls.Add(this.EnviarPedido);
            this.Sidebar.Controls.Add(this.RealizarPedido);
            this.Sidebar.Controls.Add(this.Stock);
            this.Sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.Sidebar.Location = new System.Drawing.Point(0, 32);
            this.Sidebar.Name = "Sidebar";
            this.Sidebar.Size = new System.Drawing.Size(157, 375);
            this.Sidebar.TabIndex = 2;
            // 
            // RecibirPedido
            // 
            this.RecibirPedido.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RecibirPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.RecibirPedido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RecibirPedido.BorderRadius = 0;
            this.RecibirPedido.ButtonText = "Recibir Pedido";
            this.RecibirPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RecibirPedido.DisabledColor = System.Drawing.Color.White;
            this.RecibirPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.RecibirPedido.Iconcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RecibirPedido.Iconimage = ((System.Drawing.Image)(resources.GetObject("RecibirPedido.Iconimage")));
            this.RecibirPedido.Iconimage_right = null;
            this.RecibirPedido.Iconimage_right_Selected = null;
            this.RecibirPedido.Iconimage_Selected = null;
            this.RecibirPedido.IconMarginLeft = 0;
            this.RecibirPedido.IconMarginRight = 0;
            this.RecibirPedido.IconRightVisible = true;
            this.RecibirPedido.IconRightZoom = 0D;
            this.RecibirPedido.IconVisible = true;
            this.RecibirPedido.IconZoom = 90D;
            this.RecibirPedido.IsTab = false;
            this.RecibirPedido.Location = new System.Drawing.Point(0, 132);
            this.RecibirPedido.Name = "RecibirPedido";
            this.RecibirPedido.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.RecibirPedido.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RecibirPedido.OnHoverTextColor = System.Drawing.Color.White;
            this.RecibirPedido.selected = false;
            this.RecibirPedido.Size = new System.Drawing.Size(157, 44);
            this.RecibirPedido.TabIndex = 4;
            this.RecibirPedido.Text = "Recibir Pedido";
            this.RecibirPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RecibirPedido.Textcolor = System.Drawing.Color.White;
            this.RecibirPedido.TextFont = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecibirPedido.Click += new System.EventHandler(this.RecibirPedido_Click);
            // 
            // EnviarPedido
            // 
            this.EnviarPedido.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.EnviarPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.EnviarPedido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EnviarPedido.BorderRadius = 0;
            this.EnviarPedido.ButtonText = "Enviar Pedido";
            this.EnviarPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EnviarPedido.DisabledColor = System.Drawing.Color.White;
            this.EnviarPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.EnviarPedido.Iconcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.EnviarPedido.Iconimage = ((System.Drawing.Image)(resources.GetObject("EnviarPedido.Iconimage")));
            this.EnviarPedido.Iconimage_right = null;
            this.EnviarPedido.Iconimage_right_Selected = null;
            this.EnviarPedido.Iconimage_Selected = null;
            this.EnviarPedido.IconMarginLeft = 0;
            this.EnviarPedido.IconMarginRight = 0;
            this.EnviarPedido.IconRightVisible = true;
            this.EnviarPedido.IconRightZoom = 0D;
            this.EnviarPedido.IconVisible = true;
            this.EnviarPedido.IconZoom = 90D;
            this.EnviarPedido.IsTab = false;
            this.EnviarPedido.Location = new System.Drawing.Point(0, 88);
            this.EnviarPedido.Name = "EnviarPedido";
            this.EnviarPedido.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.EnviarPedido.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.EnviarPedido.OnHoverTextColor = System.Drawing.Color.White;
            this.EnviarPedido.selected = false;
            this.EnviarPedido.Size = new System.Drawing.Size(157, 44);
            this.EnviarPedido.TabIndex = 3;
            this.EnviarPedido.Text = "Enviar Pedido";
            this.EnviarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EnviarPedido.Textcolor = System.Drawing.Color.White;
            this.EnviarPedido.TextFont = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarPedido.Click += new System.EventHandler(this.EnviarPedido_Click);
            // 
            // RealizarPedido
            // 
            this.RealizarPedido.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RealizarPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.RealizarPedido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RealizarPedido.BorderRadius = 0;
            this.RealizarPedido.ButtonText = "Realizar Pedido";
            this.RealizarPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RealizarPedido.DisabledColor = System.Drawing.Color.White;
            this.RealizarPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.RealizarPedido.Iconcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RealizarPedido.Iconimage = ((System.Drawing.Image)(resources.GetObject("RealizarPedido.Iconimage")));
            this.RealizarPedido.Iconimage_right = null;
            this.RealizarPedido.Iconimage_right_Selected = null;
            this.RealizarPedido.Iconimage_Selected = null;
            this.RealizarPedido.IconMarginLeft = 0;
            this.RealizarPedido.IconMarginRight = 0;
            this.RealizarPedido.IconRightVisible = true;
            this.RealizarPedido.IconRightZoom = 0D;
            this.RealizarPedido.IconVisible = true;
            this.RealizarPedido.IconZoom = 90D;
            this.RealizarPedido.IsTab = false;
            this.RealizarPedido.Location = new System.Drawing.Point(0, 44);
            this.RealizarPedido.Name = "RealizarPedido";
            this.RealizarPedido.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.RealizarPedido.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RealizarPedido.OnHoverTextColor = System.Drawing.Color.White;
            this.RealizarPedido.selected = false;
            this.RealizarPedido.Size = new System.Drawing.Size(157, 44);
            this.RealizarPedido.TabIndex = 2;
            this.RealizarPedido.Text = "Realizar Pedido";
            this.RealizarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RealizarPedido.Textcolor = System.Drawing.Color.White;
            this.RealizarPedido.TextFont = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealizarPedido.Click += new System.EventHandler(this.RealizarPedido_Click);
            // 
            // Stock
            // 
            this.Stock.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.Stock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.Stock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Stock.BorderRadius = 0;
            this.Stock.ButtonText = "Stock";
            this.Stock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Stock.DisabledColor = System.Drawing.Color.White;
            this.Stock.Dock = System.Windows.Forms.DockStyle.Top;
            this.Stock.Iconcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.Stock.Iconimage = ((System.Drawing.Image)(resources.GetObject("Stock.Iconimage")));
            this.Stock.Iconimage_right = null;
            this.Stock.Iconimage_right_Selected = null;
            this.Stock.Iconimage_Selected = null;
            this.Stock.IconMarginLeft = 0;
            this.Stock.IconMarginRight = 0;
            this.Stock.IconRightVisible = true;
            this.Stock.IconRightZoom = 0D;
            this.Stock.IconVisible = true;
            this.Stock.IconZoom = 90D;
            this.Stock.IsTab = false;
            this.Stock.Location = new System.Drawing.Point(0, 0);
            this.Stock.Name = "Stock";
            this.Stock.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.Stock.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.Stock.OnHoverTextColor = System.Drawing.Color.White;
            this.Stock.selected = false;
            this.Stock.Size = new System.Drawing.Size(157, 44);
            this.Stock.TabIndex = 1;
            this.Stock.Text = "Stock";
            this.Stock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Stock.Textcolor = System.Drawing.Color.White;
            this.Stock.TextFont = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stock.Click += new System.EventHandler(this.Stock_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(244)))), ((int)(((byte)(252)))));
            this.MainPanel.Controls.Add(this.RecibirPedidosLabel);
            this.MainPanel.Controls.Add(this.StockLabel);
            this.MainPanel.Controls.Add(this.RealizarPedidoLabel);
            this.MainPanel.Controls.Add(this.EnviarPedidoLabel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(157, 32);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(538, 375);
            this.MainPanel.TabIndex = 3;
            // 
            // RecibirPedidosLabel
            // 
            this.RecibirPedidosLabel.AutoSize = true;
            this.RecibirPedidosLabel.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecibirPedidosLabel.Location = new System.Drawing.Point(2, 2);
            this.RecibirPedidosLabel.Name = "RecibirPedidosLabel";
            this.RecibirPedidosLabel.Size = new System.Drawing.Size(154, 22);
            this.RecibirPedidosLabel.TabIndex = 8;
            this.RecibirPedidosLabel.Text = "Recibir pedidos";
            this.RecibirPedidosLabel.Visible = false;
            // 
            // RealizarPedidoLabel
            // 
            this.RealizarPedidoLabel.AutoSize = true;
            this.RealizarPedidoLabel.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealizarPedidoLabel.Location = new System.Drawing.Point(3, 3);
            this.RealizarPedidoLabel.Name = "RealizarPedidoLabel";
            this.RealizarPedidoLabel.Size = new System.Drawing.Size(257, 22);
            this.RealizarPedidoLabel.TabIndex = 6;
            this.RealizarPedidoLabel.Text = "Realizar Pedido a Industria";
            this.RealizarPedidoLabel.Visible = false;
            // 
            // EnviarPedidoLabel
            // 
            this.EnviarPedidoLabel.AutoSize = true;
            this.EnviarPedidoLabel.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarPedidoLabel.Location = new System.Drawing.Point(1, 3);
            this.EnviarPedidoLabel.Name = "EnviarPedidoLabel";
            this.EnviarPedidoLabel.Size = new System.Drawing.Size(269, 22);
            this.EnviarPedidoLabel.TabIndex = 7;
            this.EnviarPedidoLabel.Text = "Confeccionar lotes de envio";
            this.EnviarPedidoLabel.Visible = false;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 10;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.TopPanel;
            this.bunifuDragControl1.Vertical = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 407);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.Sidebar);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Restaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximizar)).EndInit();
            this.TopPanelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Wrapper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salir)).EndInit();
            this.Sidebar.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.PictureBox Minimizar;
        private System.Windows.Forms.PictureBox Salir;
        private System.Windows.Forms.Panel TopPanelLeft;
        private System.Windows.Forms.Panel Sidebar;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox Maximizar;
        private System.Windows.Forms.PictureBox Restaurar;
        private System.Windows.Forms.PictureBox Wrapper;
        private Bunifu.Framework.UI.BunifuFlatButton Stock;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuFlatButton RecibirPedido;
        private Bunifu.Framework.UI.BunifuFlatButton EnviarPedido;
        private Bunifu.Framework.UI.BunifuFlatButton RealizarPedido;
        private Bunifu.Framework.UI.BunifuCustomLabel StockLabel;
        private Bunifu.Framework.UI.BunifuCustomLabel RealizarPedidoLabel;
        private Bunifu.Framework.UI.BunifuCustomLabel RecibirPedidosLabel;
        private Bunifu.Framework.UI.BunifuCustomLabel EnviarPedidoLabel;
    }
}

