using Datos;
using Entidad;
using System;
using System.Data;

namespace Negocio
{
    public class ArticuloServicio
    {
        private readonly ArticuloRepositorio articuloRepositorio = new ArticuloRepositorio();

        public DataTable ListarArticulos()
        {
            DataTable articulos = articuloRepositorio.ListarArticulos();
            if (articulos.Rows.Count == 0) throw new InvalidOperationException("No existen artículos cargados.");
            return articulos;
        }

        public DataTable ListarCategorias()
        {
            DataTable categorias = articuloRepositorio.ListarCategorias();
            if (categorias.Rows.Count == 0) throw new InvalidOperationException("No existen categorías cargadas. Registre una categoría antes de guardar artículos.");
            return categorias;
        }

        public bool GuardarArticulo(string codigo, string nombre, string categoria, decimal precioCompra, decimal precioVenta, int stockActual, int stockMinimo)
        {
            Articulo nuevoArticulo = new Articulo(codigo, nombre, categoria, precioCompra, precioVenta, stockActual, stockMinimo);
            if (nuevoArticulo.Categoria.Length > 30) throw new ArgumentException("La categoría no puede superar los 30 caracteres.");
            if (articuloRepositorio.ExisteCodigo(nuevoArticulo.Codigo)) throw new InvalidOperationException("Ya existe un artículo registrado con el mismo código.");
            return articuloRepositorio.CrearArticulo(nuevoArticulo);
        }

        public bool ActualizarArticulo(int id, string codigo, string nombre, string categoria, decimal precioCompra, decimal precioVenta, int stockActual, int stockMinimo)
        {
            if (!articuloRepositorio.ExisteId(id)) throw new InvalidOperationException("No existe un artículo con el ID proporcionado.");
            Articulo articulo = new Articulo(codigo, nombre, categoria, precioCompra, precioVenta, stockActual, stockMinimo);
            articulo.IdArticulo = id;
            if (articulo.Categoria.Length > 30) throw new ArgumentException("La categoría no puede superar los 30 caracteres.");
            if (articuloRepositorio.ExisteCodigo(articulo.Codigo, id)) throw new InvalidOperationException("Otro artículo ya está registrado con el mismo código.");
            return articuloRepositorio.ActualizarArticulo(articulo);
        }

        public bool EliminarArticulo(int id)
        {
            if (!articuloRepositorio.ExisteId(id)) throw new InvalidOperationException("No existe un artículo con el ID proporcionado.");
            return articuloRepositorio.EliminarArticulo(id);
        }
    }
}
