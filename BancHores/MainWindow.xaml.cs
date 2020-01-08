using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using BancHores.Clases;
using BancHores.ClasesBBDD;
using System;

namespace BancHores
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }     

        MetodosGenerales metodosGenerales = new MetodosGenerales();
        Persona gerard = new Persona("Gerard", 0, 0, 0, 0);
        Jornada jornada = new Jornada();

        // Permite que se pueda arrastrar la ventana haciendo click en qualquier lado.
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void lbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        // Window_Loaded, Para que se ejecute al abrir el programa
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            metodosGenerales.InsertarFecha(lbFecha);

            Label[] elementos = { lbEntrada, lbSalida, lbPausa, lbContinuar };
            metodosGenerales.OcultarElementos(elementos);
        }

        // Eventos botones
        private void btRegistro_Click(object sender, RoutedEventArgs e)
        {
            RegistroMensual ventanaRegistro = new RegistroMensual();
            ventanaRegistro.ShowDialog();
        }

        private void btEntrada_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            jornada.RegistrarMarcaje(jornada, fechaYHora, lbEntrada, 0);
            btEntrada.IsEnabled = false;
            btSalida.IsEnabled = true;
            btPausa.IsEnabled = true;
            btContinuar.IsEnabled = false;
            metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Color verde es: #FF49DA49
            lbActividad.Content = "Jornada en curso";
        }

        private void btSalida_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            if (MessageBox.Show($"Estás seguro que quieres marcar tu salida?/n Són las {fechaYHora.ToString("HH:mm")}", "Confirmar salida", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                jornada.RegistrarMarcaje(jornada, fechaYHora, lbSalida, 1);
                btEntrada.IsEnabled = true;
                btSalida.IsEnabled = false;
                btPausa.IsEnabled = false;
                btContinuar.IsEnabled = false;             
                metodosGenerales.cambiarColorEllipse(elActividad, "#FFCF2A2A"); // Color rojo es: #FFCF2A2A
                lbActividad.Content = "Jornada Finalizada";
            }
        }

        private void btPausa_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            if (MessageBox.Show("Estás seguro que quieres pausar tu jornada?", "Confirmar pausa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                jornada.RegistrarMarcaje(jornada, fechaYHora, lbPausa, 2);
                btPausa.IsEnabled = false;
                btContinuar.IsEnabled = true;
                metodosGenerales.cambiarColorEllipse(elActividad, "#FF2D2DE8"); // Color azul es: #FF2D2DE8
                lbActividad.Content = "Jornada Pausada";
            }
        }

        private void btContinuar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            jornada.RegistrarMarcaje(jornada, fechaYHora, lbContinuar, 3);
            btPausa.IsEnabled = true;
            btContinuar.IsEnabled = false;
            metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Color verde es: #FF49DA49
            lbActividad.Content = "Jornada en curso";

        }
    }
}
