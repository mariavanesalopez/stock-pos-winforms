using Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ClienteRepositorio
    {
        Conexion conexion = new Conexion();

        public DataTable ListarClientes()
        {
            DataTable tabla = new DataTable();
            string query = "SELECT * FROM clientes";

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
        public bool ExisteDni(string dni) 
        {
            int count = 0;
            string query = "SELECT COUNT(1) FROM Clientes WHERE Dni = @Dni";

            // Usamos un bloque "using" para la conexión obtenida, asegurando que se cierre sí o sí al terminar
            
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    comando.Parameters.AddWithValue("@Dni", dni);

                    // ExecuteScalar devuelve un object, lo convertimos a int de forma segura
                    count = Convert.ToInt32(comando.ExecuteScalar());
                }

            // Si el count es mayor a 0, devuelve true; de lo contrario, false
            return count > 0;
        }

        public bool ExisteId(int id)
        {
            int count = 0;
            string query = "SELECT COUNT(1) FROM Clientes WHERE id_cliente = @id";

            // Usamos un bloque "using" para la conexión obtenida, asegurando que se cierre sí o sí al terminar

            using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
            {
                comando.Parameters.AddWithValue("@id", id);

                // ExecuteScalar devuelve un object, lo convertimos a int de forma segura
                count = Convert.ToInt32(comando.ExecuteScalar());
            }

            // Si el count es mayor a 0, devuelve true; de lo contrario, false
            return count > 0;
        }

        public bool CrearCliente(Cliente nuevoCliente)
        {
            string query = "INSERT INTO clientes (nombre, dni, telefono, direccion) VALUES (@nombre, @dni, @telefono, @direccion)";

            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    // Asignamos los valores del objeto a los parámetros de la consulta
                    comando.Parameters.AddWithValue("@nombre", nuevoCliente.Nombre);
                    comando.Parameters.AddWithValue("@dni", nuevoCliente.Dni);
                    comando.Parameters.AddWithValue("@telefono", nuevoCliente.Telefono);
                    comando.Parameters.AddWithValue("@direccion", nuevoCliente.Direccion);

                    // Ejecutamos la consulta de inserción (devuelve cantidad de filas afectadas)
                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0; // Retorna true si se guardó correctamente
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar cliente: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.CierraConexion();
            }
        }

        public bool ActualizarCliente(Cliente clienteActualizado)
        {
            string query = "UPDATE clientes SET Nombre = @Nombre, Dni = @Dni, Telefono = @Telefono, Direccion = @Direccion WHERE Dni = @Dni";

            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    // Asignamos los valores del objeto a los parámetros de la consulta
                    comando.Parameters.AddWithValue("@nombre", clienteActualizado.Nombre);
                    comando.Parameters.AddWithValue("@dni", clienteActualizado.Dni);
                    comando.Parameters.AddWithValue("@telefono", clienteActualizado.Telefono);
                    comando.Parameters.AddWithValue("@direccion", clienteActualizado.Direccion);

                    // Ejecutamos la consulta de modificacion (devuelve cantidad de filas afectadas)
                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0; // Retorna true si se guardó correctamente
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar cliente: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.CierraConexion();
            }
        }

        public bool EliminarCliente(int id)
        {
            string query = "DELETE FROM clientes WHERE id_cliente = @id";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    // Asignamos el valor del ID a los parámetros de la consulta
                    comando.Parameters.AddWithValue("@id", id);
                    // Ejecutamos la consulta de eliminación (devuelve cantidad de filas afectadas)
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0; // Retorna true si se eliminó correctamente
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar cliente: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.CierraConexion();
            }
        }
    }
}
