using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Crud.Order.Types;

namespace Negocio
{
    public class ProveedoresServicio
    {
        ProveedoresRepositorio proveedoresRepositorio = new ProveedoresRepositorio();

        public DataTable ListarProveedores() 
        {
            if (proveedoresRepositorio.ListarProveedores().Rows.Count == 0)
            {
                throw new InvalidOperationException("No existen Proveedores cargados.");
            }

            return proveedoresRepositorio.ListarProveedores();
        }

        public bool GuardarProveedores(string razonSocial, string cuit, string condicionIva, string telefono, string email, string direccion) 
        {
            if (proveedoresRepositorio.ExisteCuit(cuit))
            {
                throw new InvalidOperationException("Ya existe un Proveedores registrado con el mismo DNI.");
            }

            Proveedores nuevoProveedores = new Proveedores(razonSocial, cuit, condicionIva, telefono, email, direccion);
            
            return proveedoresRepositorio.CrearProveedores(nuevoProveedores);
        }

       public bool ActualizarProveedores(int id, string razonSocial, string cuit, string condicionIva, string telefono, string email, string direccion)
        {
            if (!proveedoresRepositorio.ExisteId(id))
            {
                throw new InvalidOperationException("No existe un Proveedor con el ID proporcionado.");
            }

            Proveedores ProveedoresActualizado = new Proveedores(razonSocial, cuit, condicionIva, telefono, email, direccion);

            return proveedoresRepositorio.ActualizarProveedores(ProveedoresActualizado);
        }



       

        public bool EliminarProveedores(int id)
        {
            if (!proveedoresRepositorio.ExisteId(id))
            {
                throw new InvalidOperationException("No existe un Proveedor con el ID proporcionado.");
            }
            return proveedoresRepositorio.EliminarProveedores(id);
        }
    }
}
