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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace WpfChantierApp1._2
{
    /// <summary>
    /// Interaction logic for ListeLivraisons.xaml
    /// </summary>
    public partial class ListeLivraisons : Window
    {

        public ListeLivraisons()
        {
            InitializeComponent();
            AfficherMateriaux();
        }
        // affiche dans les boîtes de texte les informations trouvées dans la base de données, 
        private void ListViewMateriaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Materiaux materiauxSelected = (Materiaux)ListViewMateriaux.SelectedItem;

            if (ListViewMateriaux.SelectedItem is Materiaux materiaux)
            {
                txtBoxMateriauxID.Text = materiaux.MateriauxID.ToString();
                txtBoxNomMateriaux.Text = materiaux.NomMateriaux;
            }
        }

        // affiche dans les contrôleurs ListView et ComboBox les informations stockées dans les tables Matériaux et Ouvrages.
        public void AfficherMateriaux()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewMateriaux.ItemsSource = dbEntities.Materiauxes.ToList();
                comboBoxOuvrageID.ItemsSource = dbEntities.Ouvrages.ToList();
            }
        }
        // Réinitialise les zones de texte de l'interface avec une chaîne vide.
        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxMateriauxID.Text = "";
            txtBoxNomMateriaux.Text = "";
        }

        // Crée un objet de type Matériaux selon la sélection de l'utilisateur, recherche dans la BD les identifiants correspondants et modifie les informations de l'enregistrement.
        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            bool verifierOK = verifierChamps();

            if (verifierOK)
            {
                Materiaux materiauxSelected = (Materiaux)ListViewMateriaux.SelectedItem;

                if (materiauxSelected != null)
                {
                    using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                    {
                        Materiaux materiauxModifier = dbEntities.Materiauxes.FirstOrDefault(mtr => mtr.MateriauxID == materiauxSelected.MateriauxID);
                        if (materiauxModifier != null)
                        {
                            materiauxModifier.NomMateriaux = txtBoxNomMateriaux.Text;
                            materiauxModifier.DateReception = datePkrDateRecept.SelectedDate.Value.ToShortDateString();
                            materiauxModifier.OuvrageID = int.Parse(comboBoxOuvrageID.SelectedValue.ToString());

                            int resultT = dbEntities.SaveChanges();
                            if (resultT > 0)
                            {
                                this.AfficherMateriaux();
                                string message = $"le materiel {materiauxModifier.NomMateriaux} a ete modifie";
                                MessageBox.Show(message);
                            }
                        }
                    }
                }
            }
            else {

                MessageBox.Show("ATTENTION: \nVérifiez que tous les champs sont correctement remplis.");
            }
        }

        // Crée un objet de type Matériaux selon la sélection de l'utilisateur, effectue une recherche dans la BD et s'il trouve des correspondances d'ID, il le supprime. 
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Materiaux materiauxSelected = (Materiaux)ListViewMateriaux.SelectedItem;

            if (materiauxSelected != null)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    Materiaux materiauxDeleted = dbEntities.Materiauxes.SingleOrDefault(mtr => mtr.MateriauxID == materiauxSelected.MateriauxID);
                    if (materiauxDeleted != null)
                    {
                        dbEntities.Materiauxes.Remove(materiauxDeleted);

                        int resultT = dbEntities.SaveChanges();

                        if (resultT > 0)
                        {
                            this.AfficherMateriaux();
                            string message = $"le materiel {materiauxDeleted.NomMateriaux} a été modifié";
                            MessageBox.Show(message);
                        }
                    }
                }
            }
        }

        // Crée un objet de type Matériaux selon les informations données par l'utilisateur. 
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool verifierOK = verifierChamps();

            if (verifierOK)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    // recherche dans la BD le dernier enregistrement de la table des matériaux. 
                    Materiaux lastMateriaux = dbEntities.Materiauxes.ToArray().LastOrDefault();
                    // enregistre l'ID du dernier enregistrement trouvé et lui ajoute 1.
                    int lastnumber = lastMateriaux.MateriauxID + 1;

                    //  contrôle d'exception, vérifiez que tous les champs d'information de l'interface sont correctement remplis. 
                    try
                    {
                        Materiaux newMateriel = new Materiaux()
                        {
                            MateriauxID = lastnumber,
                            NomMateriaux = txtBoxNomMateriaux.Text,
                            DateReception = datePkrDateRecept.SelectedDate.Value.ToShortDateString(),
                            OuvrageID = int.Parse(comboBoxOuvrageID.SelectedValue.ToString()),
                        };

                        if (newMateriel != null)
                        {
                            dbEntities.Materiauxes.Add(newMateriel);

                            int resultT = dbEntities.SaveChanges();

                            if (resultT > 0)
                            {
                                this.AfficherMateriaux();
                                string message = $"le materiel {newMateriel.NomMateriaux} a été enregistré dans le système";
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
                MessageBox.Show("ATTENTION: \nVérifiez que tous les champs sont correctement remplis.");
            }
        }


        // fonction booléenne qui renvoie la réponse vraie si tous les champs ont été remplis correctement
        private bool verifierChamps()
        {
            bool bienRempli;

            if (string.IsNullOrEmpty(txtBoxNomMateriaux.Text) || datePkrDateRecept.SelectedDate == null || comboBoxOuvrageID.SelectedIndex == -1)
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


