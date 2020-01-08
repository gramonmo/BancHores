using BancHores.Clases;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BancHores;

namespace BancHores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        MetodosGenerales metodosGenerales = new MetodosGenerales();

        // Permite que se pueda arrastrar la ventana haciendo click en qualquier lado.
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            metodosGenerales.InsertarFecha(lbFecha);
            metodosGenerales.ocultarElementos(lbEntrada, lbSalida);
        }      

        private void lbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btRegistro_Click(object sender, RoutedEventArgs e)
        {
            RegistroMensual ventanaRegistro = new RegistroMensual();
            ventanaRegistro.ShowDialog();
        }
    }
}
