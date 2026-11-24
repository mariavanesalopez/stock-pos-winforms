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
            EstiloFormulario.Aplicar(this, "Clientes", "Administrá la información de tus clientes");
            Resize += FormCliente_Resize;
            FormCliente_Resize(this, EventArgs.Empty);
        }

        private void FormCliente_Resize(object sender, EventArgs e)
        {
            int ancho = ClientSize.Width;
            int margen = 24;
            int espacio = Math.Max(0, ancho - margen * 2);
            int altoCampo = 30;
            int yCampos = 20;

            label1.Location = new Point(margen, yCampos + 6);
            txtNombre.Location = new Point(margen + 65, yCampos);
            txtNombre.Size = new Size(Math.Max(80, (espacio - 650) / 3), altoCampo);

            int x = txtNombre.Right + 18;
            label2.Location = new Point(x, yCampos + 6);
            txtDni.Location = new Point(x + 42, yCampos);
            txtDni.Size = new Size(Math.Max(80, (espacio - 650) / 3), altoCampo);

            x = txtDni.Right + 18;
            label3.Location = new Point(x, yCampos + 6);
            txtTelefono.Location = new Point(x + 62, yCampos);
            txtTelefono.Size = new Size(Math.Max(90, (espacio - 650) / 3), altoCampo);

            x = txtTelefono.Right + 18;
            label4.Location = new Point(x, yCampos + 6);
            txtDireccion.Location = new Point(x + 68, yCampos);
            txtDireccion.Size = new Size(Math.Max(100, ancho - margen - txtDireccion.Left), altoCampo);

            int yBotones = 68;
            int anchoBoton = 120;
            int separacion = 14;
            int grupo = anchoBoton * 4 + separacion * 3;
            int inicio = Math.Max(margen, (ancho - grupo) / 2);
            btnGuardar.SetBounds(inicio, yBotones, anchoBoton, 36);
            btnModificar.SetBounds(inicio + anchoBoton + separacion, yBotones, anchoBoton, 36);
            btnEliminar.SetBounds(inicio + (anchoBoton + separacion) * 2, yBotones, anchoBoton, 36);
            btnLimpiar.SetBounds(inicio + (anchoBoton + separacion) * 3, yBotones, anchoBoton, 36);

            dgvClientes.SetBounds(margen, 122, Math.Max(100, ancho - margen * 2), Math.Max(120, ClientSize.Height - 140));
            dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
