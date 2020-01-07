using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BancHores.Clases
{
    public class MetodosGenerales
    {
        public void InsertarFecha(Label label)
        {
            DateTime fecha = DateTime.Now;
            string dia = fecha.DayOfWeek.ToString();

            switch (dia)
            {
                case "Monday":
                    dia = "Lunes";
                    break;
                case "Tuesday":
                    dia = "Martes";
                    break;
                case "Wednesday":
                    dia = "Miércoles";
                    break;
                case "Thursday":
                    dia = "Jueves";
                    break;
                case "Friday":
                    dia = "Viernes";
                    break;
                case "Saturday":
                    dia = "Sábado";
                    break;
                case "Sunday":
                    dia = "Domingo";
                    break;
            }

            label.Content = $"{dia} {fecha.ToString("dd/mm/yyyy")}";
        }


    }
}
