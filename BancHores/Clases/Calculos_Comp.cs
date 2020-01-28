using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

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
            return string.Format($"{fechaYHora.Hour.ToString("00.##")}:{fechaYHora.Minute.ToString("00.##")}");
        }

        // Retorna la hora del string que reciba en función del flag.
        // flag=0 entrada/salida/pausa flag=1 pausa, flag=1 continuar
        public string ObtenerHoraDeString(string fechaYHora, int flag)
        {
            if (flag == 0)
            {
                return fechaYHora.Split(' ')[1];
            }
            else
            {
                return fechaYHora.Split(' ')[4];
            }
        }

        public string SepararHorasYMinutos(double horasTotales)
        {
            int horas = (int)horasTotales;

            double minutosDouble = (horasTotales - horas) * 60;
            int minutosInt = (int)minutosDouble;

            return $"{horas}h {minutosInt}m";
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
        public bool HayPausaEnCurso()
        {
            try
            {
                string ultimaEntrada = File.ReadLines("Pausas.txt").Last();
                if (ultimaEntrada.Length < 25) // Eso significaria que la ultima pausa no está cerrada
                {
                    return true;
                }
                return false;
            }
            catch  // Si no se puede abrir el archivo o no hay nada escrito
            {
                return false;
            }            
        }

        // Calcula las horas de difernecia que hay entre la de inicio y fin
        public double CalcularDiferenciaHoras(string hInicio, string hFin)
        {
            DateTime inicio = DateTime.Parse(hInicio);
            DateTime fin = DateTime.Parse(hFin);

            return (fin - inicio).TotalHours;
        }
        
        // Calcula las horas totales de pausa del dia actual.
        public double SumarPausasDia()
        {
            string fecha = ObtenerFechaDeDateTime(DateTime.Now);           

            string textoPausas = File.ReadAllText("Pausas.txt");
            string[] pausas = textoPausas.Split(new[] { Environment.NewLine }, StringSplitOptions.None); // Para rellenar el array con las lineas del string

            double horasPausas = 0;
            foreach (var pausa in pausas)
            {
                if (pausa.StartsWith(fecha.ToString()))
                {
                    horasPausas += ProcesarPausaCompleta(pausa);
                }
            }
            return horasPausas;            
        }

        // Procesa la pausa que recibe y retorna las horas totales
        public double ProcesarPausaCompleta(string pausaCompleta)
        {
            string hInicio = ObtenerHoraDeString(pausaCompleta, 0);
            string hFin = ObtenerHoraDeString(pausaCompleta, 1);
            return CalcularDiferenciaHoras(hInicio, hFin);
        }

        public bool EsLunes()
        {
            DateTime fecha = DateTime.Today;
            string dia = fecha.DayOfWeek.ToString();
            if (dia == "Monday")
            {
                return true;
            }
            return false;
        }

        public bool EsPrimeroDeMes()
        {
            string fecha = DateTime.Today.ToString("dd/MM/yyyy");
            string diaMes = fecha.Split('/')[0];
            if (diaMes=="01")
            {
                return true;
            }
            return false;
        }

    }
}
