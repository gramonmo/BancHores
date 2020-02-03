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

        public MySqlDataReader Select(string query)
        {
            try
            {
                MySqlConnection conexionDB = new MySqlConnection(datosBBDD.stringConexion);
                MySqlCommand comandoDB = new MySqlCommand(query, conexionDB);
                comandoDB.CommandTimeout = 60;
                MySqlDataReader reader;

                conexionDB.Open();
                reader = comandoDB.ExecuteReader();
                return reader;
            }
            catch
            {
                MessageBox.Show("No se ha podido acceder a la base de datos.");
                return null;
            }
        }


        //public string[] Select2(string query)
        //{
        //    try
        //    {

        //        MySqlConnection conexionDB = new MySqlConnection(datosBBDD.stringConexion);
        //        MySqlCommand comandoDB = new MySqlCommand(query, conexionDB);
        //        comandoDB.CommandTimeout = 60;
        //        MySqlDataReader reader;

        //        conexionDB.Open();
        //        reader = comandoDB.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                string[] fila = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5),
        //                reader.GetString(6), reader.GetString(7), reader.GetString(8)};
        //            }
        //        }
        //        return fila;
        //    }
        //    catch
        //    {
        //        MessageBox.Show("No se ha podido acceder a la base de datos.");
        //        return null;
        //    }
        //}
        public void Insert_Update(string query)
        {
            try
            {
                MySqlConnection conexionDB = new MySqlConnection(datosBBDD.stringConexion);
                MySqlCommand comandoDB = new MySqlCommand(query, conexionDB);
                comandoDB.CommandTimeout = 60;

                conexionDB.Open();
                comandoDB.ExecuteReader();
                conexionDB.Close();
            }
            catch
            {
                MessageBox.Show("No se ha podido acceder a la base de datos");
            }
        }
    }
}
