﻿using System.Windows;
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
        ControlArchivos ctrlArchivos = new ControlArchivos();
        Calculos_Comp calc_comp = new Calculos_Comp();
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
            ctrlArchivos.ComprovarArchivos();
            EstablecerUI();

            Jornada test = new Jornada();
            VentanaTrabajoReaunudado vent = new VentanaTrabajoReaunudado();
            vent.ShowDialog();
            
        }

        #region Eventos Botones
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
            metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Pintamos Ellipse verde: #FF49DA49
            lbActividad.Content = "Jornada en curso";
        }

        private void btSalida_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            if (MessageBox.Show($"Estás seguro que quieres marcar tu salida?/n Són las {fechaYHora.ToString("HH:mm")}", "Confirmar salida", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //comprovar si hi ha pausa en curs
                if (calc_comp.HayPaysaEnCurso())
                {
                    //mostrar finestra, obtenir dades i escriure al document de pausa
                }
                jornada.RegistrarMarcaje(jornada, fechaYHora, lbSalida, 1);
                btEntrada.IsEnabled = true;
                btSalida.IsEnabled = false;
                btPausa.IsEnabled = false;
                btContinuar.IsEnabled = false;             
                metodosGenerales.cambiarColorEllipse(elActividad, "#FFCF2A2A"); // Pintamos Ellipse roja: #FFCF2A2A
                lbActividad.Content = "Jornada finalizada";
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
                metodosGenerales.cambiarColorEllipse(elActividad, "#FF2D2DE8"); // Pintamos Ellipse azul: #FF2D2DE8
                lbActividad.Content = "Jornada pausada";
            }
        }

        private void btContinuar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaYHora = DateTime.Now;
            jornada.RegistrarMarcaje(jornada, fechaYHora, lbContinuar, 3);
            btPausa.IsEnabled = true;
            btContinuar.IsEnabled = false;
            metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Pintamos Ellipse verde: #FF49DA49
            lbActividad.Content = "Jornada en curso";

        }
        #endregion

        #region Metodos
        // Metodos
        public void EstablecerUI()
        {
            metodosGenerales.InsertarFecha(lbFecha);
            Label[] labels = { lbEntrada, lbSalida, lbPausa, lbContinuar };
            metodosGenerales.OcultarLabels(labels);
            string ultimaEntrada;
            if (calc_comp.YaHayEntradaEseDia(out ultimaEntrada))
            {
                btEntrada.IsEnabled = false;
                btSalida.IsEnabled = true;
                string hora = calc_comp.ObtenerHoraDeString(ultimaEntrada, 0);
                lbEntrada.Content = $"Entrada: {hora}";
                if (calc_comp.HayPaysaEnCurso()) // Si hay una pausa en curso
                {
                    btPausa.IsEnabled = false;
                    btContinuar.IsEnabled = true;
                    metodosGenerales.cambiarColorEllipse(elActividad, "#FF2D2DE8"); // Pintamos ellipse azul
                    lbActividad.Content = "Jornada pausada";
                }
                else // Si NO hay pausa en curso
                {
                    btPausa.IsEnabled = true;
                    btContinuar.IsEnabled = false;
                    metodosGenerales.cambiarColorEllipse(elActividad, "#FF49DA49"); // Pintamos ellipse verde
                    lbActividad.Content = "Jornada en curso";
                }
            }
            else
            {
                btEntrada.IsEnabled = true;
                btSalida.IsEnabled = false;
                btPausa.IsEnabled = true;
                btContinuar.IsEnabled = true;
                metodosGenerales.cambiarColorEllipse(elActividad, "#FFCF2A2A"); // Pintamos ellipse roja
                lbActividad.Content = "Fuera de jornada";
            }

        }
        #endregion
    }
}
