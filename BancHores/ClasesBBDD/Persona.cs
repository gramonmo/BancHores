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
        public double horasPracticas { get; set; }

        ControlArchivos ctrlArchivos = new ControlArchivos();
        Jornada jornada = new Jornada();

        public Persona()
        {
            LeerDocumentoUsuario();
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
                    case 5:
                        horasPracticas = valor;
                        break;
                }
            }
        }

        // Actualiza el balance de las horas del usuario
        public void CalculoBalanceHoras(double totalHorasDia)
        {
            LeerDocumentoUsuario();

            horasPracticas -= totalHorasDia;

            double horasExtraAnteriores = horasSemana - aTrabajarSemana;
            if (horasExtraAnteriores < 0)
            {
                horasExtraAnteriores = 0;
            }

            horasMes += totalHorasDia;
            horasSemana += totalHorasDia;

            if (horasSemana > aTrabajarSemana)
            {
                double horasExtraActual = horasSemana - aTrabajarSemana;
                double horasARestar = horasExtraActual - horasExtraAnteriores;
                if (horasDeuda > 0)
                {
                    horasDeuda -= horasARestar;
                    if (horasDeuda < 0)
                    {
                        bancoHoras += horasDeuda * (-1); // Las horas de mas, las sumamos a bancoHoras *-1 porque estará en negativo
                        horasDeuda = 0;
                    }
                }
                else
                {
                    bancoHoras += totalHorasDia;
                }
            }
            ActualizarDocumentoUsuario();
        }

        public void RecalcularReEntrada()
        {
            LeerDocumentoUsuario();
            double horasTotales = jornada.ObtenerJornadaDia();
            horasPracticas += horasTotales;
            horasDeuda += horasTotales - bancoHoras;
            if (horasDeuda < 0)
            {
                horasDeuda = 0;
            }
            bancoHoras -= horasTotales; // *-1 porque arriba estan en negativo, si no, se estarian sumando
            if (bancoHoras < 0)
            {
                bancoHoras = 0;
            }
            horasMes -= horasTotales;
            horasSemana -= horasTotales;
            ActualizarDocumentoUsuario();
            ctrlArchivos.EliminarUltimoRegistro("Salidas.txt");
        }

        public void ReiniciarSemana()
        {
            LeerDocumentoUsuario();
            string ultimaEntrada = ctrlArchivos.LeerUltimoRegistro("Entradas.txt");
            string fechaUltimaEntrada = ultimaEntrada.Split(' ')[0];
            string fechaActual = DateTime.Today.ToString("dd/MM/yyyy");
            if (fechaActual != fechaUltimaEntrada)
            {
                horasSemana = 0;
                ActualizarDocumentoUsuario();
            }
        }

        public void ReiniciarMes()
        {
            LeerDocumentoUsuario();
            string ultimaEntrada = ctrlArchivos.LeerUltimoRegistro("Entradas.txt");
            string fechaUltimaEntrada = ultimaEntrada.Split(' ')[0];
            string fechaActual = DateTime.Today.ToString("dd/MM/yyyy");
            if (fechaActual != fechaUltimaEntrada)
            {
                horasMes = 0;
                ActualizarDocumentoUsuario();
            }
        }

        public void ActualizarDocumentoUsuario()
        {
            string texto = $"A trabajar semanalmente: {aTrabajarSemana}\nHoras este mes: {Math.Round(horasMes, 2)}\nHoras esta semana: {Math.Round(horasSemana, 2)}\n" +
                $"Acumulado Banco horas: {Math.Round(bancoHoras, 2)}\nHoras deuda: {Math.Round(horasDeuda, 2)}\nHoras practicas: {Math.Round(horasPracticas, 2)}";
            File.WriteAllText("Usuario.txt", texto);
        }
    }
}
