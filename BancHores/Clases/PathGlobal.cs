using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancHores.Clases
{
    static class PathGlobal
    {
        public static string pathDocumentos;
        public static string pathData;

        public static void ObtenerPathData()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            userName = userName.Split('\\')[1];
            pathDocumentos = $@"C:\Users\{userName}\Documents";
            pathData = $@"C:\Users\{userName}\Documents\BancHoresData";
        }

    }
}
