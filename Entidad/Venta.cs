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
        public string nombre_cliente { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; } = "COMPLETADA";

        public List<DetalleVenta> detalles { get; set; } = new List<DetalleVenta>();

        public Venta()
        {
        }

        public int Id_venta
        {
            get { return id_venta; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(id_venta), "El ID de venta no puede ser negativo.");

                id_venta = value;
            }
        }

        public DateTime Fecha_hora
        {
            get { return fecha_hora; }
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentException("La fecha y hora de la venta no es válida.", nameof(fecha_hora));

                fecha_hora = value;
            }
        }

        public int? Id_cliente
        {
            get { return id_cliente; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(id_cliente), "El ID de cliente debe ser mayor a 0.");

                id_cliente = value;
            }
        }

        public decimal Total
        {
            get { return total; }
            set
            {
                if (value < 0m)
                    throw new ArgumentOutOfRangeException(nameof(total), "El total de la venta no puede ser negativo.");

                total = value;
            }
        }

        public string Estado        
        {
            get { return estado; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El estado de la venta no puede estar vacío.", nameof(estado));

                string normalizado = value.Trim().ToUpperInvariant();
                if (normalizado != "COMPLETADA" && normalizado != "ANULADA")
                    throw new ArgumentException("Estado de venta no válido. Use COMPLETADA o ANULADA.", nameof(estado));

                estado = normalizado;
            }
        }

        public List<DetalleVenta> Detalles
        {
            get { return detalles; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(detalles), "La lista de detalles no puede ser nula.");

                detalles = value;
            }
        }

        public decimal Subtotal => detalles.Sum(d => d.CalcularSubtotal());
    }
}
