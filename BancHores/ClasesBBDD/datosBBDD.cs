using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancHores.Clases;
using MySql.Data.MySqlClient;

namespace BancHores.ClasesBBDD
{
    static class datosBBDD
    {
        public static string nombreDB="bhprova";
        public static string stringConexion = $"datasource=127.0.0.1;port=3306;username=root;password=;database={nombreDB}";

        public static string idUsuario;

        static ControlDB ctrlDB = new ControlDB();
        static ControlArchivos ctrlArchivos = new ControlArchivos();

        public static void ObtenerIdUsuario()
        {
            string numeroTrabajador = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}/Usuario.txt");
            string query = $"SELECT persona.idPersona FROM persona WHERE Num_Trabajador='{numeroTrabajador}'";
            idUsuario = ctrlDB.Select(query).ToString();
        }


        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), 
        //                reader.GetString(6), reader.GetString(7), reader.GetString(8)};
        //        }
        //    }
    
    }
}
