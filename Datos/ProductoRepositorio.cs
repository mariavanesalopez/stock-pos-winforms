using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Entidad;

namespace Datos
{
    public class ProductoRepositorio
    {
        Conexion conexion = new Conexion(); // Tu clase de conexión que hiciste antes

        public DataTable ListarProductos()
        {
            DataTable tabla = new DataTable();
            string query = "select * from producto";

            using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
            {
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    tabla.Load(lector); // Carga directamente el DataReader en el DataTable
                }
            }
            conexion.CierraConexion();
            return tabla;
        }

        public bool GuardarProducto(Producto nuevoProducto)
        {
            string query = "INSERT INTO producto (codigo, nombre, descripcion, precio, existencia) VALUES (@codigo, @nombre, @descripcion, @precio, @existencia)";

            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    // Asignamos los valores del objeto a los parámetros de la consulta
                    comando.Parameters.AddWithValue("@codigo", nuevoProducto.codigo);
                    comando.Parameters.AddWithValue("@nombre", nuevoProducto.nombre);
                    comando.Parameters.AddWithValue("@descripcion", nuevoProducto.descripcion);
                    comando.Parameters.AddWithValue("@precio", nuevoProducto.precio);
                    comando.Parameters.AddWithValue("@existencia", nuevoProducto.existencia);

                    // Ejecutamos la consulta de inserción (devuelve las filas afectadas)
                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0; // Retorna true si se guardó correctamente
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar producto: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.CierraConexion();
            }
        }

        public int ObtenerUltimoId() 
        {
            int ultimoId = 0; // Valor por si la tabla está totalmente vacía
            string query = "SELECT MAX(codigo) FROM producto";

            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    // ExecuteScalar devuelve el primer resultado de la primera fila (el MAX)
                    object resultado = comando.ExecuteScalar();

                    // Verificamos que no sea nulo (por ejemplo, si la tabla no tiene alumnos todavía)
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        // Convertimos el resultado a entero
                        ultimoId = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el último ID: " + ex.Message);
            }
            finally
            {
                // Nos aseguramos de cerrar la conexión pase lo que pase
                conexion.CierraConexion();
            }

            return ultimoId;
        }
    }
}
