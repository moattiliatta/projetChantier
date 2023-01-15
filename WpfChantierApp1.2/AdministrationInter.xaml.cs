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
using WpfChantierApp1._2;

namespace WpfChantierApp1._2
{
    /// <summary>
    /// Interaction logic for AdministrationInter.xaml
    /// </summary>
    public partial class AdministrationInter : Window
    {
        Employe employeSession;

        public AdministrationInter()
        {
            InitializeComponent();
            AfficherEmployeSession();
           // MessageBox.Show(this.employeSession.Prenom + this.employeSession.EmployeID);
        }

        public AdministrationInter(Employe employeSession)
        {
            this.employeSession = employeSession;
        }

        //private void btnListMacons_Click(object sender, RoutedEventArgs e)
        //{
        //    //AdministrationInter admin = new AdministrationInter();
        //    //admin.ShowDialog();
        //    MessageBox.Show("Liste macons sélectionnée");
        //}

        private void btnListOuvrage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Ouvrages sélectionnée");
            ListeOuvrage ouvrage = new ListeOuvrage();
            ouvrage.ShowDialog();

        }

        private void btnListSoutraitent_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Soutraitent sélectionnée");
            ListeSousTraitants soustraitent = new ListeSousTraitants();
            soustraitent.ShowDialog();

        }

        private void btnLivraison_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Livraison sélectionnée");
            ListeLivraisons livraisons = new ListeLivraisons();
            livraisons.ShowDialog();

        }

        private void btnRegleSecurite_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Regle Securite sélectionnée, est en cours de développement ");
        }

        private void btnListOuvriers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Ouvriers sélectionnée");
            ListeOuvriers ouvriers = new ListeOuvriers();
            ouvriers.ShowDialog();

        }

        private void AfficherEmployeSession()
        {
            string message = "Bienvenue : ";
            txtBlockPrenom.Text = message + employeSession.Prenom;
            MessageBox.Show(employeSession.Prenom + employeSession.EmployeID);

        }
    }
}


//using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
//{
//    txtBlockPrenom.Text = employeSession.Prenom;

//}