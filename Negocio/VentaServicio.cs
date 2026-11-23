using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class VentaServicio
    {
        private readonly VentaRepositorio repositorio = new VentaRepositorio();

        public bool RegistrarVenta(DateTime fechaHora, int? idCliente, IList<DetalleVenta> detalles)
        {
            if (detalles == null || detalles.Count == 0) throw new InvalidOperationException("Agregue al menos un artículo antes de cobrar.");
            if (idCliente.HasValue && idCliente.Value <= 0) throw new ArgumentException("El cliente seleccionado no es válido.");
            if (detalles.Any(detalle => detalle == null || detalle.id_articulo <= 0 || detalle.cantidad <= 0 || detalle.precio_unitario < 0m))
                throw new ArgumentException("El detalle contiene un artículo, cantidad o precio inválido.");

            List<DetalleVenta> detallesNormalizados = detalles.GroupBy(detalle => detalle.id_articulo)
                .Select(grupo => new DetalleVenta(grupo.Key, grupo.Sum(detalle => detalle.cantidad), grupo.First().precio_unitario))
                .ToList();

            Venta nuevaVenta = new Venta
            {
                fecha_hora = fechaHora == default(DateTime) ? DateTime.Now : fechaHora,
                id_cliente = idCliente,
                detalles = detallesNormalizados,
                total = detallesNormalizados.Sum(detalle => detalle.CalcularSubtotal()),
                estado = "COMPLETADA"
            };
            return repositorio.CrearVenta(nuevaVenta) > 0;
        }

        public List<Venta> ListarVentas() { return repositorio.ObtenerTodas(); }
        public Venta BuscarVenta(int idVenta) { return repositorio.BuscarPorId(idVenta); }
        public List<DetalleVenta> ListarDetalles(int idVenta) { return repositorio.ObtenerDetallesPorVenta(idVenta); }
        public TicketVenta ObtenerTicket(int idVenta)
        {
            if (idVenta <= 0) throw new ArgumentException("El número de venta no es válido.");
            TicketVenta ticket = repositorio.ObtenerTicket(idVenta);
            if (ticket == null) throw new InvalidOperationException("No se encontró la venta para generar el ticket.");
            return ticket;
        }

        public bool AnularVenta(int idVenta)
        {
            if (idVenta <= 0) return false;
            return repositorio.AnularVentaYDevolverStock(idVenta);
        }
    }
}
