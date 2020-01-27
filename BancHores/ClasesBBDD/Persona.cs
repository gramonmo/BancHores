using BancHores.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancHores.ClasesBBDD
{
    public class Persona
    {
        public string nombre { get; set; }
        public double bancoHoras { get; set; }
        public double horasDeuda { get; set; }
        public double horasSemana { get; set; }
        public double horasMes { get; set; }
        public double aTrabajarSemana { get; set; }

        ControlArchivos ctrlArchivos = new ControlArchivos();

        public Persona()
        {
            ObtenerdatosPersona();
        }

        public void ObtenerdatosPersona()
        {
            string[] datos = File.ReadAllLines("Usuario.txt");
            int contador = 0;
            foreach (var linea in datos)
            {
                string valor = linea.Split(':')[1];
                if (contador==1)
                {
                    horasMes = double.Parse(valor);
                }
                if (contador == 2)
                {
                    horasSemana = double.Parse(valor);
                }
                if (contador == 3)
                {
                    bancoHoras = double.Parse(valor);
                }
                if (contador == 4)
                {
                    horasDeuda = double.Parse(valor);
                }
                contador++;
            }
        }

        public void ActualizarInfoPersona(double totalHorasDia)
        {
            double horasMesTemp = horasMes;
            double horasSemanaTemp = horasSemana;

            horasMes += totalHorasDia;
            horasSemana += totalHorasDia;

            ctrlArchivos.ActualizarDocumentoUsuario(horasMesTemp, horasSemanaTemp, horasMes, horasSemana);

            horasMes = 0;
        }
    }
}
