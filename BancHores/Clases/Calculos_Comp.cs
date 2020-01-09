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
            return fechaYHora.ToShortDateString();
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
        
        // Retorna la hora del string que reciba
        public string ObtenerHoraDeDateString(string fechaYHora)
        {
            if(fechaYHora.Length == 18)
            {
                return fechaYHora.Substring(11, 4);
            }
            else
            {
                return fechaYHora.Substring(11, 5);
            }
            
        }

        // Comprueba si en el dia actual ya hay una entrada registrada.
        // Retorna true si hay entrada ese dia, false en caso contrario.
        public bool YaHayEntradaEseDia(out string ultimaEntrada)
        {
            string fecha = DateTime.Now.ToShortDateString();
            try
            {
                ultimaEntrada = File.ReadLines("Entradas.txt").Last();
                if (ObtenerFechaDeString(ultimaEntrada) == fecha)
                {
                    return true;
                }
                return false;
            }
            catch // Si no se puede abrir el archivo o no hay nada escrito
            {
                ultimaEntrada = "";
                return false;
            }         
        }

        // Comprueba si la ultima pausa está cerrada o sigue en curso. 
        // Retorna true en caso de que esté abierta, false en caso de cerrada.
        public bool HayPaysaEnCurso()
        {
            string ultimaEntrada = File.ReadLines("Pausas.txt").Last();
            if (ultimaEntrada.Length < 25) // Eso significaria que la ultima pausa no está cerrada
            {
                return true;
            }
            return false;
        }

    }
}
