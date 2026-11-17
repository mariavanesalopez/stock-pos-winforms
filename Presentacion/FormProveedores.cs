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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
           
        }

    }
}
