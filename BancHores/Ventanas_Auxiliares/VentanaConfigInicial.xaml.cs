using BancHores.ClasesBBDD;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;

namespace BancHores.Ventanas_Auxiliares
{
    /// <summary>
    /// Lógica de interacción para VentanaConfigInicial.xaml
    /// </summary>
    public partial class VentanaConfigInicial : Window
    {
        Persona usuario = new Persona();
        public VentanaConfigInicial()
        {
            InitializeComponent();
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double horasPracticas = double.Parse(tbHorasPracticas.Text);
                double horasSemanales = double.Parse(tbHorasSemanales.Text);
                usuario.horasPracticas = horasPracticas;
                usuario.aTrabajarSemana = horasSemanales;
                usuario.UpdateConfigIni();
                this.Close();
            }
            catch
            {
                double horasPracticas = 0;
                double horasSemanales = 0;
                usuario.horasPracticas = horasPracticas;
                usuario.aTrabajarSemana = horasSemanales;
                usuario.UpdateConfigIni();
                this.Close();
            }
        }    

        private void tbHorasSemanales_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                try
                {
                    double horasPracticas = double.Parse(tbHorasPracticas.Text);
                    double horasSemanales = double.Parse(tbHorasSemanales.Text);
                    usuario.horasPracticas = horasPracticas;
                    usuario.aTrabajarSemana = horasSemanales;
                    usuario.UpdateConfigIni();
                    this.Close();
                }
                catch
                {
                    double horasPracticas = 0;
                    double horasSemanales = 0;
                    usuario.horasPracticas = horasPracticas;
                    usuario.aTrabajarSemana = horasSemanales;
                    usuario.UpdateConfigIni();
                    this.Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbHorasPracticas.Focus();
        }

        
    }
}

