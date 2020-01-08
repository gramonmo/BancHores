using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using BancHores.Clases;
using BancHores.ClasesBBDD;

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
            metodosGenerales.ocultarElementos(elementos);
        }              

        // Eventos botones
        private void btRegistro_Click(object sender, RoutedEventArgs e)
        {
            RegistroMensual ventanaRegistro = new RegistroMensual();
            ventanaRegistro.ShowDialog();
        }


    }
}
