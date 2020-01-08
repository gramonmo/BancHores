using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancHores.ClasesBBDD
{
    public class Persona
    {
        public string nombre { get; set; }
        public int bancoHoras { get; set; }
        public int horasDeuda { get; set; }
        public int horasSemana { get; set; }
        public int horasMes { get; set; }

        public Persona(string nombre, int bancoHoras, int horasDeuda, int horasSemana, int horasMes)
        {
            this.nombre = nombre;
            this.bancoHoras = bancoHoras;
            this.horasDeuda = horasDeuda;
            this.horasSemana = horasSemana;
            this.horasMes = horasMes;
        }
    }
}
