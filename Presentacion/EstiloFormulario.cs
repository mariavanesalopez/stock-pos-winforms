using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    internal static class EstiloFormulario
    {
        private static readonly Color Fondo = Color.FromArgb(245, 247, 250);
        private static readonly Color Azul = Color.FromArgb(37, 99, 168);
        private static readonly Color Texto = Color.FromArgb(38, 50, 65);

        public static void Aplicar(Form formulario, string titulo, string subtitulo)
        {
            formulario.SuspendLayout();
            formulario.Text = titulo;
            formulario.BackColor = Fondo;
            formulario.Font = new Font("Segoe UI", 9F);
            formulario.MinimumSize = new Size(700, 500);
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.AutoScroll = true;

            Label encabezado = new Label();
            encabezado.Text = titulo;
            encabezado.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            encabezado.ForeColor = Texto;
            encabezado.Location = new Point(30, 16);
            encabezado.AutoSize = true;
            encabezado.Name = "lblEncabezado";
            formulario.Controls.Add(encabezado);
            encabezado.BringToFront();

            Label descripcion = new Label();
            descripcion.Text = subtitulo;
            descripcion.Font = new Font("Segoe UI", 9F);
            descripcion.ForeColor = Color.FromArgb(105, 116, 130);
            descripcion.Location = new Point(32, 54);
            descripcion.AutoSize = true;
            descripcion.Name = "lblDescripcion";
            formulario.Controls.Add(descripcion);
            descripcion.BringToFront();

            foreach (Control control in formulario.Controls)
                AplicarControl(control);

            formulario.ResumeLayout(true);
        }

        private static void AplicarControl(Control control)
        {
            Label label = control as Label;
            if (label != null && label.Name != "lblEncabezado" && label.Name != "lblDescripcion")
            {
                label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                label.ForeColor = Texto;
            }

            TextBox caja = control as TextBox;
            if (caja != null)
            {
                caja.Font = new Font("Segoe UI", 10F);
                caja.BackColor = Color.White;
                caja.ForeColor = Texto;
                caja.BorderStyle = BorderStyle.FixedSingle;
            }

            ComboBox combo = control as ComboBox;
            if (combo != null)
            {
                combo.Font = new Font("Segoe UI", 10F);
                combo.BackColor = Color.White;
                combo.ForeColor = Texto;
                combo.FlatStyle = FlatStyle.Flat;
            }

            Button boton = control as Button;
            if (boton != null)
            {
                boton.FlatStyle = FlatStyle.Flat;
                boton.FlatAppearance.BorderSize = 0;
                boton.BackColor = boton.Name == "btnGuardar" ? Azul : Color.FromArgb(226, 232, 240);
                boton.ForeColor = boton.Name == "btnGuardar" ? Color.White : Texto;
                boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                boton.Cursor = Cursors.Hand;
                boton.Height = 36;
            }

            DataGridView grilla = control as DataGridView;
            if (grilla != null)
            {
                grilla.BackgroundColor = Color.White;
                grilla.BorderStyle = BorderStyle.None;
                grilla.GridColor = Color.FromArgb(230, 234, 239);
                grilla.EnableHeadersVisualStyles = false;
                grilla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 238, 245);
                grilla.ColumnHeadersDefaultCellStyle.ForeColor = Texto;
                grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                grilla.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                grilla.DefaultCellStyle.ForeColor = Texto;
                grilla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(218, 232, 248);
                grilla.DefaultCellStyle.SelectionForeColor = Texto;
                grilla.RowHeadersVisible = false;
                grilla.RowTemplate.Height = 30;
                grilla.AllowUserToAddRows = false;
                grilla.ReadOnly = true;
                grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
