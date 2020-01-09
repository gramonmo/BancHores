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

        public void EscribirEntradaSalida(string path, DateTime fechaYHora)
        {
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(fechaYHora);
        }

        // flag=0 Pausa, flag=1 continuar.
        public void EscribirPausaContinuar(string path, DateTime fechaYHora, int flag)
        {
            StreamWriter sw = File.AppendText(path);
            if (flag==0)
            {
                sw.Write($"{fechaYHora} -> " );
            }
            else
            {
                sw.WriteLine(fechaYHora);
            }
        }

        public string LeerUltimoRegistro(string path)
        {
            return File.ReadLines(path).Last();
        }

    }
}
