using System;
using System.IO;
using System.Linq;
using BancHores.Ventanas_Auxiliares;

namespace BancHores.Clases
{
    class ControlArchivos
    {
        public bool ComprovarArchivos()
        {
            if (!Directory.Exists(PathGlobal.pathData))
            {
                Directory.CreateDirectory(PathGlobal.pathData);
            }

            bool faltaUsuario = false;
            if (!File.Exists($@"{PathGlobal.pathData}\Entradas.txt"))
            {
                File.Create($@"{PathGlobal.pathData}\Entradas.txt");
            }
            if (!File.Exists($@"{PathGlobal.pathData}\Salidas.txt"))
            {
                File.Create($@"{PathGlobal.pathData}\Salidas.txt");
            }
            if (!File.Exists($@"{PathGlobal.pathData}\Pausas.txt"))
            {
                File.Create($@"{PathGlobal.pathData}\Pausas.txt");
            }
            if (!File.Exists($@"{PathGlobal.pathData}\Usuario.txt"))
            {
                File.Create($@"{PathGlobal.pathData}\Usuario.txt");
                faltaUsuario = true;              
            }
            if (!File.Exists($@"{PathGlobal.pathData}\Registro.txt"))
            {
                File.Create($@"{PathGlobal.pathData}\Registro.txt");
            }
            return faltaUsuario;
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
