using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClienteServicio
    {
        ClienteRepositorio clienteRepositorio = new ClienteRepositorio();

        public DataTable ListarClientes() 
        {
            if (clienteRepositorio.ListarClientes().Rows.Count == 0)
            {
                throw new InvalidOperationException("No existen clientes cargados.");
            }

            return clienteRepositorio.ListarClientes();
        }

        public bool GuardarCliente(string nombre, string dni, string telefono, string direccion) 
        {
            if (clienteRepositorio.ExisteDni(dni))
            {
                throw new InvalidOperationException("Ya existe un cliente registrado con el mismo DNI.");
            }

            Cliente nuevoCliente = new Cliente(nombre, dni, telefono, direccion);
            
            return clienteRepositorio.CrearCliente(nuevoCliente);
        }

        public bool ActualizarCliente(int id, string nombre, string dni, string telefono, string direccion)
        {
            if (!clienteRepositorio.ExisteId(id))
            {
                throw new InvalidOperationException("No existe un cliente con el ID proporcionado.");
            }

            Cliente clienteActualizado = new Cliente(nombre, dni, telefono, direccion);
            return clienteRepositorio.ActualizarCliente(clienteActualizado);
        }

        public bool EliminarCliente(int id)
        {
            if (!clienteRepositorio.ExisteId(id))
            {
                throw new InvalidOperationException("No existe un cliente con el ID proporcionado.");
            }
            return clienteRepositorio.EliminarCliente(id);
        }
    }
}
