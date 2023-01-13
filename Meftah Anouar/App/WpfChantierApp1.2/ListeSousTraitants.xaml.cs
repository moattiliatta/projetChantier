using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    /// Interaction logic for ListeSousTraitants.xaml
    /// </summary>
    public partial class ListeSousTraitants : Window
    {
        ObservableCollection<SousTraitant> sousTraitants = new ObservableCollection<SousTraitant>();
        SqlConnection connection = null;
        public ListeSousTraitants()
        {
            InitializeComponent();
        }

        private void listViewDataBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {

        }

        private void Button_supprimer(object sender, RoutedEventArgs e)
        {
            
            textBoxDomainSousTraitant.Text = "";
            textBoxOuvrageID.Text = "";
            textBoxDate_Debut.Text = "";
            textBoxDate_Fin.Text = "";
           

        }

        private void Button_Trier(object sender, RoutedEventArgs e)
        {

        }

        private void Button_modifier(object sender, RoutedEventArgs e)
        {

            int index = listViewDataBase.SelectedIndex;
            if (index > 0)
            {
                SousTraitant sousTraitant = sousTraitants [index];
                
                if (sousTraitant != null)
                {
                   // if (Crud.Modifier(sousTraitant, connection))
                    {
                        sousTraitant.OuvrageID = textBoxDomainSousTraitant.Text;
                        sousTraitant.DomainSousTraitant = textBoxDomainSousTraitant.Text;
                        sousTraitant.Date_Debut = textBoxDate_Debut.SelectedDate.Value;
                        sousTraitant.Date_Fin = textBoxDate_Fin.SelectedDate.Value;

                        
                    }
                }
            }
        }

        private void Button_Annuler(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void Button_Afficher(object sender, RoutedEventArgs e)
        {
            sousTraitants = Crud.GetListEmployes(connection); // connection avec la base de donnée 

            listViewDataBase.ItemsSource = sousTraitants; // Liaison de données (Binding) avec la liste
        }
    }
}
