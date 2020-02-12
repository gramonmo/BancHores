using BancHores.ClasesBBDD;
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

namespace BancHores.Ventanas_Auxiliares
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class VentanaModificarBancoHoras : Window
    {
        public int flag; // flag indica si vas a sumar o restar horas.

        Persona usuario = new Persona();

        string[] horasCB = new string[24];
        string[] minutosCB = new string[60];

        public VentanaModificarBancoHoras(int flag)
        {
            InitializeComponent();
            this.flag = flag;
            GenerarValoresComboBox();       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EstablecerMensaje();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double horas = double.Parse(cbHoras.SelectedItem.ToString());
                double minutos = double.Parse(cbMinutos.SelectedItem.ToString());
                minutos /= 60;
                horas += minutos;

                if (flag == 0)
                {
                    SumarBancoHoras(horas);
                }
                else
                {
                    RestarBancoHoras(horas);
                }
                usuario.ActualizarDocumentoUsuario();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error, no se ha modificado el banco de horas correctamente.");
                this.Close();
            }
        }

        public void EstablecerMensaje()
        {
            if (flag == 0)
            {
                tBlText.Text = "Cuantas horas quieres AÑADIR al banco de horas?";
            }
            else
            {
                tBlText.Text = "Cuantas horas quieres QUITAR al banco de horas?";
            }
        }

        public void SumarBancoHoras(double valor)
        {
            usuario.LeerDocumentoUsuario();
            if (usuario.horasDeuda > 0)
            {
                usuario.horasDeuda -= valor;
                if (usuario.horasDeuda < 0)
                {
                    double diferencia = usuario.horasDeuda * (-1);
                    usuario.horasDeuda = 0;
                    usuario.bancoHoras += diferencia;
                }
            }
            else
            {
                usuario.bancoHoras += valor;
            }
        }

        public void RestarBancoHoras(double valor)
        {
            usuario.LeerDocumentoUsuario();
            if (usuario.bancoHoras > 0)
            {
                usuario.bancoHoras -= valor;
                if (usuario.bancoHoras < 0)
                {
                    double diferencia = usuario.bancoHoras * (-1);
                    usuario.bancoHoras = 0;
                    usuario.horasDeuda += diferencia;
                }
            }
            else
            {
                usuario.horasDeuda += valor;
            }
        }

        private void GenerarValoresComboBox()
        {
            for (int i = 0; i < 60; i++)
            {
                // Crear lista de minutos
                if (i < 10)
                {
                    minutosCB[i] = $"0{i}";
                }
                else
                {
                    minutosCB[i] = $"{i}";
                }

                // Crear lista de horas
                if (i < 24)
                {
                    if (i < 10)
                    {
                        horasCB[i] = $"0{i}";
                    }
                    else
                    {
                        horasCB[i] = $"{i}";
                    }
                }
            }
            cbHoras.ItemsSource = horasCB;
            cbHoras.SelectedIndex = 0;
            cbMinutos.ItemsSource = minutosCB;
            cbMinutos.SelectedIndex = 0;
        }
    }
}
