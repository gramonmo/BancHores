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
            LeerDocumentoUsuario();
        }



        public void ActualizarInfoPersona(double totalHorasDia)
        {
            double horasMesTemp = horasMes;
            double horasSemanaTemp = horasSemana;

            horasMes += totalHorasDia;
            horasSemana += totalHorasDia;

            ctrlArchivos.ActualizarHorasUsuario(horasMesTemp, horasSemanaTemp, horasMes, horasSemana);

            horasMes = 0;
        }

        // Lee la info de usuario.txt y lo asigna a las propiedades
        public void LeerDocumentoUsuario()
        {
            string[] datos = File.ReadAllLines("Usuario.txt");
            for (int i = 0; i < datos.Length; i++)
            {
                double valor = double.Parse(datos[i].Split(':')[1]);
                switch (i)
                {
                    case 0:
                        aTrabajarSemana = valor;
                        break;
                    case 1:
                        horasMes = valor;
                        break;
                    case 2:
                        horasSemana = valor;
                        break;
                    case 3:
                        bancoHoras = valor;
                        break;
                    case 4:
                        horasDeuda = valor;
                        break;
                }
            }
        }

        // Actualiza el balance de las horas del usuario
        public void CalculoBalanceHoras(double totalHorasDia)
        {
            LeerDocumentoUsuario();
            horasMes += totalHorasDia;
            horasSemana += totalHorasDia;

            if (horasSemana > aTrabajarSemana)
            {
                double horasExtraSemana = horasSemana - aTrabajarSemana;
                if (horasDeuda > 0)
                {
                    horasDeuda -= horasExtraSemana;
                    if (horasDeuda < 0)
                    {
                        bancoHoras += horasDeuda * (-1); // Las horas de mas, las sumamos a bancoHoras *-1 porque estará en negativo
                        horasDeuda = 0;
                    }                   
                }
                else
                {
                    bancoHoras += horasExtraSemana;
                }
            }
            ActualizarDocumentoUsuario();
        }

        public void ActualizarDocumentoUsuario()
        {
            string texto = $"A trabajar semanalmente: {aTrabajarSemana}\nHoras este mes: {Math.Round(horasMes, 2)}\nHoras esta semana: {Math.Round(horasSemana, 2)}\n" +
                $"Acumulado Banco horas: {Math.Round(bancoHoras, 2)}\nHoras deuda: {Math.Round(horasDeuda, 2)}";
            File.WriteAllText("Usuario.txt", texto);
        }
    }
}
