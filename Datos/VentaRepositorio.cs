using Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Datos
{
    public class VentaRepositorio
    {
        private readonly Conexion conexion = new Conexion();

        public int CrearVenta(Venta venta)
        {
            if (venta == null) throw new ArgumentNullException(nameof(venta));
            if (venta.detalles == null || venta.detalles.Count == 0) throw new InvalidOperationException("La venta debe incluir al menos un artículo.");

            MySqlConnection conn = conexion.Abrirconexion();
            MySqlTransaction transaccion = null;
            try
            {
                transaccion = conn.BeginTransaction();
                const string insertarVenta = "INSERT INTO ventas (fecha_hora, id_cliente, total, estado) VALUES (@fecha, @cliente, @total, @estado)";
                using (MySqlCommand comando = new MySqlCommand(insertarVenta, conn, transaccion))
                {
                    comando.Parameters.AddWithValue("@fecha", venta.fecha_hora);
                    comando.Parameters.AddWithValue("@cliente", venta.id_cliente.HasValue ? (object)venta.id_cliente.Value : DBNull.Value);
                    comando.Parameters.AddWithValue("@total", venta.total);
                    comando.Parameters.AddWithValue("@estado", venta.estado);
                    comando.ExecuteNonQuery();
                }

                int idVenta;
                using (MySqlCommand comando = new MySqlCommand("SELECT LAST_INSERT_ID()", conn, transaccion))
                    idVenta = Convert.ToInt32(comando.ExecuteScalar());

                const string insertarDetalle = "INSERT INTO detalle_venta (id_venta, id_articulo, cantidad, precio_unitario) VALUES (@venta, @articulo, @cantidad, @precio)";
                const string descontarStock = "UPDATE articulos SET stock_actual = stock_actual - @cantidad WHERE id_articulo = @articulo AND stock_actual >= @cantidad";

                foreach (DetalleVenta detalle in venta.detalles)
                {
                    using (MySqlCommand comando = new MySqlCommand(descontarStock, conn, transaccion))
                    {
                        comando.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        comando.Parameters.AddWithValue("@articulo", detalle.id_articulo);
                        if (comando.ExecuteNonQuery() != 1)
                            throw new InvalidOperationException("No hay stock suficiente para uno de los artículos. La venta no se guardó.");
                    }
                    using (MySqlCommand comando = new MySqlCommand(insertarDetalle, conn, transaccion))
                    {
                        comando.Parameters.AddWithValue("@venta", idVenta);
                        comando.Parameters.AddWithValue("@articulo", detalle.id_articulo);
                        comando.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        comando.Parameters.AddWithValue("@precio", detalle.precio_unitario);
                        comando.ExecuteNonQuery();
                    }
                    detalle.id_venta = idVenta;
                }

                transaccion.Commit();
                venta.id_venta = idVenta;
                return idVenta;
            }
            catch
            {
                if (transaccion != null) { try { transaccion.Rollback(); } catch { } }
                throw;
            }
            finally
            {
                if (transaccion != null) transaccion.Dispose();
                conexion.CierraConexion();
            }
        }

        public List<Venta> ObtenerTodas()
        {
            List<Venta> ventas = new List<Venta>();
            const string query = "SELECT v.id_venta, v.fecha_hora, v.id_cliente, c.nombre AS nombre_cliente, v.total, v.estado FROM ventas v LEFT JOIN clientes c ON v.id_cliente = c.id_cliente ORDER BY v.id_venta DESC";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        int columnaCliente = lector.GetOrdinal("id_cliente");
                        ventas.Add(new Venta { id_venta = lector.GetInt32("id_venta"), fecha_hora = lector.GetDateTime("fecha_hora"), id_cliente = lector.IsDBNull(columnaCliente) ? (int?)null : lector.GetInt32("id_cliente"), nombre_cliente = lector.IsDBNull(lector.GetOrdinal("nombre_cliente")) ? "Consumidor final" : lector.GetString("nombre_cliente"), total = lector.GetDecimal("total"), estado = lector.GetString("estado") });
                    }
                }
                return ventas;
            }
            finally { conexion.CierraConexion(); }
        }

        public Venta BuscarPorId(int idVenta)
        {
            const string query = "SELECT id_venta, fecha_hora, id_cliente, total, estado FROM ventas WHERE id_venta = @id";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    comando.Parameters.AddWithValue("@id", idVenta);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read()) return null;
                        int columnaCliente = lector.GetOrdinal("id_cliente");
                        return new Venta { id_venta = lector.GetInt32("id_venta"), fecha_hora = lector.GetDateTime("fecha_hora"), id_cliente = lector.IsDBNull(columnaCliente) ? (int?)null : lector.GetInt32(columnaCliente), total = lector.GetDecimal("total"), estado = lector.GetString("estado") };
                    }
                }
            }
            finally { conexion.CierraConexion(); }
        }

        public List<DetalleVenta> ObtenerDetallesPorVenta(int idVenta)
        {
            List<DetalleVenta> detalles = new List<DetalleVenta>();
            const string query = "SELECT id_detalle_venta, id_venta, id_articulo, cantidad, precio_unitario FROM detalle_venta WHERE id_venta = @id ORDER BY id_detalle_venta";
            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexion.Abrirconexion()))
                {
                    comando.Parameters.AddWithValue("@id", idVenta);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                        while (lector.Read()) detalles.Add(new DetalleVenta { id_detalle_venta = lector.GetInt32("id_detalle_venta"), id_venta = lector.GetInt32("id_venta"), id_articulo = lector.GetInt32("id_articulo"), cantidad = lector.GetInt32("cantidad"), precio_unitario = lector.GetDecimal("precio_unitario") });
                }
                return detalles;
            }
            finally { conexion.CierraConexion(); }
        }

        public TicketVenta ObtenerTicket(int idVenta)
        {
            TicketVenta ticket = null;
            const string queryVenta = @"SELECT v.id_venta, v.fecha_hora, v.total, v.estado, COALESCE(c.nombre, 'Consumidor final') AS cliente
                                       FROM ventas v LEFT JOIN clientes c ON c.id_cliente = v.id_cliente
                                       WHERE v.id_venta = @id";
            const string queryDetalle = @"SELECT a.nombre AS articulo, d.cantidad, d.precio_unitario
                                         FROM detalle_venta d INNER JOIN articulos a ON a.id_articulo = d.id_articulo
                                         WHERE d.id_venta = @id ORDER BY d.id_detalle_venta";
            MySqlConnection conn = conexion.Abrirconexion();
            try
            {
                using (MySqlCommand comando = new MySqlCommand(queryVenta, conn))
                {
                    comando.Parameters.AddWithValue("@id", idVenta);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read()) return null;
                        ticket = new TicketVenta
                        {
                            IdVenta = lector.GetInt32("id_venta"),
                            FechaHora = lector.GetDateTime("fecha_hora"),
                            Total = lector.GetDecimal("total"),
                            Estado = lector.GetString("estado"),
                            Cliente = lector.GetString("cliente")
                        };
                    }
                }

                using (MySqlCommand comando = new MySqlCommand(queryDetalle, conn))
                {
                    comando.Parameters.AddWithValue("@id", idVenta);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                        while (lector.Read()) ticket.Detalles.Add(new LineaTicketVenta
                        {
                            Articulo = lector.GetString("articulo"),
                            Cantidad = lector.GetInt32("cantidad"),
                            PrecioUnitario = lector.GetDecimal("precio_unitario")
                        });
                }
                return ticket;
            }
            finally { conexion.CierraConexion(); }
        }

        public bool AnularVentaYDevolverStock(int idVenta)
        {
            MySqlConnection conn = conexion.Abrirconexion();
            MySqlTransaction transaccion = null;
            try
            {
                transaccion = conn.BeginTransaction();
                const string estadoActual = "SELECT estado FROM ventas WHERE id_venta = @id FOR UPDATE";
                using (MySqlCommand comando = new MySqlCommand(estadoActual, conn, transaccion))
                {
                    comando.Parameters.AddWithValue("@id", idVenta);
                    object estado = comando.ExecuteScalar();
                    if (estado == null || estado == DBNull.Value || string.Equals(Convert.ToString(estado), "ANULADA", StringComparison.OrdinalIgnoreCase))
                    {
                        transaccion.Rollback();
                        return false;
                    }
                }

                List<DetalleVenta> detalles = new List<DetalleVenta>();
                using (MySqlCommand comando = new MySqlCommand("SELECT id_articulo, cantidad FROM detalle_venta WHERE id_venta = @id", conn, transaccion))
                {
                    comando.Parameters.AddWithValue("@id", idVenta);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                        while (lector.Read()) detalles.Add(new DetalleVenta { id_articulo = lector.GetInt32("id_articulo"), cantidad = lector.GetInt32("cantidad") });
                }

                foreach (DetalleVenta detalle in detalles)
                {
                    using (MySqlCommand comando = new MySqlCommand("UPDATE articulos SET stock_actual = stock_actual + @cantidad WHERE id_articulo = @articulo", conn, transaccion))
                    {
                        comando.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        comando.Parameters.AddWithValue("@articulo", detalle.id_articulo);
                        comando.ExecuteNonQuery();
                    }
                }

                using (MySqlCommand comando = new MySqlCommand("UPDATE ventas SET estado = 'ANULADA' WHERE id_venta = @id", conn, transaccion))
                {
                    comando.Parameters.AddWithValue("@id", idVenta);
                    if (comando.ExecuteNonQuery() != 1) throw new InvalidOperationException("No se pudo anular la venta.");
                }
                transaccion.Commit();
                return true;
            }
            catch
            {
                if (transaccion != null) { try { transaccion.Rollback(); } catch { } }
                throw;
            }
            finally
            {
                if (transaccion != null) transaccion.Dispose();
                conexion.CierraConexion();
            }
        }
    }
}
