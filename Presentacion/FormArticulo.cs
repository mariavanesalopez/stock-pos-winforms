using Negocio;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FormArticulo : Form
    {
        private readonly ArticuloServicio articuloServicio = new ArticuloServicio();

        public FormArticulo() { InitializeComponent(); }

        private void FormArticulo_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarArticulosEnGrilla();
        }

        private void CargarCategorias()
        {
            try
            {
                cmbCategoria.DataSource = articuloServicio.ListarCategorias();
                cmbCategoria.DisplayMember = "nombre";
                cmbCategoria.ValueMember = "nombre";
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                cmbCategoria.DataSource = null;
                MessageBox.Show(ex.Message, "Categorías", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarArticulosEnGrilla()
        {
            try
            {
                dgvArticulos.DataSource = articuloServicio.ListarArticulos();
                if (dgvArticulos.Columns.Contains("categoria")) dgvArticulos.Columns["categoria"].HeaderText = "Categoría";
                if (dgvArticulos.Columns.Contains("id_articulo")) dgvArticulos.Columns["id_articulo"].HeaderText = "ID";
                if (dgvArticulos.Columns.Contains("codigo")) dgvArticulos.Columns["codigo"].HeaderText = "Código";
                if (dgvArticulos.Columns.Contains("nombre")) dgvArticulos.Columns["nombre"].HeaderText = "Nombre";
                if (dgvArticulos.Columns.Contains("precio_compra")) dgvArticulos.Columns["precio_compra"].HeaderText = "Precio compra";
                if (dgvArticulos.Columns.Contains("precio_venta")) dgvArticulos.Columns["precio_venta"].HeaderText = "Precio venta";
                if (dgvArticulos.Columns.Contains("stock_actual")) dgvArticulos.Columns["stock_actual"].HeaderText = "Stock actual";
                if (dgvArticulos.Columns.Contains("stock_minimo")) dgvArticulos.Columns["stock_minimo"].HeaderText = "Stock mínimo";
            }
            catch (InvalidOperationException ex) when (ex.Message == "No existen artículos cargados.") { dgvArticulos.DataSource = null; }
            catch (Exception ex) { MessageBox.Show("Error al cargar los artículos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private string CategoriaSeleccionada()
        {
            if (cmbCategoria.SelectedIndex < 0 || cmbCategoria.SelectedValue == null || cmbCategoria.SelectedValue is DataRowView)
                throw new ArgumentException("Seleccione una categoría.");
            return cmbCategoria.Text;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(delegate
            {
                bool resultado = articuloServicio.GuardarArticulo(txtCodigo.Text, txtNombre.Text, CategoriaSeleccionada(), LeerDecimal(txtPrecioCompra, "precio de compra"), LeerDecimal(txtPrecioVenta, "precio de venta"), LeerEntero(txtStockActual, "stock actual"), LeerEntero(txtStockMinimo, "stock mínimo"));
                if (!resultado) throw new InvalidOperationException("No se pudo guardar el artículo.");
                MessageBox.Show("¡Artículo guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarArticulosEnGrilla(); LimpiarCampos();
            });
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(delegate
            {
                int id = LeerEntero(txtId, "ID del artículo");
                bool resultado = articuloServicio.ActualizarArticulo(id, txtCodigo.Text, txtNombre.Text, CategoriaSeleccionada(), LeerDecimal(txtPrecioCompra, "precio de compra"), LeerDecimal(txtPrecioVenta, "precio de venta"), LeerEntero(txtStockActual, "stock actual"), LeerEntero(txtStockMinimo, "stock mínimo"));
                if (!resultado) throw new InvalidOperationException("No se pudo actualizar el artículo.");
                MessageBox.Show("¡Artículo actualizado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarArticulosEnGrilla(); LimpiarCampos();
            });
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(delegate
            {
                int id = LeerEntero(txtId, "ID del artículo");
                if (MessageBox.Show("¿Está seguro de eliminar el artículo con ID " + id + "?", "Confirmar eliminación", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;
                if (!articuloServicio.EliminarArticulo(id)) throw new InvalidOperationException("No se pudo eliminar el artículo.");
                MessageBox.Show("¡Artículo eliminado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarArticulosEnGrilla(); LimpiarCampos();
            });
        }

        private void dgvArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvArticulos.Rows[e.RowIndex];
            txtId.Text = Valor(fila, "id_articulo");
            txtCodigo.Text = Valor(fila, "codigo");
            txtNombre.Text = Valor(fila, "nombre");
            cmbCategoria.Text = Valor(fila, "categoria");
            txtPrecioCompra.Text = Valor(fila, "precio_compra");
            txtPrecioVenta.Text = Valor(fila, "precio_venta");
            txtStockActual.Text = Valor(fila, "stock_actual");
            txtStockMinimo.Text = Valor(fila, "stock_minimo");
        }

        private static string Valor(DataGridViewRow fila, string columna)
        {
            object valor = fila.Cells[columna].Value;
            return valor == null || valor == DBNull.Value ? string.Empty : Convert.ToString(valor, CultureInfo.CurrentCulture);
        }

        private static int LeerEntero(TextBox campo, string nombre)
        {
            int valor;
            if (!int.TryParse(campo.Text, out valor)) throw new ArgumentException("Ingrese un valor entero válido para " + nombre + ".");
            return valor;
        }

        private static decimal LeerDecimal(TextBox campo, string nombre)
        {
            decimal valor;
            if (!decimal.TryParse(campo.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out valor) && !decimal.TryParse(campo.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out valor))
                throw new ArgumentException("Ingrese un valor numérico válido para " + nombre + ".");
            return valor;
        }

        private void EjecutarOperacion(Action operacion)
        {
            try { operacion(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LimpiarCampos()
        {
            txtId.Clear(); txtCodigo.Clear(); txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            txtPrecioCompra.Clear(); txtPrecioVenta.Clear(); txtStockActual.Clear(); txtStockMinimo.Clear();
            txtCodigo.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e) { LimpiarCampos(); }
    }
}
