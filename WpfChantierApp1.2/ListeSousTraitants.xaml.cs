using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public ListeSousTraitants()
        {
            InitializeComponent();
            AfficherSousTraitant();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Ajouter");
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Supprimer");
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Modifier");

            Sous_Traitant sousTraitantSelected = (Sous_Traitant)ListViewSousTraitants.SelectedItem;

            if(sousTraitantSelected != null)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {

                    Sous_Traitant sousTraitModifier = dbEntities.Sous_Traitant.FirstOrDefault(str => str.SousTraitantID == sousTraitantSelected.SousTraitantID);

                    if(sousTraitModifier != null)
                    {
                        sousTraitModifier.DomainSousTraitant = txtBoxDomainSousTraitant.Text;
                        // MODIFIER : includ Ouvrage ID into a combo box to avoid furute bugs
                        sousTraitModifier.OuvrageID = int.Parse(txtBoxOuvrageID.Text);
                        sousTraitModifier.Date_Debut_SousTraitant = txtBoxDebutSousTraitant.Text;
                        sousTraitModifier.Date_Fin_SousTraitant = txtBoxFinSousTraitant.Text;

                        int resultat = dbEntities.SaveChanges();
                        if (resultat > 0)
                        {
                            this.AfficherSousTraitant();
                            string message = $"Le soustratan avec ID # : {sousTraitModifier.SousTraitantID} \n dans le domain : {sousTraitModifier.DomainSousTraitant} a ete modifie";
                            MessageBox.Show(message);
                        }

                    }

                }

            }


        }
 
        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxSousTraitantID.Text = "";
            txtBoxDomainSousTraitant.Text = "";
            txtBoxOuvrageID.Text = "";
            txtBoxDebutSousTraitant.Text = "";
            txtBoxFinSousTraitant.Text = "";
        }

        public void AfficherSousTraitant() {

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewSousTraitants.ItemsSource = dbEntities.Sous_Traitant.ToList();
            }
        }
    }
}
