using System;
using System.Text.RegularExpressions;

namespace Entidad
{
    public class Articulo
    {
        private int _idArticulo;
        private string _codigo;
        private string _nombre;
        private string _categoria;
        private decimal _precioCompra;
        private decimal _precioVenta;
        private int _stockActual;
        private int _stockMinimo;

        public Articulo(string codigo, string nombre, string categoria, decimal precioCompra, decimal precioVenta, int stockActual, int stockMinimo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Categoria = categoria;
            PrecioCompra = precioCompra;
            PrecioVenta = precioVenta;
            StockActual = stockActual;
            StockMinimo = stockMinimo;
        }

        public int IdArticulo
        {
            get { return _idArticulo; }
            set { if (value <= 0) throw new ArgumentException("El ID del artículo debe ser un número entero positivo."); _idArticulo = value; }
        }

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("El código del artículo no puede estar vacío.");
                if (value.Trim().Length > 20) throw new ArgumentException("El código no puede superar los 20 caracteres.");
                _codigo = value.Trim();
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("El nombre del artículo no puede estar vacío.");
                if (value.Trim().Length > 150) throw new ArgumentException("El nombre no puede superar los 150 caracteres.");
                _nombre = value.Trim();
            }
        }

        public string Categoria
        {
            get { return _categoria; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Debe seleccionar una categoría.");
                if (value.Trim().Length > 30) throw new ArgumentException("El nombre de categoría no puede superar los 30 caracteres.");
                _categoria = value.Trim();
            }
        }

        public decimal PrecioCompra
        {
            get { return _precioCompra; }
            set { if (value < 0) throw new ArgumentException("El precio de compra no puede ser negativo."); _precioCompra = value; }
        }

        public decimal PrecioVenta
        {
            get { return _precioVenta; }
            set { if (value < 0) throw new ArgumentException("El precio de venta no puede ser negativo."); _precioVenta = value; }
        }

        public int StockActual
        {
            get { return _stockActual; }
            set { if (value < 0) throw new ArgumentException("El stock actual no puede ser negativo."); _stockActual = value; }
        }

        public int StockMinimo
        {
            get { return _stockMinimo; }
            set { if (value < 0) throw new ArgumentException("El stock mínimo no puede ser negativo."); _stockMinimo = value; }
        }
    }
}
