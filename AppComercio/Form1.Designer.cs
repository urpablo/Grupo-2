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
            this.EnvioLabel = new System.Windows.Forms.Label();
            this.RecibirLabel = new System.Windows.Forms.Label();
            this.Restaurar = new System.Windows.Forms.PictureBox();
            this.StockLabel = new System.Windows.Forms.Label();
            this.Maximizar = new System.Windows.Forms.PictureBox();
            this.TopPanelLeft = new System.Windows.Forms.Panel();
            this.Wrapper = new System.Windows.Forms.PictureBox();
            this.Minimizar = new System.Windows.Forms.PictureBox();
            this.Salir = new System.Windows.Forms.PictureBox();
            this.Sidebar = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RecibirPedido = new System.Windows.Forms.Button();
            this.EnviarPedido = new System.Windows.Forms.Button();
            this.RealizarPedido = new System.Windows.Forms.Button();
            this.Stock = new System.Windows.Forms.Button();
            this.RealizarLabel = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Restaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximizar)).BeginInit();
            this.TopPanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Wrapper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salir)).BeginInit();
            this.Sidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(224)))), ((int)(((byte)(227)))));
            this.TopPanel.Controls.Add(this.RealizarLabel);
            this.TopPanel.Controls.Add(this.EnvioLabel);
            this.TopPanel.Controls.Add(this.RecibirLabel);
            this.TopPanel.Controls.Add(this.Restaurar);
            this.TopPanel.Controls.Add(this.StockLabel);
            this.TopPanel.Controls.Add(this.Maximizar);
            this.TopPanel.Controls.Add(this.TopPanelLeft);
            this.TopPanel.Controls.Add(this.Minimizar);
            this.TopPanel.Controls.Add(this.Salir);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(695, 32);
            this.TopPanel.TabIndex = 1;
            // 
            // EnvioLabel
            // 
            this.EnvioLabel.AutoSize = true;
            this.EnvioLabel.Font = new System.Drawing.Font("Lucida Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnvioLabel.Location = new System.Drawing.Point(163, 5);
            this.EnvioLabel.Name = "EnvioLabel";
            this.EnvioLabel.Size = new System.Drawing.Size(297, 24);
            this.EnvioLabel.TabIndex = 3;
            this.EnvioLabel.Text = "Confeccionar lotes de envio";
            this.EnvioLabel.Visible = false;
            // 
            // RecibirLabel
            // 
            this.RecibirLabel.AutoSize = true;
            this.RecibirLabel.Font = new System.Drawing.Font("Lucida Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecibirLabel.Location = new System.Drawing.Point(163, 5);
            this.RecibirLabel.Name = "RecibirLabel";
            this.RecibirLabel.Size = new System.Drawing.Size(165, 24);
            this.RecibirLabel.TabIndex = 6;
            this.RecibirLabel.Text = "Recibir Pedidos";
            this.RecibirLabel.Visible = false;
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
            // StockLabel
            // 
            this.StockLabel.AutoSize = true;
            this.StockLabel.Font = new System.Drawing.Font("Lucida Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StockLabel.Location = new System.Drawing.Point(163, 5);
            this.StockLabel.Name = "StockLabel";
            this.StockLabel.Size = new System.Drawing.Size(183, 24);
            this.StockLabel.TabIndex = 5;
            this.StockLabel.Text = "Control de Stock";
            this.StockLabel.Visible = false;
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
            // Sidebar
            // 
            this.Sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(147)))), ((int)(((byte)(206)))));
            this.Sidebar.Controls.Add(this.pictureBox4);
            this.Sidebar.Controls.Add(this.pictureBox3);
            this.Sidebar.Controls.Add(this.pictureBox2);
            this.Sidebar.Controls.Add(this.pictureBox1);
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
            // 
            // RecibirPedido
            // 
            this.RecibirPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(179)))), ((int)(((byte)(227)))));
            this.RecibirPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.RecibirPedido.FlatAppearance.BorderSize = 0;
            this.RecibirPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(209)))));
            this.RecibirPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecibirPedido.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecibirPedido.ForeColor = System.Drawing.Color.White;
            this.RecibirPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RecibirPedido.Location = new System.Drawing.Point(0, 132);
            this.RecibirPedido.Name = "RecibirPedido";
            this.RecibirPedido.Size = new System.Drawing.Size(157, 44);
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
            this.EnviarPedido.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarPedido.ForeColor = System.Drawing.Color.White;
            this.EnviarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EnviarPedido.Location = new System.Drawing.Point(0, 88);
            this.EnviarPedido.Name = "EnviarPedido";
            this.EnviarPedido.Size = new System.Drawing.Size(157, 44);
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
            this.RealizarPedido.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealizarPedido.ForeColor = System.Drawing.Color.White;
            this.RealizarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RealizarPedido.Location = new System.Drawing.Point(0, 44);
            this.RealizarPedido.Name = "RealizarPedido";
            this.RealizarPedido.Size = new System.Drawing.Size(157, 44);
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
            this.Stock.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stock.ForeColor = System.Drawing.Color.White;
            this.Stock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Stock.Location = new System.Drawing.Point(0, 0);
            this.Stock.Name = "Stock";
            this.Stock.Size = new System.Drawing.Size(157, 44);
            this.Stock.TabIndex = 9;
            this.Stock.Text = "Stock";
            this.Stock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Stock.UseVisualStyleBackColor = false;
            this.Stock.Click += new System.EventHandler(this.Stock_Click);
            // 
            // RealizarLabel
            // 
            this.RealizarLabel.AutoSize = true;
            this.RealizarLabel.Font = new System.Drawing.Font("Lucida Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealizarLabel.Location = new System.Drawing.Point(163, 5);
            this.RealizarLabel.Name = "RealizarLabel";
            this.RealizarLabel.Size = new System.Drawing.Size(286, 24);
            this.RealizarLabel.TabIndex = 7;
            this.RealizarLabel.Text = "Realizar pedido a industria";
            this.RealizarLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 407);
            this.Controls.Add(this.Sidebar);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Restaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximizar)).EndInit();
            this.TopPanelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Wrapper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salir)).EndInit();
            this.Sidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.PictureBox Minimizar;
        private System.Windows.Forms.PictureBox Salir;
        private System.Windows.Forms.Panel TopPanelLeft;
        private System.Windows.Forms.Panel Sidebar;
        private System.Windows.Forms.PictureBox Maximizar;
        private System.Windows.Forms.PictureBox Restaurar;
        private System.Windows.Forms.PictureBox Wrapper;
        private System.Windows.Forms.Button Stock;
        private System.Windows.Forms.Button RecibirPedido;
        private System.Windows.Forms.Button EnviarPedido;
        private System.Windows.Forms.Button RealizarPedido;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label EnvioLabel;
        private System.Windows.Forms.Label StockLabel;
        private System.Windows.Forms.Label RecibirLabel;
        private System.Windows.Forms.Label RealizarLabel;
    }
}

