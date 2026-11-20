using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DetalleVenta
    {
        public int id_detalle_venta { get; set; }
        public int id_venta { get; set; }
        public int id_articulo { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }


        public DetalleVenta()
        {
                
        }

        public decimal CalcularSubtotal() 
        {
            return cantidad * precio_unitario;
        }
    }

    
}
