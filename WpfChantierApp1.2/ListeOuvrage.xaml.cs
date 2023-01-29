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

namespace WpfChantierApp1._2
{
    /// <summary>
    /// Interaction logic for ListeOuvrage.xaml
    /// </summary>
    public partial class ListeOuvrage : Window
    {
        public ListeOuvrage()
        {
            InitializeComponent();
            AfficherOuvrage();
        }
        private void ListViewOuvrage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            if(ListViewOuvrage.SelectedItem is Ouvrage ouvrage)
            {
                txtBoxOuvrageID.Text = ouvrage.OuvrageID.ToString();
                txtBoxNomOuvrage.Text = ouvrage.NomOuvrage;
                txtBoxDescOuvrage.Text = ouvrage.Description_Ouvrage;
                //txtBoxEquipeId.Text = ouvrage.EquipeID.ToString();
                //txtBoxDebutOuvrage.Text = ouvrage.Date_Debut_Ouvrage.ToString();
                //txtBoxFinOuvrage.Text = ouvrage.Date_Fin_Ouvrage.ToString() ;
            }
        }

        public void AfficherOuvrage()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewOuvrage.ItemsSource = dbEntities.Ouvrages.ToList();
                // remplir notre combobox avec les ID des appareils existants dans la base de données 
                comboBoxEquipeID.ItemsSource = dbEntities.Equipes.ToList();

            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Ajouter en cours de developpement");
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Supprimer en cours de developpement");
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Modifier en cours de developpement");
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxOuvrageID.Text = "";
            txtBoxNomOuvrage.Text = "";
            txtBoxDescOuvrage.Text = "";
            //txtBoxEquipeId.Text = "";
            //txtBoxDebutOuvrage.Text = "";
            //txtBoxFinOuvrage.Text = "";

        }

      
    }
}
