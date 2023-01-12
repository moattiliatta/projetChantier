﻿using System;
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

namespace ProjetChantierApp1._1
{
    /// <summary>
    /// Interaction logic for SanteSecuriteInterface.xaml
    /// </summary>
    public partial class SanteSecuriteInterface : Window
    {
        public SanteSecuriteInterface()
        {
            InitializeComponent();
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {

            chkBoxBottes.IsChecked = false;
            chkBoxCasque.IsChecked = false;
            chkBoxLunettes.IsChecked = false;
        }
    }
}