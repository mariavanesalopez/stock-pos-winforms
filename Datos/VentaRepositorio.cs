using Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class VentaRepositorio
    {
        private Conexion conexion = new Conexion();

        // Inserta en ventas y devuelve el id_venta generado
        public int CrearVenta(Venta venta)
        {
            int idGenerado = 0;

            try
            {
                MySqlConnection conn = conexion.Abrirconexion();

                string query = @"INSERT INTO ventas (fecha_hora, id_cliente, total, estado) 
                             VALUES (@fecha_hora, @id_cliente, @total, @estado);
                             SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha_hora", venta.fecha_hora);
                    cmd.Parameters.AddWithValue("@id_cliente", venta.id_cliente.HasValue ? (object)venta.id_cliente.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@total", venta.total);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrWhiteSpace(venta.estado) ? "COMPLETADA" : venta.estado);

                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        idGenerado = id;
                        venta.id_venta = idGenerado;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear venta: " + ex.Message);
            }
            finally
            {
                conexion.CierraConexion();
            }

            return idGenerado;
        }

        public int ObtenerUltimoIdVenta()
        {
            int ultimoId = 0;

            try
            {
                MySqlConnection conn = conexion.Abrirconexion();
                string query = "SELECT MAX(id_venta) FROM ventas;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out int id))
                    {
                        ultimoId = id;
                    }
                    else
                    {
                        ultimoId = 0; // No hay ventas registradas aún
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el último id de venta: " + ex.Message);
            }
            finally
            {
                conexion.CierraConexion();
            }

            return ultimoId;
        }

        public List<Venta> ObtenerTodas()
        {
            List<Venta> lista = new List<Venta>();

            try
            {
                MySqlConnection conn = conexion.Abrirconexion();
                string query = @"SELECT id_venta, fecha_hora, id_cliente, total, estado 
                             FROM ventas 
                             ORDER BY id_venta DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idxCliente = reader.GetOrdinal("id_cliente");

                        lista.Add(new Venta
                        {
                            id_venta = reader.GetInt32("id_venta"),
                            fecha_hora = reader.GetDateTime("fecha_hora"),
                            id_cliente = reader.IsDBNull(idxCliente) ? (int?)null : reader.GetInt32(idxCliente),
                            total = reader.GetDecimal("total"),
                            estado = reader.GetString("estado")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar ventas: " + ex.Message);
            }
            finally
            {
                conexion.CierraConexion();
            }

            return lista;
        }

        public Venta BuscarPorId(int idVenta)
        {
            Venta venta = null;

            try
            {
                MySqlConnection conn = conexion.Abrirconexion();
                string query = @"SELECT id_venta, fecha_hora, id_cliente, total, estado 
                             FROM ventas 
                             WHERE id_venta = @id_venta";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idxCliente = reader.GetOrdinal("id_cliente");

                            venta = new Venta
                            {
                                id_venta = reader.GetInt32("id_venta"),
                                fecha_hora = reader.GetDateTime("fecha_hora"),
                                id_cliente = reader.IsDBNull(idxCliente) ? (int?)null : reader.GetInt32(idxCliente),
                                total = reader.GetDecimal("total"),
                                estado = reader.GetString("estado")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar venta: " + ex.Message);
            }
            finally
            {
                conexion.CierraConexion();
            }

            return venta;
        }

        public bool ActualizarVenta(Venta venta)
        {
            bool actualizado = false;

            try
            {
                MySqlConnection conn = conexion.Abrirconexion();
                string query = @"UPDATE ventas 
                             SET fecha_hora = @fecha_hora, 
                                 id_cliente = @id_cliente, 
                                 total = @total, 
                                 estado = @estado 
                             WHERE id_venta = @id_venta";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fecha_hora", venta.fecha_hora);
                    cmd.Parameters.AddWithValue("@id_cliente", venta.id_cliente.HasValue ? (object)venta.id_cliente.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@total", venta.total);
                    cmd.Parameters.AddWithValue("@estado", venta.estado);
                    cmd.Parameters.AddWithValue("@id_venta", venta.id_venta);

                    actualizado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar venta: " + ex.Message);
            }
            finally
            {
                conexion.CierraConexion();
            }

            return actualizado;
        }

        public List<DetalleVenta> ObtenerDetallesPorVenta(int idVenta)
        {
            List<DetalleVenta> detalles = new List<DetalleVenta>();

            try
            {
                MySqlConnection conn = conexion.Abrirconexion();
                string query = @"SELECT id_detalle_venta, id_venta, id_articulo, cantidad, precio_unitario
                         FROM detalle_venta
                         WHERE id_venta = @id_venta";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detalles.Add(new DetalleVenta
                            {
                                id_detalle_venta = reader.GetInt32("id_detalle_venta"),
                                id_venta = reader.GetInt32("id_venta"),
                                id_articulo = reader.GetInt32("id_articulo"),
                                cantidad = reader.GetInt32("cantidad"),
                                precio_unitario = reader.GetDecimal("precio_unitario")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener detalles de venta: " + ex.Message);
            }
            finally
            {
                conexion.CierraConexion();
            }

            return detalles;
        }

        public bool AnularVentaYDevolverStock(int idVenta)
        {
            List<DetalleVenta> detalles = ObtenerDetallesPorVenta(idVenta);
            MySqlConnection conn = null;
            MySqlTransaction tx = null;

            try
            {
                conn = conexion.Abrirconexion();
                tx = conn.BeginTransaction();

                // 1. Devolver el stock de cada artículo vendido
                string sqlStock = @"UPDATE articulos 
                            SET stock = stock + @cantidad 
                            WHERE id_articulo = @id_articulo";

                foreach (var item in detalles)
                {
                    using (MySqlCommand cmdStock = new MySqlCommand(sqlStock, conn, tx))
                    {
                        cmdStock.Parameters.AddWithValue("@cantidad", item.cantidad);
                        cmdStock.Parameters.AddWithValue("@id_articulo", item.id_articulo);
                        cmdStock.ExecuteNonQuery();
                    }
                }

                // 2. Cambiar el estado de la venta a ANULADA (baja lógica)
                string sqlVenta = @"UPDATE ventas 
                            SET estado = 'ANULADA' 
                            WHERE id_venta = @id_venta";

                int filasAfectadas;
                using (MySqlCommand cmdVenta = new MySqlCommand(sqlVenta, conn, tx))
                {
                    cmdVenta.Parameters.AddWithValue("@id_venta", idVenta);
                    filasAfectadas = cmdVenta.ExecuteNonQuery();
                }

                if (filasAfectadas == 0)
                {
                    tx.Rollback();
                    return false;
                }

                tx.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    tx?.Rollback();
                }
                catch { /* ignorar error de rollback si ya falló la conexión */ }

                Console.WriteLine("Error al anular venta y devolver stock: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.CierraConexion();
            }
        }

        public bool EliminarVenta(int idVenta)
        {
            bool eliminado = false;

            try
            {
                MySqlConnection conn = conexion.Abrirconexion();
                string query = "DELETE FROM ventas WHERE id_venta = @id_venta";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    eliminado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar venta: " + ex.Message);
            }
            finally
            {
                conexion.CierraConexion();
            }

            return eliminado;
        }

        /*public void CrearDetalleVenta(DetalleVenta detalle)
        {
            try
            {
                MySqlConnection conn = conexion.Abrirconexion();
                string query = "INSERT INTO detalle_venta (id_venta, id_articulo, cantidad, precio_unitario) VALUES (@id_venta, @id_articulo, @cantidad, @precio_unitario); SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", detalle.id_venta);
                    cmd.Parameters.AddWithValue("@id_articulo", detalle.id_articulo);
                    cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                    cmd.Parameters.AddWithValue("@precio_unitario", detalle.precio_unitario);

                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        idGenerado = id;
                        detalle.id_detalle_venta = idGenerado;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }*/
    }
}
