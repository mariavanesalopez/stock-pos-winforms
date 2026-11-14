namespace Presentacion
{
    partial class FormArticulo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblPrecioCompra = new System.Windows.Forms.Label();
            this.lblPrecioVenta = new System.Windows.Forms.Label();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.lblStockMinimo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.txtStockActual = new System.Windows.Forms.TextBox();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.dgvArticulos = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).BeginInit();
            this.SuspendLayout();
            this.lblCodigo.AutoSize = true; this.lblCodigo.Location = new System.Drawing.Point(30, 30); this.lblCodigo.Text = "Código";
            this.txtCodigo.Location = new System.Drawing.Point(150, 27); this.txtCodigo.MaxLength = 20; this.txtCodigo.Size = new System.Drawing.Size(180, 22);
            this.lblNombre.AutoSize = true; this.lblNombre.Location = new System.Drawing.Point(30, 70); this.lblNombre.Text = "Nombre";
            this.txtNombre.Location = new System.Drawing.Point(150, 67); this.txtNombre.MaxLength = 150; this.txtNombre.Size = new System.Drawing.Size(250, 22);
            this.lblCategoria.AutoSize = true; this.lblCategoria.Location = new System.Drawing.Point(440, 30); this.lblCategoria.Text = "Categoría";
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbCategoria.Location = new System.Drawing.Point(550, 27); this.cmbCategoria.Size = new System.Drawing.Size(150, 24);
            this.lblPrecioCompra.AutoSize = true; this.lblPrecioCompra.Location = new System.Drawing.Point(440, 70); this.lblPrecioCompra.Text = "Precio compra";
            this.txtPrecioCompra.Location = new System.Drawing.Point(550, 67); this.txtPrecioCompra.Size = new System.Drawing.Size(120, 22);
            this.lblPrecioVenta.AutoSize = true; this.lblPrecioVenta.Location = new System.Drawing.Point(700, 30); this.lblPrecioVenta.Text = "Precio venta";
            this.txtPrecioVenta.Location = new System.Drawing.Point(800, 27); this.txtPrecioVenta.Size = new System.Drawing.Size(120, 22);
            this.lblStockActual.AutoSize = true; this.lblStockActual.Location = new System.Drawing.Point(700, 70); this.lblStockActual.Text = "Stock actual";
            this.txtStockActual.Location = new System.Drawing.Point(800, 67); this.txtStockActual.Size = new System.Drawing.Size(120, 22);
            this.lblStockMinimo.AutoSize = true; this.lblStockMinimo.Location = new System.Drawing.Point(30, 110); this.lblStockMinimo.Text = "Stock mínimo";
            this.txtStockMinimo.Location = new System.Drawing.Point(150, 107); this.txtStockMinimo.Size = new System.Drawing.Size(120, 22);
            this.txtId.Enabled = false; this.txtId.Location = new System.Drawing.Point(300, 107); this.txtId.Visible = false;
            this.btnGuardar.Location = new System.Drawing.Point(150, 155); this.btnGuardar.Size = new System.Drawing.Size(110, 32); this.btnGuardar.Text = "GUARDAR"; this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnModificar.Location = new System.Drawing.Point(290, 155); this.btnModificar.Size = new System.Drawing.Size(110, 32); this.btnModificar.Text = "MODIFICAR"; this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            this.btnEliminar.Location = new System.Drawing.Point(430, 155); this.btnEliminar.Size = new System.Drawing.Size(110, 32); this.btnEliminar.Text = "ELIMINAR"; this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            this.btnLimpiar.Location = new System.Drawing.Point(570, 155); this.btnLimpiar.Size = new System.Drawing.Size(110, 32); this.btnLimpiar.Text = "LIMPIAR"; this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.dgvArticulos.AllowUserToAddRows = false; this.dgvArticulos.ReadOnly = true; this.dgvArticulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvArticulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvArticulos.Location = new System.Drawing.Point(30, 210); this.dgvArticulos.Size = new System.Drawing.Size(950, 300); this.dgvArticulos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArticulos_CellClick);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.ClientSize = new System.Drawing.Size(1015, 540);
            this.Controls.Add(this.lblCodigo); this.Controls.Add(this.txtCodigo); this.Controls.Add(this.lblNombre); this.Controls.Add(this.txtNombre); this.Controls.Add(this.lblCategoria); this.Controls.Add(this.cmbCategoria); this.Controls.Add(this.lblPrecioCompra); this.Controls.Add(this.txtPrecioCompra); this.Controls.Add(this.lblPrecioVenta); this.Controls.Add(this.txtPrecioVenta); this.Controls.Add(this.lblStockActual); this.Controls.Add(this.txtStockActual); this.Controls.Add(this.lblStockMinimo); this.Controls.Add(this.txtStockMinimo); this.Controls.Add(this.txtId); this.Controls.Add(this.btnGuardar); this.Controls.Add(this.btnModificar); this.Controls.Add(this.btnEliminar); this.Controls.Add(this.btnLimpiar); this.Controls.Add(this.dgvArticulos);
            this.Name = "FormArticulo"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Artículos"; this.Load += new System.EventHandler(this.FormArticulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).EndInit(); this.ResumeLayout(false); this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCodigo, lblNombre, lblCategoria, lblPrecioCompra, lblPrecioVenta, lblStockActual, lblStockMinimo;
        private System.Windows.Forms.TextBox txtCodigo, txtNombre, txtPrecioCompra, txtPrecioVenta, txtStockActual, txtStockMinimo, txtId;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.DataGridView dgvArticulos;
        private System.Windows.Forms.Button btnGuardar, btnModificar, btnEliminar, btnLimpiar;
    }
}
