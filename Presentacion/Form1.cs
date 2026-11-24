using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        private Form seccionActual;
        private Button botonActivo;

        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle areaTrabajo = Screen.FromControl(this).WorkingArea;
            const int anchoObjetivo = 1200;
            const int altoObjetivo = 760;
            ClientSize = new Size(Math.Min(anchoObjetivo, areaTrabajo.Width - 40), Math.Min(altoObjetivo, areaTrabajo.Height - 40));
            MinimumSize = Size;
            MaximumSize = Size;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            panelSeccion.Width = Math.Max(100, panelContenido.ClientSize.Width - 56);
            panelSeccion.Height = Math.Max(100, panelContenido.ClientSize.Height - 129);
            StartPosition = FormStartPosition.Manual;
            Location = new Point(areaTrabajo.Left + Math.Max(0, (areaTrabajo.Width - Width) / 2), areaTrabajo.Top + Math.Max(0, (areaTrabajo.Height - Height) / 2));
            MostrarInicio();
        }

        private void btnInicio_Click(object sender, EventArgs e) { MostrarInicio(); }
        private void btnFormCliente_Click(object sender, EventArgs e) { MostrarSeccion(new FormCliente(), btnFormCliente, "Clientes", "Administrá la información de tus clientes"); }
        private void btnFormArticulo_Click(object sender, EventArgs e) { MostrarSeccion(new FormArticulo(), btnFormArticulo, "Artículos", "Consultá y mantené el catálogo de productos"); }
        private void btnProveedores_Click(object sender, EventArgs e) { MostrarSeccion(new FormProveedores(), btnProveedores, "Proveedores", "Mantené los datos de tus proveedores"); }
        private void btnFormVenta_Click(object sender, EventArgs e) { MostrarSeccion(new FormVenta(), btnFormVenta, "Ventas", "Registrá una venta y consultá su comprobante"); }

        private void MostrarInicio()
        {
            LimpiarSeccionActual();
            SeleccionarBoton(btnInicio);
            lblTituloSeccion.Text = "Panel principal";
            lblSubtituloSeccion.Text = "Elegí una sección para comenzar a trabajar.";
            panelBienvenida.Visible = true;
            panelSeccion.Visible = false;
        }

        private void MostrarSeccion(Form formulario, Button boton, string titulo, string subtitulo)
        {
            LimpiarSeccionActual();
            SeleccionarBoton(boton);
            lblTituloSeccion.Text = titulo;
            lblSubtituloSeccion.Text = subtitulo;
            panelBienvenida.Visible = false;
            panelSeccion.Visible = true;

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.AutoScaleMode = AutoScaleMode.None;
            formulario.AutoScroll = false;
            formulario.Dock = DockStyle.Fill;
            panelSeccion.Controls.Add(formulario);
            seccionActual = formulario;
            foreach (Control control in formulario.Controls)
            {
                if (control.Name == "lblEncabezado" || control.Name == "lblDescripcion")
                    control.Visible = false;
            }
            formulario.Show();
            formulario.BringToFront();
        }

        private void CentrarSeccionActual() { }

        private void LimpiarSeccionActual()
        {
            if (seccionActual != null)
            {
                seccionActual.Close();
                seccionActual.Dispose();
                seccionActual = null;
            }
            panelSeccion.Controls.Clear();
        }

        private void SeleccionarBoton(Button boton)
        {
            if (botonActivo != null)
            {
                botonActivo.BackColor = Color.FromArgb(37, 48, 65);
                botonActivo.ForeColor = Color.FromArgb(205, 214, 226);
            }
            botonActivo = boton;
            botonActivo.BackColor = Color.FromArgb(53, 120, 190);
            botonActivo.ForeColor = Color.White;
        }

        private void panelSeccion_Resize(object sender, EventArgs e) { }
    }
}
