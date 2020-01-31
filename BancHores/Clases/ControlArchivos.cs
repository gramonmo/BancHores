using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BancHores.Ventanas_Auxiliares;

namespace BancHores.Clases
{
    class ControlArchivos
    {
        Calculos_Comp calc_comp = new Calculos_Comp();

        public void ComprovarArchivos()
        {
            if (!Directory.Exists(PathGlobal.pathData))
            {
                Directory.CreateDirectory(PathGlobal.pathData);
            }
            if (!File.Exists($@"{PathGlobal.pathData}\Usuario.txt"))
            {
                File.Create($@"{PathGlobal.pathData}\Usuario.txt");             
            }         
        }

        // Registra la hora del marcaje de la jornada y lo muestra en el label en función del flag
        // (flag=0 entrada, flag=1 salida, flag=2 pausa, flag=3 continuar)
        public void RegistrarMarcaje(DateTime fechaYHora, Label lbEscritura, int flag)
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
                ctrlArchivos.EscribirEntradaSalida($@"{PathGlobal.pathData}\Entradas.txt", fechaYHoraStr);
            }
            else if (flag == 1) // Salida
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Salida: {hora}";
                ctrlArchivos.EscribirEntradaSalida($@"{PathGlobal.pathData}\Salidas.txt", fechaYHoraStr);

            }
            else if (flag == 2) // Pausa
            {
                jornada.entrada = fechaYHora;
                lbEscritura.Content = $"Inicio pausa: {hora}";
                ctrlArchivos.EscribirPausaContinuar($@"{PathGlobal.pathData}\Pausas.txt", fechaYHoraStr, 0);
            }
            else // Continuar
            {
                jornada.salida = fechaYHora;
                lbEscritura.Content = $"Fin pausa: {hora}";
                ctrlArchivos.EscribirPausaContinuar($@"{PathGlobal.pathData}\Pausas.txt", fechaYHoraStr, 1);
            }
        }

        public void EscribirEntradaSalida(string path, string fechaYHora)
        {
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(fechaYHora);
            sw.Close();
        }

        // flag=0 Pausa, flag=1 continuar.
        public void EscribirPausaContinuar(string path, string fechaYHora, int flag)
        {
            StreamWriter sw = File.AppendText(path);
            if (flag==0) // Pausa
            {
                sw.Write($"{fechaYHora} - " );
            }
            else // Continuar
            {
                sw.WriteLine(fechaYHora);
            }
            sw.Close();
        }

        public void EscribirPausaCompleta(string path, string pausaCompleta)
        {
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(pausaCompleta);
            sw.Close();
        }

        public string LeerUltimoRegistro(string path)
        {
            try
            {
                return File.ReadLines(path).Last();
            }
            catch
            {
                return "";
            }      
        }

        public void RegistrarJornada(string fecha, double horas)
        {
            horas = Math.Round(horas, 2);
            StreamWriter sw = File.AppendText($@"{PathGlobal.pathData}\Registro.txt");
            sw.WriteLine($"{fecha}: {horas}");
            sw.Close();
        }

        public void ActualizarHorasUsuario(double horasMesViejas, double horasSemanaViejas, double horasMesNuevas, double horasSemanaNuevas)
        {
            horasMesNuevas = Math.Round(horasMesNuevas, 2);
            horasSemanaNuevas = Math.Round(horasSemanaNuevas, 2);

            string datos = File.ReadAllText($@"{PathGlobal.pathData}\Usuario.txt");
            datos = datos.Replace($"Horas este mes: {horasMesViejas.ToString()}", $"Horas este mes: {horasMesNuevas.ToString()}");
            datos = datos.Replace($"Horas esta semana: {horasSemanaViejas.ToString()}", $"Horas esta semana: {horasSemanaNuevas.ToString()}");
            File.WriteAllText($@"{PathGlobal.pathData}\Usuario.txt", datos);
        }
        
        public void EliminarUltimoRegistro(string archivo)
        {
            string[] linias = File.ReadAllLines(archivo);
            File.WriteAllLines(archivo, linias.Take(linias.Length - 1).ToArray());
        }
    }
}
