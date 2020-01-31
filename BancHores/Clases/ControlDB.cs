using BancHores.ClasesBBDD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BancHores.Clases
{
    class ControlDB
    {

        public MySqlDataReader LanzarSelect(string query)
        {
            try
            {
                MySqlConnection conexionDB = new MySqlConnection(datosBBDD.nombreDB);
                MySqlCommand comandoDB = new MySqlCommand(query, conexionDB);
                comandoDB.CommandTimeout = 60;
                MySqlDataReader reader;

                conexionDB.Open();
                reader = comandoDB.ExecuteReader();
                conexionDB.Close();
                return reader;
            }
            catch
            {
                MessageBox.Show("No se ha podido acceder a la base de datos.");
                return null;
            }
        }


    }
}
