using BancHores.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BancHores
{
    /// <summary>
    /// Lógica de interacción para VentanaPausaCustom.xaml
    /// </summary>
    public partial class VentanaPausaCustom : Window
    {
        public VentanaPausaCustom()
        {
            InitializeComponent();
        }

        string[] horas = new string[24];
        string[] minutos = new string[60];

        ControlArchivos ctrlArchivos = new ControlArchivos();

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            string fecha = DateTime.Now.Date.ToShortDateString();
            string horaIni = cbHoraIni.SelectedItem.ToString();
            string minutoIni = cbMinutoIni.SelectedItem.ToString();
            string horaFin = cbHoraFin.SelectedItem.ToString();
            string minutoFin = cbMinutoFin.SelectedItem.ToString();

            string pausaCustom = $"{fecha} {horaIni}:{minutoIni} - {fecha} {horaFin}:{minutoFin}";
            ctrlArchivos.EscribirPausaCompleta("Pausas.txt", pausaCustom);
            this.Close();
        }

        // Rellena los string con las horas y minutos a mostrar en los combobox y los añade a estos.
        private void GenerarValoresComboBox()
        {
            for (int i = 0; i < 60; i++)
            {
                // Crear lista de minutos
                if (i < 10)
                {
                    minutos[i] = $"0{i}";
                }
                else
                {
                    minutos[i] = $"{i}";
                }

                // Crear lista de horas
                if (i < 24)
                {
                    if (i < 10)
                    {
                        horas[i] = $"0{i}";
                    }
                    else
                    {
                        horas[i] = $"{i}";
                    }
                }
            }
            cbHoraIni.ItemsSource = horas;
            cbHoraIni.SelectedIndex = 0;
            cbMinutoIni.ItemsSource = minutos;
            cbMinutoIni.SelectedIndex = 0;

            cbHoraFin.ItemsSource = horas;
            cbHoraFin.SelectedIndex = 0;
            cbMinutoFin.ItemsSource = minutos;
            cbMinutoFin.SelectedIndex = 0;
        }   
    }
}
