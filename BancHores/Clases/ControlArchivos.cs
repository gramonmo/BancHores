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

    }
}
