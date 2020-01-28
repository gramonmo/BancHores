using BancHores.ClasesBBDD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancHores.Clases
{
    class ControlArchivos
    {
        public void ComprovarArchivos()
        {
            if (!File.Exists("Entradas.txt"))
            {
                File.Create("Entradas.txt");
            }
            if (!File.Exists("Salidas.txt"))
            {
                File.Create("Salidas.txt");
            }
            if (!File.Exists("Pausas.txt"))
            {
                File.Create("Pausas.txt");
            }
            if (!File.Exists("Usuario.txt"))
            {
                File.Create("Usuario.txt");
            }
            if (!File.Exists("Registro.txt"))
            {
                File.Create("Registro.txt");
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

        public string LeerUltimoRegistro(string path)
        {
            return File.ReadLines(path).Last();
        }

        public void RegistrarJornada(string fecha, double horas)
        {
            horas = Math.Round(horas, 2);
            StreamWriter sw = File.AppendText("Registro.txt");
            sw.WriteLine($"{fecha}: {horas}");
            sw.Close();
        }

        public void ActualizarHorasUsuario(double horasMesViejas, double horasSemanaViejas, double horasMesNuevas, double horasSemanaNuevas)
        {
            horasMesNuevas = Math.Round(horasMesNuevas, 2);
            horasSemanaNuevas = Math.Round(horasSemanaNuevas, 2);

            string datos = File.ReadAllText("Usuario.txt");
            datos = datos.Replace($"Horas este mes: {horasMesViejas.ToString()}", $"Horas este mes: {horasMesNuevas.ToString()}");
            datos = datos.Replace($"Horas esta semana: {horasSemanaViejas.ToString()}", $"Horas esta semana: {horasSemanaNuevas.ToString()}");
            File.WriteAllText("Usuario.txt", datos);
        }
        
        public void EliminarUltimoRegistro(string archivo)
        {
            string[] linias = File.ReadAllLines(archivo);
            File.WriteAllLines(archivo, linias.Take(linias.Length - 1).ToArray());
        }
    }
}
