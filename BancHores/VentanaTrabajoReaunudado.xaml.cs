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

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
