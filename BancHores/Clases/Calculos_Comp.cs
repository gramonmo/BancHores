using System;
using System.IO;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Windows;
using BancHores.ClasesBBDD;

namespace BancHores.Clases
{
    class Calculos_Comp
    {
        ControlArchivos ctrlArchivos = new ControlArchivos();
        ControlDB ctrlDB = new ControlDB();
        Persona usuario = new Persona();

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
        // flag=0 entrada/salida/pausa flag=1 continuar
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

        public bool FaltaNumeroTrabajador()
        {
            try
            {
                string numeroTrabajador = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}/Usuario.txt");
                if (numeroTrabajador == "")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return true;
            }
            
        }

        public bool FaltaConfiguracionInicial()
        {
            string query = $"SELECT Horas_a_trabajar_semanalmente, Horas_totales_practicas FROM persona WHERE persona.idPersona='{datosBBDD.idUsuario}'";
            MySqlDataReader datos = ctrlDB.Select(query);
            if (datos.GetString(0)=="0" || datos.GetString(1) == "0")
            {
                return true;
            }
            return false;
        }

        // Comprueba si en el dia actual ya hay una entrada registrada.
        // Retorna true si hay entrada ese dia, false en caso contrario.
        public bool YaHayEntradaEseDia(out string ultimaEntrada)
        {
            try
            {
                string fechaActual = DateTime.Now.ToShortDateString();
                string query = "";
                MySqlDataReader datos = ctrlDB.Select(query);
                ultimaEntrada = ""; // fora
                return true; //fora
            }
            catch
            {
                ultimaEntrada = "";
                return false;
            }
        }

        // Comprueba si la ultima pausa está cerrada o sigue en curso. 
        // Retorna true en caso de que esté abierta, false en caso de cerrada.
        public bool HayPausaEnCurso()
        {
            string ultimaEntrada = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}\Pausas.txt");
            if (ultimaEntrada.Length < 25) // Eso significaria que la ultima pausa no está cerrada
            {
                return true;
            }
            return false;
        }

        public bool YaHaySalidaEseDia()
        {
            try
            {
                string fechaActual = DateTime.Now.ToShortDateString();
                string ultimaSalida = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}\Salidas.txt");
                if (ObtenerFechaDeString(ultimaSalida) == fechaActual)
                {
                    return true;
                }
                return false;
            }
            catch
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

            string textoPausas = File.ReadAllText($@"{PathGlobal.pathData}\Pausas.txt");
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
            if (diaMes == "01")
            {
                return true;
            }
            return false;
        }

        // Comprueba si el dia anterior no tiene la salida marcada
        public bool DiaAnteriorSinSalida()
        {
            try
            {
                string fechaEntradaUltimoDia = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}\Entradas.txt");
                fechaEntradaUltimoDia = ObtenerFechaDeString(fechaEntradaUltimoDia);
                string fechaSalidaUltimoDia = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}\Salidas.txt");
                fechaSalidaUltimoDia = ObtenerFechaDeString(fechaSalidaUltimoDia);
                if (fechaEntradaUltimoDia != fechaSalidaUltimoDia && fechaEntradaUltimoDia != "")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
