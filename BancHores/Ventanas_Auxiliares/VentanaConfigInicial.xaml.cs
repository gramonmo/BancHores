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
            usuario.LeerDocumentoUsuario();
            double horasPracticas = double.Parse(tbHorasPracticas.Text);
            double horasSemanales = double.Parse(tbHorasSemanales.Text);
            usuario.horasPracticas = horasPracticas;
            usuario.horasSemana = horasSemanales;
            usuario.ActualizarDocumentoUsuario();
            this.Close();
        }

        public void EstablecerPlantillaUsuario()
        {
            string texto = "A trabajar semanalmente: 0\nHoras este mes: 0\nHoras esta semana: 0\n" +
                "Acumulado Banco horas: 0\nHoras deuda: 0\nHoras practicas: 0";
            File.WriteAllText("Usuario.txt", texto);
        }

        private void tbHorasSemanales_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                usuario.LeerDocumentoUsuario();
                double horasPracticas = double.Parse(tbHorasPracticas.Text);
                double horasSemanales = double.Parse(tbHorasSemanales.Text);
                usuario.horasPracticas = horasPracticas;
                usuario.horasSemana = horasSemanales;
                usuario.ActualizarDocumentoUsuario();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbHorasPracticas.Focus();
        }
    }
}

