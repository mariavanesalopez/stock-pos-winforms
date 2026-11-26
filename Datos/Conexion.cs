using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    class Conexion
    {
        // Declaramos las variables a nivel de clase
        string CadenaConexion;
        public MySqlConnection Conectar;

        public Conexion()
        {
            // Asignamos la cadena e inicializamos la conexión en el constructor

           CadenaConexion = "server=localhost;port=3310;database=punto_venta;user=root;password=;";

           Conectar = new MySqlConnection(CadenaConexion);
        }

        // Método para abrir la conexión
        public MySqlConnection Abrirconexion()
        {
            if (Conectar.State == System.Data.ConnectionState.Closed)
            {
                Conectar.Open();
            }
            return Conectar;
        }

        // Método para cerrar la conexión
        public MySqlConnection CierraConexion()
        {
            if (Conectar.State != System.Data.ConnectionState.Closed)
            {
                Conectar.Close();
            }
            return Conectar;
        }
    }
}

