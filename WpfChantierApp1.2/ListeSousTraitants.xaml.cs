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
using System.Windows.Media.Media3D;

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

        public void AfficherSousTraitant()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewSousTraitants.ItemsSource = dbEntities.Sous_Traitant.ToList();
                comboBoxOuvrageID.ItemsSource = dbEntities.Ouvrages.ToList();
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {

            bool verifierOK = verifierChamps();

            if (verifierOK)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    Sous_Traitant derniereSousTraitant = dbEntities.Sous_Traitant.ToArray().LastOrDefault();
                    
                    int lastnumber = derniereSousTraitant.SousTraitantID + 1;

                    

                    //  contrôle d'exception, vérifiez que tous les champs d'information de l'interface sont correctement remplis.
                    try
                    {
                        Sous_Traitant newSoustraitant = new Sous_Traitant()
                        {
                            SousTraitantID = lastnumber,
                            DomainSousTraitant = txtBoxDomainSousTraitant.Text,
                            Date_Debut_SousTraitant = datePkrDebutSousTraitant.SelectedDate.Value.ToShortDateString(),
                            Date_Fin_SousTraitant = datePkrFinSousTraitant.SelectedDate.Value.ToShortDateString(),
                            OuvrageID = int.Parse(comboBoxOuvrageID.SelectedValue.ToString()),
                        };

                        if (newSoustraitant != null)
                        {
                            dbEntities.Sous_Traitant.Add(newSoustraitant);
                            int resultat = dbEntities.SaveChanges();
                            if (resultat > 0)
                            {
                                this.AfficherSousTraitant();
                                string message = $"Le Sous Traitant {newSoustraitant.SousTraitantID} a été enregistré dans le système";
                                MessageBox.Show(message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString() + "\n\nATTENTION: \nVérifiez que tous les champs sont correctement remplis.  ");
                    }
                }
            }
            else
            {
                MessageBox.Show("ATTENTION: \n Vérifiez que tous les champs sont correctement remplis");
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Supprimer");

            Sous_Traitant soustraitantSelected = (Sous_Traitant)ListViewSousTraitants.SelectedItem;

            if (soustraitantSelected != null)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    Sous_Traitant soustraitantDeleted = dbEntities.Sous_Traitant.SingleOrDefault(soustr => soustr.SousTraitantID == soustraitantSelected.SousTraitantID);

                    if (soustraitantDeleted != null)
                    {
                        dbEntities.Sous_Traitant.Remove(soustraitantDeleted);
                        int resultat = dbEntities.SaveChanges();
                        if (resultat > 0)
                        {
                            this.AfficherSousTraitant();
                            string message = $"L'soustraitant {soustraitantDeleted.SousTraitantID} à été supprimé ";
                            MessageBox.Show(message);
                        }
                    }
                }
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn Modifier");

            Sous_Traitant sousTraitantSelected = (Sous_Traitant)ListViewSousTraitants.SelectedItem;

            if (sousTraitantSelected != null)
            {

                try
                {
                    if (string.IsNullOrEmpty(txtBoxDomainSousTraitant.Text) || comboBoxOuvrageID.SelectedValue == null || string.IsNullOrEmpty(datePkrDebutSousTraitant.Text) || string.IsNullOrEmpty(datePkrFinSousTraitant.Text))
                    {
                        throw new Exception("Veuillez remplir tous les champs requis.");
                    }

                    using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                    {

                        Sous_Traitant sousTraitModifier = dbEntities.Sous_Traitant.FirstOrDefault(str => str.SousTraitantID == sousTraitantSelected.SousTraitantID);

                        if (sousTraitModifier != null)
                        {
                            sousTraitModifier.DomainSousTraitant = txtBoxDomainSousTraitant.Text;
                            // MODIFIER : includ Ouvrage ID into a combo box to avoid furute bugs
                            sousTraitModifier.OuvrageID = int.Parse(comboBoxOuvrageID.SelectedValue.ToString());
                            sousTraitModifier.Date_Debut_SousTraitant = datePkrDebutSousTraitant.Text;
                            sousTraitModifier.Date_Fin_SousTraitant = datePkrFinSousTraitant.Text;

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
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); }

            }



        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxSousTraitantID.Text = "";
            txtBoxDomainSousTraitant.Text = "";
            comboBoxOuvrageID.Text = "";
            datePkrDebutSousTraitant.Text = "";
            datePkrFinSousTraitant.Text = "";
        }



        private void ListViewSousTraitants_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            if (ListViewSousTraitants.SelectedItem is Sous_Traitant sous_traitant)
            {
                datePkrDebutSousTraitant.Text = sous_traitant.Date_Debut_SousTraitant.ToString();
                datePkrFinSousTraitant.Text = sous_traitant.Date_Fin_SousTraitant.ToString();
                txtBoxSousTraitantID.Text = sous_traitant.SousTraitantID.ToString();
                txtBoxDomainSousTraitant.Text = sous_traitant.DomainSousTraitant.ToString();
                comboBoxOuvrageID.Text = sous_traitant.OuvrageID.ToString();
            }

        }

        private bool verifierChamps()
        {
            bool bienRempli;

            if(string.IsNullOrEmpty(txtBoxDomainSousTraitant.Text) || datePkrDebutSousTraitant.SelectedDate == null || datePkrFinSousTraitant.SelectedDate == null || comboBoxOuvrageID.SelectedIndex == -1)
            {
                bienRempli = false;
            }
            else
            {
                bienRempli = true;
            }
            return bienRempli;
        }
    }
}


