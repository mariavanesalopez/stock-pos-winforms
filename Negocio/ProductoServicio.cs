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
    public class ProductoServicio
    {
        ProductoRepositorio repositorio = new ProductoRepositorio();

        public DataTable ObtenerAlumnos()
        {
            // Aquí podrías validar cosas antes de devolver los datos si fuera necesario
            return repositorio.ListarProductos();
        }

        public bool CrearProducto(string nombre, string descripcion, double precio, int existencia) 
        {
            Producto nuevoProducto = new Producto();
            nuevoProducto.codigo = repositorio.ObtenerUltimoId() + 1;
            nuevoProducto.nombre = nombre;
            nuevoProducto.descripcion = descripcion;
            nuevoProducto.precio = precio;
            nuevoProducto.existencia = existencia;

            return repositorio.GuardarProducto(nuevoProducto);
        }
    }
}
