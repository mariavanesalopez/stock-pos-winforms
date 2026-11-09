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
    public partial class Form1 : Form
    {
        // Instanciamos la capa de negocio
        ProductoServicio productoServicio = new ProductoServicio();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarProductoEnGrilla();
        }

        public void CargarProductoEnGrilla() 
        {
            try
            {
                // Asignamos el DataTable que viene desde Negocio directamente al DataGridView
                dgvProductos.DataSource = productoServicio.ObtenerAlumnos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            double precio = double.Parse(txtPrecio.Text);
            int existencia = Convert.ToInt32(txtStock.Text);

            bool resultado = productoServicio.CrearProducto(nombre, descripcion, precio, existencia);

            if (resultado)
            {
                MessageBox.Show("¡Producto guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizamos Grilla
                CargarProductoEnGrilla();

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos() 
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtNombre.Focus();
        }
    }
}
