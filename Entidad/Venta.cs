using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Venta
    {
        public int id_venta { get; set; }
        public DateTime fecha_hora { get; set; }
        public int? id_cliente { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; } = "COMPLETADA";

        public List<DetalleVenta> detalles { get; set; } = new List<DetalleVenta>();

        public Venta()
        {
        }

        public decimal Subtotal => detalles.Sum(d => d.CalcularSubtotal());
    }
}
