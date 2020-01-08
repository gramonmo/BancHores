using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancHores.ClasesBBDD
{
    public class Pausas
    {
        public DateTime fecha { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public int pausaTotal { get; set; }

        public Pausas(DateTime fecha, DateTime inicio, DateTime fin, int pausaTotal)
        {
            this.fecha = fecha;
            this.inicio = inicio;
            this.fin = fin;
            this.pausaTotal = pausaTotal;
        }

    }

}
