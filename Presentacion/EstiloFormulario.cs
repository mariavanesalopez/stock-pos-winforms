using System.Drawing;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    internal static class EstiloFormulario
    {
        private static readonly Color Fondo = Color.White;
        private static readonly Color Azul = Color.FromArgb(47, 111, 178);
        private static readonly Color Texto = Color.FromArgb(38, 50, 65);

        public static void Aplicar(Form formulario, string titulo, string subtitulo)
        {
            formulario.SuspendLayout();
            formulario.Text = titulo;
            formulario.BackColor = Fondo;
            formulario.Font = new Font("Segoe UI", 9F);
            formulario.AutoScroll = true;

            Label encabezado = new Label { Text = titulo, Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = Texto, Location = new Point(24, 12), AutoSize = true, Name = "lblEncabezado" };
            Label descripcion = new Label { Text = subtitulo, Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(105, 116, 130), Location = new Point(26, 48), AutoSize = true, Name = "lblDescripcion" };
            formulario.Controls.Add(encabezado);
            formulario.Controls.Add(descripcion);
            foreach (Control control in formulario.Controls) AplicarControl(control);
            formulario.ResumeLayout(true);
        }

        private static void AplicarControl(Control control)
        {
            if (control is Label label && label.Name != "lblEncabezado" && label.Name != "lblDescripcion")
            {
                label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                label.ForeColor = Texto;
            }
            if (control is TextBox caja)
            {
                caja.Font = new Font("Segoe UI", 10F);
                caja.BackColor = Color.White;
                caja.ForeColor = Texto;
                caja.BorderStyle = BorderStyle.FixedSingle;
            }
            if (control is ComboBox combo)
            {
                combo.Font = new Font("Segoe UI", 10F);
                combo.BackColor = Color.White;
                combo.ForeColor = Texto;
                // Flat puede quitar el borde nativo del ComboBox en algunos temas de Windows.
                // El estilo estándar conserva un contorno claro y consistente.
                combo.FlatStyle = FlatStyle.Standard;
            }
            if (control is Button boton)
            {
                boton.FlatStyle = FlatStyle.Flat;
                boton.FlatAppearance.BorderSize = 0;
                boton.BackColor = boton.Name == "btnGuardar" || boton.Name == "btnCobrar" ? Azul : Color.FromArgb(232, 237, 243);
                boton.ForeColor = boton.Name == "btnGuardar" || boton.Name == "btnCobrar" ? Color.White : Texto;
                boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                boton.Cursor = Cursors.Hand;
                boton.Height = Math.Max(boton.Height, 32);
            }
            if (control is DataGridView grilla)
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
                grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
