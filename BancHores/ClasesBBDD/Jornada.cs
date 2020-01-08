using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancHores.ClasesBBDD
{
    public class Jornada
    {     
        public DateTime fecha { get; set; }
        public DateTime entrada { get; set; }
        public DateTime salida { get; set; }
        public int horasTotales { get; set; }
        public string resumen { get; set; }

        public Jornada(DateTime fecha, DateTime entrada, DateTime salida, int horasTotales, string resumen)
        {
            this.fecha = fecha;
            this.entrada = entrada;
            this.salida = salida;
            this.horasTotales = horasTotales;
            this.resumen = resumen;
        }

    }
}
