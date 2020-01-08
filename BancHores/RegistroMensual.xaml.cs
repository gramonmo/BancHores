using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace BancHores
{
    /// <summary>
    /// Lógica de interacción para RegistroMensual.xaml
    /// </summary>
    public partial class RegistroMensual : Window
    {
        public RegistroMensual()
        {
            InitializeComponent();
        }
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
    }
}
