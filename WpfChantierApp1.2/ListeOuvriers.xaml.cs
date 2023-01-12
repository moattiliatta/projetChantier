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
using WpfChantierApp1._2;

namespace WpfChantierApp1._2
{
    /// <summary>
    /// Interaction logic for ListeOuvriers.xaml
    /// </summary>
    public partial class ListeOuvriers : Window
    {
        public ListeOuvriers()
        {
            InitializeComponent();
            AfficherEmployes();
        }

        private void ListViewOuvriers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // Employe employeSelected = (Employe)ListViewOuvriers.SelectedItem;

            //if (ListViewOuvriers.SelectedItem is Employe employe)
            //{
            //    txtBoxDateEmbauche.Text = employe.DateEmbauche.ToString();
            //    txtBoxEmployeID.Text = employe.EmployeID.ToString();
            //    txtBoxEmployeNom.Text = employe.Nom;
            //    txtBoxEmployePreNom.Text = employe.Prenom;
            //    txtBoxTelephone.Text = employe.Telephone;
            //    txtBoxEquipeID.Text = employe.EquipeID.ToString();
            //    txtBoxMotPasse.Text = employe.EmployeMotPasse.ToString();

            //}

            if (ListViewOuvriers.SelectedItem is Employe employe)
            {
                txtBoxDateEmbauche.Text = employe.DateEmbauche.ToString();
                txtBoxEmployeID.Text = employe.EmployeID.ToString();
                txtBoxEmployeNom.Text = employe.Nom;
                txtBoxEmployePreNom.Text = employe.Prenom;
                txtBoxTelephone.Text = employe.Telephone;
                txtBoxEquipeID.Text = employe.EquipeID.ToString();
                txtBoxMotPasse.Text = employe.EmployeMotPasse.ToString();
            }
        }

        public void AfficherEmployes()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewOuvriers.ItemsSource = dbEntities.Employes.ToList();
            }

        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn ajouter");
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn supprimer");
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn modifier");
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn effacer");
        }
    }
}


//private void ListViewMateriaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
//{
//    Materiaux materiauxSelected = (Materiaux)ListViewMateriaux.SelectedItem;

//    if (ListViewMateriaux.SelectedItem is Materiaux materiaux)
//    {
//        txtBoxMateriauxID.Text = materiaux.MateriauxID.ToString();
//        txtBoxNomMateriaux.Text = materiaux.NomMateriaux;
//        txtBoxDateRecept.Text = materiaux.DateReception.ToString();
//        txtBoxOuvrageID.Text = materiaux.OuvrageID.ToString();
//    }
//}

//public void AfficherMateriaux()
//{
//    using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
//    {
//        ListViewMateriaux.ItemsSource = dbEntities.Materiauxes.ToList();
//    }
//}