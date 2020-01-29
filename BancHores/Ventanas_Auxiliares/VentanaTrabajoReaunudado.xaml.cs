using BancHores.Clases;
using System;
using System.Windows;

namespace BancHores
{
    /// <summary>
    /// Lógica de interacción para VentanaTrabajoReaunudado.xaml
    /// </summary>
    public partial class VentanaTrabajoReaunudado : Window
    {
        public VentanaTrabajoReaunudado()
        {                
            InitializeComponent();
            GenerarValoresComboBox();
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
            string hora = cbHora.SelectedItem.ToString();
            string minuto = cbMinuto.SelectedItem.ToString();
            string fechaYHoraStr = $"{fecha} {hora}:{minuto}";

            ctrlArchivos.EscribirPausaContinuar("Pausas.txt", fechaYHoraStr, 1);
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
            cbHora.ItemsSource = horas;
            cbHora.SelectedIndex = 0;
            cbMinuto.ItemsSource = minutos;
            cbMinuto.SelectedIndex = 0;
        }        
    }
}
