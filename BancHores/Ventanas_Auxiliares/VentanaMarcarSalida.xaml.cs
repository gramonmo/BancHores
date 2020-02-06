using BancHores.Clases;
using System;
using System.Windows;

namespace BancHores.Ventanas_Auxiliares
{
    /// <summary>
    /// Lógica de interacción para VentanaMarcarSalida.xaml
    /// </summary>
    public partial class VentanaMarcarSalida : Window
    {
        public VentanaMarcarSalida()
        {
            InitializeComponent();
            GenerarValoresComboBox();
        }

        string[] horas = new string[24];
        string[] minutos = new string[60];
        bool salidaMarcada = false;

        ControlArchivos ctrlArchivos = new ControlArchivos();
        Calculos_Comp calc_comp = new Calculos_Comp();

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (cbHora.Text == "00" && cbMinuto.Text == "00")
            {
                if(MessageBox.Show("Estas seguro que tu jornada terminó a las 00:00?", "Es correcta la hora?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    salidaMarcada = true;
                    string fechaEntradaUltimoDia = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}\Entradas.txt");
                    fechaEntradaUltimoDia = calc_comp.ObtenerFechaDeString(fechaEntradaUltimoDia);
                    string hora = "23";
                    string minuto = "59";
                    string fechaYHoraStr = $"{fechaEntradaUltimoDia} {hora}:{minuto}";
                    ctrlArchivos.EscribirEntradaSalida($@"{PathGlobal.pathData}\Salidas.txt", fechaYHoraStr);
                    this.Close();
                }
            }
            else
            {
                salidaMarcada = true;
                string fechaEntradaUltimoDia = ctrlArchivos.LeerUltimoRegistro($@"{PathGlobal.pathData}\Entradas.txt");
                fechaEntradaUltimoDia = calc_comp.ObtenerFechaDeString(fechaEntradaUltimoDia);
                string hora = cbHora.SelectedItem.ToString();
                string minuto = cbMinuto.SelectedItem.ToString();
                string fechaYHoraStr = $"{fechaEntradaUltimoDia} {hora}:{minuto}";
                ctrlArchivos.EscribirEntradaSalida($@"{PathGlobal.pathData}\Salidas.txt", fechaYHoraStr);
                this.Close();
            }           
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!salidaMarcada)
            {
                MessageBox.Show("No has introducido la hora a la que terminaste la jornada.");
                e.Cancel = true;
            }           
        }
    }
}
