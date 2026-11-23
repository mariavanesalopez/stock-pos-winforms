using System;
using System.Collections.Generic;

namespace Entidad
{
    public class TicketVenta
    {
        public int IdVenta { get; set; }
        public DateTime FechaHora { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public List<LineaTicketVenta> Detalles { get; set; } = new List<LineaTicketVenta>();
    }

    public class LineaTicketVenta
    {
        public string Articulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get { return Cantidad * PrecioUnitario; } }
    }
}
