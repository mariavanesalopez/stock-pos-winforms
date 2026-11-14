using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Cliente
    {
        private int _idCliente;
        private string _nombre;
        private string _dni;
        private string _telefono;
        private string _direccion;

        public Cliente(string nombre, string dni, string telefono, string direccion)
        {
            this.Nombre = nombre;
            this.Dni = dni;
            this.Telefono = telefono;
            this.Direccion = direccion;
        }

        public int IdCliente
        {
            get { return _idCliente; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El ID del cliente debe ser un número entero positivo.");
                }
                _idCliente = value;
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre del cliente no puede estar vacío.");
                }
                if (value.Trim().Length > 100)
                {
                    throw new ArgumentException("El nombre no puede superar los 100 caracteres.");
                }
                _nombre = value.Trim();
            }
        }

        public string Dni
        {
            get { return _dni; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El DNI no puede estar vacío.");
                }

                string dniLimpio = value.Trim();

                // Validación de formato numérico estándar (ej. 7 u 8 dígitos)
                if (!System.Text.RegularExpressions.Regex.IsMatch(dniLimpio, @"^\d{7,8}$"))
                {
                    throw new ArgumentException("El DNI debe contener solo números y tener entre 7 y 8 dígitos.");
                }

                _dni = dniLimpio;
            }
        }

        public string Telefono
        {
            get { return _telefono; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    string telLimpio = value.Trim();
                    if (!System.Text.RegularExpressions.Regex.IsMatch(telLimpio, @"^[0-9\s\-\+\(\)]+$"))
                    {
                        throw new ArgumentException("El formato del teléfono no es válido.");
                    }
                    _telefono = telLimpio;
                }
                else
                {
                    _telefono = string.Empty;
                }
            }
        }

        public string Direccion
        {
            get { return _direccion; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Trim().Length < 5)
                {
                    throw new ArgumentException("La dirección ingresada es demasiado corta.");
                }
                _direccion = value?.Trim();
            }
        }
    }
}
