using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VentaServicio
    {
        private VentaRepositorio repositorio = new VentaRepositorio();

        /*public bool RegistrarVenta(DateTime fechaHora, int? idCliente, decimal total, List<DetalleVenta> detalles, string estado = "COMPLETADA")
        {
            if (detalles != null && detalles.Any())
            {
                total = detalles.Sum(d => d.CalcularSubtotal());
            }

            if (total < 0)
                return false;

            Venta nuevaVenta = new Venta
            {
                fecha_hora = fechaHora == default ? DateTime.Now : fechaHora,
                id_cliente = idCliente,
                total = total,
                estado = string.IsNullOrWhiteSpace(estado) ? "COMPLETADA" : estado,
                detalles = detalles ?? new List<DetalleVenta>()
            };

            int idCreado = repositorio.CrearVenta(nuevaVenta);

            foreach (var detalle in nuevaVenta.detalles)
            {
                detalle.id_venta = idCreado;
                repositorio.CrearDetalleVenta(detalle);
            }

            return idCreado > 0;
        }*/

        public int ObtenerUltimoIdVenta()
        {
            return repositorio.ObtenerUltimoIdVenta() + 1;
        }

        public List<Venta> ListarVentas()
        {
            return repositorio.ObtenerTodas();
        }

        public Venta BuscarVenta(int idVenta)
        {
            return repositorio.BuscarPorId(idVenta);
        }

        public bool ModificarVenta(int idVenta, DateTime fechaHora, int? idCliente, decimal total, string estado)
        {
            if (total < 0)
                return false;

            Venta venta = new Venta
            {
                id_venta = idVenta,
                fecha_hora = fechaHora,
                id_cliente = idCliente,
                total = total,
                estado = estado
            };

            return repositorio.ActualizarVenta(venta);
        }

        public bool AnularVenta(int idVenta)
        {
            if (idVenta <= 0)
                return false;

            Venta venta = repositorio.BuscarPorId(idVenta);
            if (venta == null)
                return false;

            if (string.Equals(venta.estado, "ANULADA", StringComparison.OrdinalIgnoreCase))
                return false;

            return repositorio.AnularVentaYDevolverStock(idVenta);
        }

        public bool EliminarVenta(int idVenta)
        {
            return repositorio.EliminarVenta(idVenta);
        }
    }
}
