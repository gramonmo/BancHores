using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BancHores.ClasesBBDD
{
    static class datosBBDD
    {
        public static string nombreDB="banchoresprova";
        public static string stringConexion = $"datasource=127.0.0.1;port=3306;username=root;password=;database={nombreDB}";

        public static string query = "INSERT INTO persona";

        //public void Insertar()
        //{
        //    MySqlConnection databaseConnection = new MySqlConnection(stringConexion);
        //    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
        //    commandDatabase.CommandTimeout = 60;
        //    MySqlDataReader reader;
        //}

        public static void ConsultaProva()
        {
            MySqlConnection databaseConnection = new MySqlConnection(stringConexion);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            // Abre la base de datos
            databaseConnection.Open();

            // Ejecuta la consultas
            reader = commandDatabase.ExecuteReader();

            // Hasta el momento todo bien, es decir datos obtenidos

            // IMPORTANTE :#
            // Si tu consulta retorna un resultado, usa el siguiente proceso para obtener datos

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), 
                        reader.GetString(6), reader.GetString(7), reader.GetString(8)};
                }
            }
            else
            {
                Console.WriteLine("No se encontraron datos.");
            }

            // Cerrar la conexión
            databaseConnection.Close();
        }
    }
}
