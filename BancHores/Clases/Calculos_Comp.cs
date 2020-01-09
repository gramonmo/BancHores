using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancHores.Clases
{
    class Calculos_Comp
    {
        // Retorna solamente la fecha del DateTime que recibe
        public string ObtenerFechaDeDateTime(DateTime fechaYHora)
        {
            return fechaYHora.Date.ToString();
        }

        // Retorna solamente la fecha del string que reciba
        public string ObtenerFechaDeString(string fechaYHora)
        {
            return fechaYHora.Substring(0, 10);           
        }

        public string ObtenerHoraDeDateTime(DateTime fechaYHora)
        {
            return string.Format($"{fechaYHora.Hour}:{fechaYHora.Minute}");
        }

        public bool YaHayEntradaEseDia()
        {
            string fecha = DateTime.Now.Date.ToString();
            string ultimaEntrada = File.ReadLines("Entradas.txt").Last();
            if (ObtenerFechaDeString(ultimaEntrada) == fecha)
            {
                return true;
            }
            return false;
        }

    }
}
