using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BancHores.Clases;
using BancHores.ClasesBBDD;
using BancHores.Ventanas_Auxiliares;
using System;
using System.Windows.Media.Animation;

namespace BancHores
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        MetodosGenerales metodosGenerales = new MetodosGenerales();
        ControlArchivos ctrlArchivos = new ControlArchivos();
        Calculos_Comp calculos_comp = new Calculos_Comp();
        Jornada jornada = new Jornada();
        Persona usuario = new Persona();

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
            if (ctrlArchivos.ComprovarArchivos())
            {
                VentanaConfigInicial ventana = new VentanaConfigInicial();
                ventana.ShowDialog();
            }
            EstablecerUI();

            if (calculos_comp.EsLunes())
            {
                usuario.ReiniciarSemana();
            }
            if (calculos_comp.EsPrimeroDeMes())
            {
                usuario.ReiniciarMes();
            }
            while (calculos_comp.DiaAnteriorSinSalida())
            {
                MarcarSalidaUltimoDia();
            }

            ActualizarTablaResumen();
        }

        #region Eventos Botones
        // Eventos botones
        
        private void imgMenu_MouseMove(object sender, MouseEventArgs e)
        {
            metodosGenerales.cambiarImagen(imgMenu, "Recursos/menuBlanco.png");
        }

        private void imgMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            metodosGenerales.cambiarImagen(imgMenu, "Recursos/menuNegro.png");
        }

        private void imgMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animWidth = new DoubleAnimation(0, 220, TimeSpan.FromSeconds(0.35));
            spMenu.BeginAnimation(Window.WidthProperty, animWidth);
            spMenu.Visibility = Visibility.Visible;
            
        }

        private void spMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            spMenu.Visibility = Visibility.Hidden;
        }

        private void btRegistro_Click(object sender, RoutedEventArgs e)
        {
            RegistroMensual ventanaRegistro = new RegistroMensual();
            ventanaRegistro.ShowDialog();
        }

        private void btEntrada_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            if (calculos_comp.YaHaySalidaEseDia())
            {
                // msgbox avis si vol reaunudar -> si -> esborrar ultima entrada, carregar valors entrada d'aquell dia
                if (MessageBox.Show($"Ya has terminado tu jornada de hoy. Quieres anular tu anterior salida?", "Anular anterior salida", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    usuario.RecalcularReEntrada();
                    EstablecerUI();
                }
                return;
            }
            jornada.RegistrarMarcaje(jornada, fechaYHora, lbEntrada, 0);
            btEntrada.IsEnabled = false;
            btSalida.IsEnabled = true;
            btPausa.IsEnabled = true;
            btPausaCustom.IsEnabled = true;
            btContinuar.IsEnabled = false;
            metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Pintamos Ellipse verde: #FF49DA49
            lbActividad.Content = $"Jornada en curso";
        }

        private void btSalida_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            if (MessageBox.Show($"Estás seguro que quieres marcar tu salida?{Environment.NewLine}Són las {fechaYHora.ToString("HH:mm")}", "Confirmar salida", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Si hay una pausa en curso, pregunta a que hora se ha vuelto a poner a trabajar.
                if (calculos_comp.HayPausaEnCurso())
                {
                    VentanaTrabajoReaunudado vent = new VentanaTrabajoReaunudado();
                    vent.ShowDialog();

                    // Si cierra la ventana emergente o cancela (la pausa sigue) deja de ejecutar la salida
                    if (calculos_comp.HayPausaEnCurso())
                    {
                        MessageBox.Show("No se ha completado la pausa. No sé ha registrado la salida", "ATENCION!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
                jornada.RegistrarMarcaje(jornada, fechaYHora, lbSalida, 1);
                btEntrada.IsEnabled = true;
                btSalida.IsEnabled = false;
                btPausa.IsEnabled = false;
                btPausaCustom.IsEnabled = false;
                btContinuar.IsEnabled = false;
                metodosGenerales.cambiarColorEllipse(elActividad, "#FFCF2A2A"); // Pintamos Ellipse roja: #FFCF2A2A             

                double horasTotales = jornada.ObtenerJornadaDia();
                string horasTotalesStr = calculos_comp.SepararHorasYMinutos(horasTotales);
                lbActividad.Content = $"Jornada finalizada. Has trabajado {horasTotalesStr}";

                usuario.CalculoBalanceHoras(horasTotales);

                ActualizarTablaResumen();
            }
        }

        private void btPausa_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            if (MessageBox.Show("Estás seguro que quieres pausar tu jornada?", "Confirmar pausa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                jornada.RegistrarMarcaje(jornada, fechaYHora, lbPausa, 2);
                btPausa.IsEnabled = false;
                btPausaCustom.IsEnabled = false;
                btContinuar.IsEnabled = true;
                metodosGenerales.cambiarColorEllipse(elActividad, "#FF2D2DE8"); // Pintamos Ellipse azul: #FF2D2DE8
                lbActividad.Content = "Jornada pausada";
            }
        }

        private void btContinuar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            jornada.RegistrarMarcaje(jornada, fechaYHora, lbContinuar, 3);
            btPausa.IsEnabled = true;
            btPausaCustom.IsEnabled = true;
            btContinuar.IsEnabled = false;
            metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Pintamos Ellipse verde: #FF49DA49
            lbActividad.Content = "Jornada en curso";
        }

        private void btPausaCustom_Click(object sender, RoutedEventArgs e)
        {
            VentanaPausaCustom ventana = new VentanaPausaCustom();
            ventana.ShowDialog();
        }

        private void btSumarDeuda_Click(object sender, RoutedEventArgs e)
        {
            // Flag 0 añade deuda
            VentanaModificarDeuda ventana = new VentanaModificarDeuda(0);
            ventana.ShowDialog();
            ActualizarTablaResumen();
        }

        private void btRestarDeuda_Click(object sender, RoutedEventArgs e)
        {
            // Flag 1 resta deuda
            VentanaModificarDeuda ventana = new VentanaModificarDeuda(1);
            ventana.ShowDialog();
            ActualizarTablaResumen();
        }
        #endregion

        #region Metodos
        // Metodos
        public void EstablecerUI()
        {
            ActualizarTablaResumen();
            metodosGenerales.InsertarFecha(lbFecha);
            Label[] labels = { lbEntrada, lbSalida, lbPausa, lbContinuar };
            metodosGenerales.OcultarLabels(labels);

            string ultimaEntrada;
            if (calculos_comp.YaHayEntradaEseDia(out ultimaEntrada))
            {
                btEntrada.IsEnabled = false;
                btSalida.IsEnabled = true;
                string hora = calculos_comp.ObtenerHoraDeString(ultimaEntrada, 0);
                lbEntrada.Content = $"Entrada: {hora}";
                lbEntrada.Visibility = Visibility.Visible;

                if (calculos_comp.HayPausaEnCurso()) // Si hay una pausa en curso
                {
                    btPausa.IsEnabled = false;
                    btPausaCustom.IsEnabled = false;
                    btContinuar.IsEnabled = true;
                    metodosGenerales.cambiarColorEllipse(elActividad, "#FF2D2DE8"); // Pintamos ellipse azul
                    lbActividad.Content = "Jornada pausada";
                }
                else // Si NO hay pausa en curso
                {
                    btPausa.IsEnabled = true;
                    btPausaCustom.IsEnabled = true;
                    btContinuar.IsEnabled = false;
                    metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Pintamos ellipse verde
                    lbActividad.Content = $"Jornada en curso";
                }
            }
            else // Si no hay entrada registrada de ese dia
            {
                btEntrada.IsEnabled = true;
                btSalida.IsEnabled = false;
                btPausa.IsEnabled = false;
                btPausaCustom.IsEnabled = false;
                btContinuar.IsEnabled = false;
                metodosGenerales.cambiarColorEllipse(elActividad, "#FFCF2A2A"); // Pintamos ellipse roja
                lbActividad.Content = "Fuera de jornada";
            }
        }

        // Rellena la tabla resumen con la info del usuario
        public void ActualizarTablaResumen()
        {
            usuario.LeerDocumentoUsuario();
            lbHorasMes.Content = calculos_comp.SepararHorasYMinutos(usuario.horasMes);
            lbHorasSemana.Content = calculos_comp.SepararHorasYMinutos(usuario.horasSemana);
            lbHorasAcumuladas.Content = calculos_comp.SepararHorasYMinutos(usuario.bancoHoras);
            lbHorasDeuda.Content = calculos_comp.SepararHorasYMinutos(usuario.horasDeuda);
            lbHorasPracticas.Content = $"{usuario.horasPracticas}h";
        }

        public void MarcarSalidaUltimoDia()
        {
            VentanaMarcarSalida ventana = new VentanaMarcarSalida();
            ventana.ShowDialog();
            while (calculos_comp.HayPausaEnCurso())
            {
                VentanaTrabajoReaunudado vent = new VentanaTrabajoReaunudado();
                vent.ShowDialog();
            }

            double horasTotales = jornada.ObtenerJornadaDia();
            string horasTotalesStr = calculos_comp.SepararHorasYMinutos(horasTotales);
            lbActividad.Content = $"Jornada finalizada. Trabajaste {horasTotalesStr}";

            usuario.CalculoBalanceHoras(horasTotales);

            ActualizarTablaResumen();
        }
        #endregion     
    }
}
