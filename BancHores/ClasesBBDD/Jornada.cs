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
        public DateTime inicioPausa { get; set; }
        public DateTime finPausa { get; set; }
        public int horasTotales { get; set; }
        public string resumen { get; set; }

        ControlArchivos ctrlArchivos = new ControlArchivos();
        Calculos_Comp calculos_Comp = new Calculos_Comp();

        // Retorna un String con la Hora y Minuto del DateTime que recibe como parametro


        // Registra la hora del marcaje de la jornada y lo muestra en el label en función del flag
        // (flag=0 entrada, flag=1 salida, flag=2 pausa, flag=3 continuar)
        public void RegistrarMarcaje(Jornada jornada, DateTime fechaYHora, Label lbEscritura, int flag)
        {
            if (flag < 0 || flag > 3)
            {
                MessageBox.Show("No hay ninguna función para el flag indicado.");
                return;
            }

            string fechaYHoraStr = fechaYHora.ToString("dd/MM/yyyy HH:mm");
            string hora = calculos_Comp.ObtenerHoraDeDateTime(fechaYHora);

            lbEscritura.Visibility = Visibility.Visible;
            if (flag == 0) // Entrada
            {
                jornada.entrada = fechaYHora;
                lbEscritura.Content = $"Entrada: {hora}";
                ctrlArchivos.EscribirEntradaSalida("Entradas.txt", fechaYHoraStr);
            }
            else if (flag == 1) // Salida
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Salida: {hora}";
                ctrlArchivos.EscribirEntradaSalida("Salidas.txt", fechaYHoraStr);

            }
            else if (flag == 2) // Pausa
            {
                jornada.entrada = fechaYHora;
                lbEscritura.Content = $"Inicio pausa: {hora}";
                ctrlArchivos.EscribirPausaContinuar("Pausas.txt", fechaYHoraStr, 0);
            }
            else // Continuar
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Fin pausa: {hora}";
                ctrlArchivos.EscribirPausaContinuar("Pausas.txt", fechaYHoraStr, 1);
            }
        }

        // Obtiene las horas hechas en el dia actual y lo registra en "Registro.txt"
        public double ObtenerJornadaDia()
        {
            string entrada = ctrlArchivos.LeerUltimoRegistro("Entradas.txt");
            string salida = ctrlArchivos.LeerUltimoRegistro("Salidas.txt");
            double jornadaEntradaSalida = calculos_Comp.CalcularDiferenciaHoras(entrada, salida);
            double jornadaPausas = calculos_Comp.SumarPausasDia();
            double jornadaFinal = jornadaEntradaSalida - jornadaPausas;
            ctrlArchivos.RegistrarJornada(DateTime.Now.ToString("dd/MM/yyyy"), jornadaFinal);
            return jornadaFinal;
        }
    }
}
