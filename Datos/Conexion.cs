using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
            CadenaConexion = "server=localhost;port=3306;database=punto_venta;user=root;password=root;";
            Conectar = new MySqlConnection(CadenaConexion);
        }

        public MySqlConnection Abrirconexion()
        {
            try
            {
                if (Conectar.State == System.Data.ConnectionState.Closed)
                {
                    Conectar.Open();
                    Console.WriteLine("Conexion abierta!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al abrir conexion a la base " + e);
            }
            return Conectar;
        }

        // Método para cerrar la conexión de forma segura
        public MySqlConnection CierraConexion()
        {
            try
            {
                if (Conectar.State == System.Data.ConnectionState.Open)
                {
                    Conectar.Close();
                    Console.WriteLine("Conexion Cerrada");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al cerrar la conexión: " + e);
            }
            return Conectar;
        }
    }
}

