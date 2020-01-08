using System;
using System.Windows;
using System.Windows.Controls;
using BancHores.Clases;

namespace BancHores.ClasesBBDD
{
    public class Jornada
    {
        public DateTime fecha { get; set; }
        public DateTime entrada { get; set; }
        public DateTime salida { get; set; }
        public int horasTotales { get; set; }
        public string resumen { get; set; }

        // Retorna un String con la Hora y Minuto del DateTime que recibe como parametro
        public string HoraToString(DateTime fechaYHora)
        {
            string hora = $"{fechaYHora.Hour}:{fechaYHora.Minute}";
            return hora;
        }

        // Registra la hora del marcaje de la jornada y lo muestra en el label en función del flag
        // (flag=0 entrada, flag=1 salida, flag=2 pausa, flag=3 continuar)
        public void RegistrarMarcaje(Jornada jornada, DateTime fechaYHora, Label lbEscritura, int flag)
        {
            string hora = HoraToString(fechaYHora);
            lbEscritura.Visibility = Visibility.Visible;
            if (flag == 0)
            {
                jornada.entrada = fechaYHora;
                lbEscritura.Content = $"Entrada: {hora}";
            }
            else if (flag == 1)
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Salida: {hora}";
            }
            else if (flag == 2)
            {
                jornada.entrada = fechaYHora;
                lbEscritura.Content = $"Inicio pausa: {hora}";
            }
            else
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Fin pausa: {hora}";
            }
        }

        public float CalcularJornada(DateTime horaEntrada, DateTime horaSalida, float pausaTotal)
        {
            return 0;
        }


    }
}
