using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entidad
{
    public class Proveedores
    {
        private int _idProveedores;
        private string _razonSocial;
        private string _cuit;
        private string _condicionIva;
        private string _telefono;
        private string _email;
        private string _direccion;

        public Proveedores(string razonSocial, string cuit, string condicionIva, string telefono, string email, string direccion)
        {
            RazonSocial = razonSocial;
            Cuit = cuit;
            CondicionIva = condicionIva;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;
        }

        public int IdProveedores
        {
            get { return _idProveedores; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El ID del Proveedores debe ser un número entero positivo.");
                }
                _idProveedores = value;
            }
        }

        public string RazonSocial
        {
            get { return _razonSocial; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre del Proveedores no puede estar vacío.");
                }
                if (value.Trim().Length > 100)
                {
                    throw new ArgumentException("El nombre no puede superar los 100 caracteres.");
                }
                _razonSocial = value.Trim();
            }
        }

        public string Cuit
        {
            get { return _cuit; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El CUIT no puede estar vacío.");
                }

                string cuitLimpio = value.Trim();

                // Validación de formato numérico estándar 
                if (!Regex.IsMatch(cuitLimpio, @"^\d{11}$"))
                {
                    throw new ArgumentException("El CUIT debe contener exactamente 11 dígitos.");
                }

                _cuit = cuitLimpio;
            }
        }



       public string CondicionIva
        {
            get { return _condicionIva; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La condición de IVA no puede estar vacía.");
                }

                _condicionIva = value.Trim();
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


      
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El email no puede estar vacío.");
                }

                string emailLimpio = value.Trim();

                // Validación del formato del email
                if (!System.Text.RegularExpressions.Regex.IsMatch(
                    emailLimpio, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new ArgumentException("El email ingresado no tiene un formato válido.");
                }

                _email = emailLimpio;
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
