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
    public partial class VentanaModificarDeuda : Window
    {
        public int flag; // flag indica si vas a sumar o restar horas.

        Persona usuario = new Persona();

        public VentanaModificarDeuda(int flag)
        {
            this.flag = flag;
            InitializeComponent();
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
                double valor = double.Parse(tbEntrada.Text);
                if (flag == 0)
                {
                    SumarDeuda(valor);
                }
                else
                {
                    RestarDeuda(valor);
                }
                usuario.ActualizarDocumentoUsuario();
                this.Close();
            }
            catch
            {
                this.Close();
            }
        }

        public void EstablecerMensaje()
        {
            if (flag == 0)
            {
                tBlTexto.Text = "Quantas horas de deuda quieres AÑADIR?";
            }
            else
            {
                tBlTexto.Text = "Quantas horas de deuda quieres QUITAR?";
            }
        }

        public void SumarDeuda(double valor)
        {
            usuario.LeerDocumentoUsuario();
            usuario.horasDeuda += valor;
        }

        public void RestarDeuda(double valor)
        {
            usuario.LeerDocumentoUsuario();
            usuario.horasDeuda -= valor;
            if (usuario.horasDeuda < 0)
            {
                usuario.horasDeuda = 0;
            }
        }
    }
}
