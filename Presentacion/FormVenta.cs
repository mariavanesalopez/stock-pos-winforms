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
            txtNumVenta.Text = ventaServicio.ObtenerUltimoIdVenta().ToString();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
    }
}
