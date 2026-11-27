using System;

namespace Entidad
{
    public class DetalleVenta
    {
        public int id_detalle_venta { get; set; }
        public int id_venta { get; set; }
        public int id_articulo { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }

        public DetalleVenta() { }

        public DetalleVenta(int idArticulo, int cantidad, decimal precioUnitario)
        {
            if (idArticulo <= 0) throw new ArgumentException("El artículo debe ser válido.");
            if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");
            if (precioUnitario < 0m) throw new ArgumentException("El precio unitario no puede ser negativo.");
            id_articulo = idArticulo;
            this.cantidad = cantidad;
            precio_unitario = precioUnitario;
        }

        public decimal CalcularSubtotal()
        {
            return cantidad * precio_unitario;
        }
    }
}
