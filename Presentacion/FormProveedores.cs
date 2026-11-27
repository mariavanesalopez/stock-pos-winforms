using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FormProveedores : Form
    {
        ProveedoresServicio proveedoresServicio = new ProveedoresServicio();
        public FormProveedores()
        {
            InitializeComponent();
            EstiloFormulario.Aplicar(this, "Proveedores", "Mantené los datos de tus proveedores");
            Resize += FormProveedores_Resize;
            FormProveedores_Resize(this, EventArgs.Empty);
        }

        private void FormProveedores_Resize(object sender, EventArgs e)
        {
            int margen = 24;
            int anchoColumna = Math.Max(180, (ClientSize.Width - margen * 2) / 3);
            int fila1 = 14;
            int fila2 = 54;

            PosicionarCampo(label1, txtRazonSocial, margen, fila1, 92, anchoColumna);
            PosicionarCampo(label2, txtCuit, margen + anchoColumna, fila1, 48, anchoColumna);
            PosicionarCampo(label5, txtCondicionIva, margen + anchoColumna * 2, fila1, 94, anchoColumna - margen);

            PosicionarCampo(label3, txtTelefono, margen, fila2, 70, anchoColumna);
            PosicionarCampo(label6, txtEmail, margen + anchoColumna, fila2, 105, anchoColumna);
            PosicionarCampo(label4, txtDireccion, margen + anchoColumna * 2, fila2, 74, anchoColumna - margen);

            txtId.Visible = false;
            int yBotones = 98;
            int anchoBoton = 120;
            int separacion = 14;
            int inicio = Math.Max(margen, (ClientSize.Width - (anchoBoton * 4 + separacion * 3)) / 2);
            btnGuardar.SetBounds(inicio, yBotones, anchoBoton, 36);
            btnModificar.SetBounds(inicio + anchoBoton + separacion, yBotones, anchoBoton, 36);
            btnEliminar.SetBounds(inicio + (anchoBoton + separacion) * 2, yBotones, anchoBoton, 36);
            btnLimpiar.SetBounds(inicio + (anchoBoton + separacion) * 3, yBotones, anchoBoton, 36);

            dgvProveedores.SetBounds(margen, 150, Math.Max(100, ClientSize.Width - margen * 2), Math.Max(120, ClientSize.Height - 168));
            dgvProveedores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private static void PosicionarCampo(Label etiqueta, TextBox campo, int x, int y, int anchoEtiqueta, int anchoGrupo)
        {
            etiqueta.Location = new Point(x, y + 7);
            campo.Location = new Point(x + anchoEtiqueta, y);
            campo.Size = new Size(Math.Max(80, anchoGrupo - anchoEtiqueta - 12), 30);
        }

        private void FormProveedores_Load(object sender, EventArgs e)
        {
            CargarProveedoresEnGrilla();
        }

        public void CargarProveedoresEnGrilla()
        {
            try
            {
                // Asignamos el DataTable que viene desde Negocio directamente al DataGridView
                dgvProveedores.DataSource = proveedoresServicio.ListarProveedores();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
            string razonSocial = txtRazonSocial.Text;
            string cuit = txtCuit.Text;
            string condicionIva = txtCondicionIva.Text;
            string telefono = txtTelefono.Text;
            string email = txtEmail.Text;
            string direccion = txtDireccion.Text;

            bool resultado = proveedoresServicio.GuardarProveedores(razonSocial, cuit, condicionIva,telefono,email,direccion);

            if (resultado)
            {
                MessageBox.Show("¡Proveedor guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizamos Grilla
                CargarProveedoresEnGrilla();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
            catch (Exception ex) { MostrarErrorOperacion(ex); }
        }

        private void LimpiarCampos()
        {
            txtRazonSocial.Clear();
            txtCuit.Clear();
            txtCondicionIva.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtRazonSocial.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
            int id = txtId.Text != "" ? Convert.ToInt32(txtId.Text) : 0;
            string razonSocial = txtRazonSocial.Text;
            string cuit = txtCuit.Text;
            string condicionIva = txtCondicionIva.Text;
            string telefono = txtTelefono.Text;
            string email = txtEmail.Text;
            string direccion = txtDireccion.Text;

            bool resultado = proveedoresServicio.ActualizarProveedores(id, razonSocial, cuit, condicionIva, telefono, email, direccion);

            if (resultado)
            {
                MessageBox.Show("¡Proveedor actualizado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizamos Grilla
                CargarProveedoresEnGrilla();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
            catch (Exception ex) { MostrarErrorOperacion(ex); }
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada actual
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                txtId.Text = fila.Cells["id_proveedor"].Value.ToString();
                txtRazonSocial.Text = fila.Cells["razon_social"].Value.ToString();
                txtCuit.Text = fila.Cells["cuit"].Value.ToString();
                txtCondicionIva.Text = fila.Cells["condicion_iva"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
                txtEmail.Text = fila.Cells["email"].Value.ToString();
                txtDireccion.Text = fila.Cells["direccion"].Value.ToString();

                txtId.Enabled = false;

                             
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
            if (MessageBox.Show("¿¡Esta seguro de Eliminar al Proveedor con ID " + txtId.Text + "!?", "Éxito", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                int id = Convert.ToInt32(txtId.Text);
                bool resultado = proveedoresServicio.EliminarProveedores(id);

                if (resultado)
                {
                    MessageBox.Show("¡Proveedor eliminado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizamos Grilla
                    CargarProveedoresEnGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }
            catch (Exception ex) { MostrarErrorOperacion(ex); }
        }

        private void MostrarErrorOperacion(Exception ex)
        {
            MessageBox.Show(ex.Message, "No se pudo completar la operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
           
        }

    }
}
