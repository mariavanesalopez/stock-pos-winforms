using Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Datos
{
    public class ArticuloRepositorio
    {
        private readonly Conexion conexion = new Conexion();

        public DataTable ListarArticulos()
        {
            DataTable tabla = new DataTable();
            const string query = "SELECT id_articulo, nombre, categoria, precio_compra, precio_venta, stock_actual, stock_minimo FROM articulos ORDER BY nombre";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                using (MySqlDataReader lector = comando.ExecuteReader()) tabla.Load(lector);
                return tabla;
            }
            finally { conexion.CierraConexion(); }
        }

        public DataTable ListarCategorias()
        {
            DataTable tabla = new DataTable();
            const string query = "SELECT nombre FROM categorias ORDER BY nombre";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                using (MySqlDataReader lector = comando.ExecuteReader()) tabla.Load(lector);
                return tabla;
            }
            finally { conexion.CierraConexion(); }
        }

        public bool ExisteCodigo(string codigo, int idExcluir = 0)
        {
            const string query = "SELECT COUNT(1) FROM articulos WHERE codigo = @codigo AND id_articulo <> @id";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    comando.Parameters.AddWithValue("@codigo", codigo);
                    comando.Parameters.AddWithValue("@id", idExcluir);
                    return Convert.ToInt32(comando.ExecuteScalar()) > 0;
                }
            }
            finally { conexion.CierraConexion(); }
        }

        public bool ExisteId(int id)
        {
            const string query = "SELECT COUNT(1) FROM articulos WHERE id_articulo = @id";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    return Convert.ToInt32(comando.ExecuteScalar()) > 0;
                }
            }
            finally { conexion.CierraConexion(); }
        }

        public bool CrearArticulo(Articulo articulo)
        {
            const string query = "INSERT INTO articulos (codigo, nombre, categoria, precio_compra, precio_venta, stock_actual, stock_minimo) VALUES (@codigo, @nombre, @categoria, @compra, @venta, @stock, @minimo)";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    AgregarParametros(comando, articulo);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex) { throw new InvalidOperationException("No se pudo guardar el artículo. Verifique que el código no esté repetido y que la categoría sea válida.", ex); }
            finally { conexion.CierraConexion(); }
        }

        public bool ActualizarArticulo(Articulo articulo)
        {
            const string query = "UPDATE articulos SET codigo = @codigo, nombre = @nombre, categoria = @categoria, precio_compra = @compra, precio_venta = @venta, stock_actual = @stock, stock_minimo = @minimo WHERE id_articulo = @id";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    AgregarParametros(comando, articulo);
                    comando.Parameters.AddWithValue("@id", articulo.IdArticulo);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex) { throw new InvalidOperationException("No se pudo actualizar el artículo. Verifique que el código no esté repetido y que la categoría sea válida.", ex); }
            finally { conexion.CierraConexion(); }
        }

        public bool EliminarArticulo(int id)
        {
            const string query = "DELETE FROM articulos WHERE id_articulo = @id";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex) { throw new InvalidOperationException("No se pudo eliminar el artículo porque tiene registros relacionados o ocurrió un error en la base de datos.", ex); }
            finally { conexion.CierraConexion(); }
        }

        private static void AgregarParametros(MySqlCommand comando, Articulo articulo)
        {
            comando.Parameters.AddWithValue("@codigo", articulo.Codigo);
            comando.Parameters.AddWithValue("@nombre", articulo.Nombre);
            comando.Parameters.AddWithValue("@categoria", articulo.Categoria);
            comando.Parameters.AddWithValue("@compra", articulo.PrecioCompra);
            comando.Parameters.AddWithValue("@venta", articulo.PrecioVenta);
            comando.Parameters.AddWithValue("@stock", articulo.StockActual);
            comando.Parameters.AddWithValue("@minimo", articulo.StockMinimo);
        }

        public Articulo ObtenerArticuloPorId(int id)
        {
            const string query = "SELECT id_articulo, codigo, nombre, categoria, precio_compra, precio_venta, stock_actual, stock_minimo FROM articulos WHERE id_articulo = @id";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            string Codigo = lector.GetString("codigo");
                            string Nombre = lector.GetString("nombre");
                            string Categoria = lector.GetString("categoria");
                            decimal PrecioCompra = lector.GetDecimal("precio_compra");
                            decimal PrecioVenta = lector.GetDecimal("precio_venta");
                            int StockActual = lector.GetInt32("stock_actual");
                            int StockMinimo = lector.GetInt32("stock_minimo");
                            return new Articulo(Codigo, Nombre, Categoria, PrecioCompra, PrecioVenta, StockActual, StockMinimo);
                        }
                    }
                }
            }
            catch (MySqlException ex) { throw new InvalidOperationException("No se pudo obtener el artículo.", ex); }
            finally { conexion.CierraConexion(); }
            throw new InvalidOperationException("No se encontró un artículo con el ID proporcionado."); 
        }
    }
}
