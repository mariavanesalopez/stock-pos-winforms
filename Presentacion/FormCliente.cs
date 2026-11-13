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
    public partial class FormCliente : Form
    {
        ClienteServicio clienteServicio = new ClienteServicio();
        public FormCliente()
        {
            InitializeComponent();
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            CargarclientesEnGrilla();
        }

        public void CargarclientesEnGrilla()
        {
            try
            {
                // Asignamos el DataTable que viene desde Negocio directamente al DataGridView
                dgvClientes.DataSource = clienteServicio.ListarClientes();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string dni = txtDni.Text;
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;

            bool resultado = clienteServicio.GuardarCliente(nombre, dni, telefono, direccion);

            if (resultado)
            {
                MessageBox.Show("¡Cliente guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizamos Grilla
                CargarclientesEnGrilla();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtNombre.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = txtId.Text != "" ? Convert.ToInt32(txtId.Text) : 0;
            string nombre = txtNombre.Text;
            string dni = txtDni.Text;
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;

            bool resultado = clienteServicio.ActualizarCliente(id, nombre, dni, telefono, direccion);

            if (resultado)
            {
                MessageBox.Show("¡Cliente actualizado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizamos Grilla
                CargarclientesEnGrilla();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada actual
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

                txtId.Text = fila.Cells["id_cliente"].Value.ToString();
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtDni.Text = fila.Cells["dni"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
                txtDireccion.Text = fila.Cells["direccion"].Value.ToString();

                txtId.Enabled = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿¡Esta seguro de Eliminar al cliente con ID " + txtId.Text + "!?", "Éxito", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                int id = Convert.ToInt32(txtId.Text);
                bool resultado = clienteServicio.EliminarCliente(id);

                if (resultado)
                {
                    MessageBox.Show("¡Cliente eliminado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizamos Grilla
                    CargarclientesEnGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
