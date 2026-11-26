namespace Presentacion
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (seccionActual != null) seccionActual.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblMarcaSub = new System.Windows.Forms.Label();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnFormCliente = new System.Windows.Forms.Button();
            this.btnFormArticulo = new System.Windows.Forms.Button();
            this.btnFormVenta = new System.Windows.Forms.Button();
            this.btnHistorialVentas = new System.Windows.Forms.Button();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.panelNavegacion = new System.Windows.Forms.FlowLayoutPanel();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.lblTituloSeccion = new System.Windows.Forms.Label();
            this.lblSubtituloSeccion = new System.Windows.Forms.Label();
            this.panelBienvenida = new System.Windows.Forms.Panel();
            this.lblIconoBienvenida = new System.Windows.Forms.Label();
            this.lblBienvenidaTitulo = new System.Windows.Forms.Label();
            this.lblBienvenidaTexto = new System.Windows.Forms.Label();
            this.panelSeccion = new System.Windows.Forms.Panel();
            this.panelSuperior.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelBienvenida.SuspendLayout();
            this.SuspendLayout();
            // panelSuperior
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(27, 37, 52);
            this.panelSuperior.Controls.Add(this.lblMarca);
            this.panelSuperior.Controls.Add(this.lblMarcaSub);
            this.panelSuperior.Controls.Add(this.panelNavegacion);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Height = 82;
            this.panelSuperior.Padding = new System.Windows.Forms.Padding(20, 12, 20, 8);
            this.panelSuperior.Name = "panelSuperior";
            // marca
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(20, 10);
            this.lblMarca.Text = "Punto de venta";
            this.lblMarcaSub.AutoSize = true;
            this.lblMarcaSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMarcaSub.ForeColor = System.Drawing.Color.FromArgb(166, 180, 199);
            this.lblMarcaSub.Location = new System.Drawing.Point(23, 43);
            this.lblMarcaSub.Text = "GESTIÓN COMERCIAL";
            // navigation
            this.panelNavegacion.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelNavegacion.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelNavegacion.WrapContents = false;
            this.panelNavegacion.Width = 690;
            this.panelNavegacion.Height = 58;
            this.panelNavegacion.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelNavegacion.Name = "panelNavegacion";
            PrepararBoton(this.btnInicio, "Inicio", this.btnInicio_Click);
            PrepararBoton(this.btnFormCliente, "Clientes", this.btnFormCliente_Click);
            PrepararBoton(this.btnFormArticulo, "Artículos", this.btnFormArticulo_Click);
            PrepararBoton(this.btnFormVenta, "Nueva venta", this.btnFormVenta_Click);
            PrepararBoton(this.btnHistorialVentas, "Ventas", this.btnHistorialVentas_Click);
            PrepararBoton(this.btnProveedores, "Proveedores", this.btnProveedores_Click);
            this.panelNavegacion.Controls.Add(this.btnInicio);
            this.panelNavegacion.Controls.Add(this.btnFormCliente);
            this.panelNavegacion.Controls.Add(this.btnFormArticulo);
            this.panelNavegacion.Controls.Add(this.btnFormVenta);
            this.panelNavegacion.Controls.Add(this.btnHistorialVentas);
            this.panelNavegacion.Controls.Add(this.btnProveedores);
            // panelContenido
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.panelContenido.Controls.Add(this.panelSeccion);
            this.panelContenido.Controls.Add(this.panelBienvenida);
            this.panelContenido.Controls.Add(this.lblSubtituloSeccion);
            this.panelContenido.Controls.Add(this.lblTituloSeccion);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Padding = new System.Windows.Forms.Padding(28, 20, 28, 24);
            this.panelContenido.Name = "panelContenido";
            // titles
            this.lblTituloSeccion.AutoSize = true;
            this.lblTituloSeccion.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblTituloSeccion.ForeColor = System.Drawing.Color.FromArgb(34, 47, 64);
            this.lblTituloSeccion.Location = new System.Drawing.Point(30, 20);
            this.lblTituloSeccion.Name = "lblTituloSeccion";
            this.lblTituloSeccion.Text = "Panel principal";
            this.lblSubtituloSeccion.AutoSize = true;
            this.lblSubtituloSeccion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtituloSeccion.ForeColor = System.Drawing.Color.FromArgb(112, 124, 140);
            this.lblSubtituloSeccion.Location = new System.Drawing.Point(34, 60);
            this.lblSubtituloSeccion.Name = "lblSubtituloSeccion";
            this.lblSubtituloSeccion.Text = "Elegí una sección para comenzar a trabajar.";
            // welcome
            this.panelBienvenida.BackColor = System.Drawing.Color.White;
            this.panelBienvenida.Controls.Add(this.lblIconoBienvenida);
            this.panelBienvenida.Controls.Add(this.lblBienvenidaTitulo);
            this.panelBienvenida.Controls.Add(this.lblBienvenidaTexto);
            this.panelBienvenida.Location = new System.Drawing.Point(35, 112);
            this.panelBienvenida.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.panelBienvenida.Height = 235;
            this.panelBienvenida.Name = "panelBienvenida";
            this.lblIconoBienvenida.Font = new System.Drawing.Font("Segoe UI Emoji", 34F);
            this.lblIconoBienvenida.Location = new System.Drawing.Point(32, 43);
            this.lblIconoBienvenida.Size = new System.Drawing.Size(78, 70);
            this.lblIconoBienvenida.Text = "🛍";
            this.lblBienvenidaTitulo.AutoSize = true;
            this.lblBienvenidaTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblBienvenidaTitulo.ForeColor = System.Drawing.Color.FromArgb(34, 47, 64);
            this.lblBienvenidaTitulo.Location = new System.Drawing.Point(125, 48);
            this.lblBienvenidaTitulo.Text = "¡Bienvenido!";
            this.lblBienvenidaTexto.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblBienvenidaTexto.ForeColor = System.Drawing.Color.FromArgb(112, 124, 140);
            this.lblBienvenidaTexto.Location = new System.Drawing.Point(130, 92);
            this.lblBienvenidaTexto.Size = new System.Drawing.Size(570, 70);
            this.lblBienvenidaTexto.Text = "Usá la barra superior para administrar clientes, artículos y proveedores, o para registrar una venta.";
            // section host
            this.panelSeccion.BackColor = System.Drawing.Color.White;
            this.panelSeccion.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right;
            this.panelSeccion.Location = new System.Drawing.Point(28, 105);
            this.panelSeccion.Size = new System.Drawing.Size(1184, 660);
            this.panelSeccion.Name = "panelSeccion";
            this.panelSeccion.Visible = false;
            this.panelSeccion.AutoScroll = true;
            this.panelSeccion.Resize += new System.EventHandler(this.panelSeccion_Resize);
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 247, 251);
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelSuperior);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(820, 620);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Punto de venta | Panel principal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.panelContenido.PerformLayout();
            this.panelBienvenida.ResumeLayout(false);
            this.panelBienvenida.PerformLayout();
            this.ResumeLayout(false);
        }

        private void PrepararBoton(System.Windows.Forms.Button boton, string texto, System.EventHandler click)
        {
            boton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = System.Drawing.Color.FromArgb(37, 48, 65);
            boton.ForeColor = System.Drawing.Color.FromArgb(205, 214, 226);
            boton.Cursor = System.Windows.Forms.Cursors.Hand;
            boton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            boton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            boton.Size = new System.Drawing.Size(texto == "Proveedores" ? 112 : texto == "Artículos" ? 94 : 88, 40);
            boton.Name = "btn" + texto;
            boton.Text = texto;
            boton.AutoSize = false;
            boton.Width = texto == "Nueva venta" ? 116 : (texto == "Proveedores" ? 112 : 92);
            boton.Click += click;
        }

        private System.Windows.Forms.Panel panelSuperior, panelContenido, panelBienvenida, panelSeccion;
        private System.Windows.Forms.FlowLayoutPanel panelNavegacion;
        private System.Windows.Forms.Label lblMarca, lblMarcaSub, lblTituloSeccion, lblSubtituloSeccion, lblIconoBienvenida, lblBienvenidaTitulo, lblBienvenidaTexto;
        private System.Windows.Forms.Button btnInicio, btnFormCliente, btnFormArticulo, btnFormVenta, btnHistorialVentas, btnProveedores;
    }
}
