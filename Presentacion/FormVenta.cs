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
    public partial class FormVenta : Form
    {
        VentaServicio ventaServicio = new VentaServicio();
        ClienteServicio clienteServicio = new ClienteServicio();
        ArticuloServicio articuloServicio = new ArticuloServicio();
        public FormVenta()
        {
            InitializeComponent();
        }

        private void FormVenta_Load(object sender, EventArgs e)
        {
            lblNVenta.Text = ventaServicio.ObtenerUltimoIdVenta().ToString();
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            cbxCliente.DataSource = clienteServicio.ListarClientes();
            cbxCliente.DisplayMember = "nombre";
            cbxCliente.ValueMember = "id_cliente";
            cbxArticulo.DataSource = articuloServicio.ListarArticulos();
            cbxArticulo.DisplayMember = "nombre";
            cbxArticulo.ValueMember = "id_articulo";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Validar cantidad
            if (!int.TryParse(updCantidad.Value.ToString(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingresá una cantidad válida mayor a 0.");
                return;
            }

            // 2. Obtener el cliente seleccionado
            if (cbxCliente.SelectedIndex >= 0)
            {
                cbxCliente.Enabled = false;
            }

            // 2. Obtener el artículo seleccionado
            if (cbxArticulo.SelectedItem == null)
            {
                MessageBox.Show("Seleccioná un artículo.");
                return;
            }

            int codigoArticulo = (int)cbxArticulo.SelectedValue;
            decimal precioUnitario = cbxArticulo.SelectedValue != null ? articuloServicio.ObtenerArticuloPorId(codigoArticulo).PrecioVenta : 0; // ajustá al nombre de tu propiedad
            decimal subtotal = cantidad * precioUnitario;

            // 3. Agregar la fila al DataGridView
            dgvDetalleVenta.Rows.Add(
                cbxArticulo.SelectedValue,
                cbxArticulo.Text,
                cantidad,
                precioUnitario,
                subtotal
            );

            // 4. Recalcular el total en pantalla y limpiar campos
            ActualizarTotalEnPantalla();
            updCantidad.Value = 1;
            cbxArticulo.Focus();
        }

        private void ActualizarTotalEnPantalla()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                if (row.Cells["colSubtotal"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["colSubtotal"].Value);
                }
            }
            txtTotal.Text = total.ToString("F2");
        }

        private void dgvDetalleVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void QuitarFilaSeleccionada()
        {
            if (dgvDetalleVenta.CurrentRow == null || dgvDetalleVenta.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccioná una fila del detalle para quitar.");
                return;
            }

            dgvDetalleVenta.Rows.Remove(dgvDetalleVenta.CurrentRow);
            ActualizarTotalEnPantalla();
        }


        private void dgvDetalleVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Solo actuar cuando se editó la columna de cantidad
            if (e.RowIndex < 0 || dgvDetalleVenta.Columns[e.ColumnIndex].Name != "colCantidad")
                return;

            DataGridViewRow fila = dgvDetalleVenta.Rows[e.RowIndex];
            if (fila.IsNewRow) return;

            // Validar que la cantidad ingresada sea un entero mayor a 0
            if (!int.TryParse(fila.Cells["colCantidad"].Value?.ToString(), out int nuevaCantidad) || nuevaCantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número entero mayor a 0.");
                // Podés restaurar a 1 o al valor anterior si querés
                fila.Cells["colCantidad"].Value = 1;
                nuevaCantidad = 1;
            }

            decimal precioUnitario = Convert.ToDecimal(fila.Cells["colPrecioUnitario"].Value);
            decimal nuevoSubtotal = nuevaCantidad * precioUnitario;

            fila.Cells["colCantidad"].Value = nuevaCantidad;
            fila.Cells["colSubtotal"].Value = nuevoSubtotal;

            ActualizarTotalEnPantalla();
        }

        private void dgvDetalleVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                QuitarFilaSeleccionada();
                e.Handled = true;
            }
        }

        private void btnQuitar_Click_1(object sender, EventArgs e)
        {
            if (dgvDetalleVenta.CurrentRow == null || dgvDetalleVenta.CurrentRow.IsNewRow)
            {
                cbxCliente.Enabled = true;
                return;
            }
            QuitarFilaSeleccionada();
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            int? idCliente = cbxCliente.SelectedValue as int?;
            decimal total = decimal.TryParse(txtTotal.Text, out decimal parsedTotal) ? parsedTotal : 0;

            bool registrado = ventaServicio.RegistrarVenta(
                fechaHora,
                idCliente,
                total,
                "COMPLETADA"
            );

            if (registrado)
            {
                MessageBox.Show("Venta registrada correctamente.");
                LimpiarFormularioNuevaVenta();
            }
            else
            {
                MessageBox.Show("Error al registrar la venta.");
            }
        }

        private void LimpiarFormularioNuevaVenta()
        {
            // 1. Limpiar detalle de la venta
            dgvDetalleVenta.Rows.Clear();

            // 2. Restaurar cliente por defecto (Consumidor Final) y habilitarlo
            cbxCliente.Enabled = true;
            if (cbxCliente.Items.Count > 0)
            {
                cbxCliente.SelectedIndex = 0;
            }

            // 3. Limpiar controles de carga de artículo
            cbxArticulo.SelectedIndex = -1;
            updCantidad.ResetText();

            // 4. Reiniciar total en pantalla
            ActualizarTotalEnPantalla();

            // 5. Dejar el foco listo para la siguiente venta
            cbxCliente.Focus();
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            LimpiarFormularioNuevaVenta();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
