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
            if (flag < 0 || flag > 3)
            {
                MessageBox.Show("No hay ninguna función para el flag indicado.");
                return;
            }

            string hora = HoraToString(fechaYHora);
            lbEscritura.Visibility = Visibility.Visible;
            ControlArchivos ctrlArchivos = new ControlArchivos();
            if (flag == 0) // Entrada
            {
                jornada.entrada = fechaYHora;
                lbEscritura.Content = $"Entrada: {hora}";
                ctrlArchivos.EscribirEntradaSalida("Entradas.txt", fechaYHora);
            }
            else if (flag == 1) // Salida
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Salida: {hora}";
                ctrlArchivos.EscribirEntradaSalida("Salidas.txt", fechaYHora);
            }
            else if (flag == 2) // Pausa
            {
                jornada.entrada = fechaYHora;
                lbEscritura.Content = $"Inicio pausa: {hora}";
                ctrlArchivos.EscribirPausaContinuar("Pausas.txt", fechaYHora, 0);
            }
            else // Continuar
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Fin pausa: {hora}";
                ctrlArchivos.EscribirPausaContinuar("Pausas.txt", fechaYHora, 1);
            }
        }

        public float CalcularJornada(DateTime horaEntrada, DateTime horaSalida, float pausaTotal)
        {
            return 0;
        }


    }
}
