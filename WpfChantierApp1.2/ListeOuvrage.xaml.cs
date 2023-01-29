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
            Ouvrage ouvrageSelected = (Ouvrage)ListViewOuvrage.SelectedItem;
            int selectedEquipeID = 0;

            if (ListViewOuvrage.SelectedItem is Ouvrage ouvrage)
            {
                txtBoxOuvrageID.Text = ouvrage.OuvrageID.ToString();
                txtBoxNomOuvrage.Text = ouvrage.NomOuvrage;
                txtBoxDescOuvrage.Text = ouvrage.Description_Ouvrage;
                selectedEquipeID = ouvrage.EquipeID.Value;
            }
            AfficherMateriaux(ouvrageSelected);
            AfficherEmployes(selectedEquipeID);
        }

        private void AfficherEmployes(int selectedEquipeID)
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                var query = from empl in dbEntities.Employes
                            where empl.EmployeID == selectedEquipeID
                            select new
                            {
                                EmployeID = empl.EmployeID,
                                Nom = empl.Nom,
                                Prenom = empl.Prenom,
                                DateEmbauche = empl.DateEmbauche,
                                Telephone = empl.Telephone,
                                EquipeID = empl.EquipeID,
                                PosteEmploi = empl.PosteEmploi,
                                EmployeMotPasse = empl.EmployeMotPasse,
                               // ValiderSanteSecuritaire = empl.ValiderSanteSecuritaire,
                                Equipe = empl.Equipe
                            };
                ListViewTravailleurs.ItemsSource = query.ToList();
            }
        }

        private void AfficherMateriaux(Ouvrage ouvrageSelected)
        {
            int ouvrageID = ouvrageSelected.OuvrageID;

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
               // comboBoxOuvrageID.ItemsSource = dbEntities.Ouvrages.ToList();
                var query = from materiau in dbEntities.Materiauxes
                            where materiau.OuvrageID == ouvrageID
                            select new
                            {
                                MateriauxID = materiau.OuvrageID,
                                NomMateriaux = materiau.NomMateriaux,
                                DateReception = materiau.DateReception,
                                OuvrageID = materiau.OuvrageID,
                            };
                ListViewMateriaux.ItemsSource = query.ToList();
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
            MessageBox.Show("btn Ajouter");

            string equipeIdCombo = comboBoxEquipeID.SelectedValue.ToString();
            int equipeSelectedId = int.Parse(equipeIdCombo);

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                Ouvrage lastOuvrage = dbEntities.Ouvrages.ToArray().LastOrDefault();
                int lastOuvrageID = lastOuvrage.OuvrageID + 1;

                Equipe equipeCherche = dbEntities.Equipes.SingleOrDefault(x => x.EquipeID == equipeSelectedId);


                Ouvrage newOuvrage = new Ouvrage()
                {
                    OuvrageID = lastOuvrageID,
                    NomOuvrage = txtBoxNomOuvrage.Text,
                    Description_Ouvrage = txtBoxDescOuvrage.Text,

                    EquipeID = int.Parse(comboBoxEquipeID.SelectedValue.ToString()),
                    Date_Debut_Ouvrage = datePkrDebutOuvrage.SelectedDate.Value.ToString(),
                    Date_Fin_Ouvrage = datePkrFinOuvrage.SelectedDate.Value.ToString(),
                    Equipe = equipeCherche,
                };

                if (newOuvrage != null)
                {
                    dbEntities.Ouvrages.Add(newOuvrage);

                    int resultat = dbEntities.SaveChanges();
                    if (resultat > 0)
                    {
                        this.AfficherOuvrage();
                        string message = $"L'ouvrage {newOuvrage.NomOuvrage} a été enregistré dans le système";
                        MessageBox.Show(message);
                    }
                }
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn supprimer");

            Ouvrage ouvrageSelected = (Ouvrage)ListViewOuvrage.SelectedItem;

            if (ouvrageSelected != null)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    Ouvrage ouvrDeleted = dbEntities.Ouvrages.SingleOrDefault(ouvr => ouvr.OuvrageID == ouvrageSelected.OuvrageID);

                    if (ouvrDeleted != null)
                    {
                        dbEntities.Ouvrages.Remove(ouvrDeleted);
                        int resultat = dbEntities.SaveChanges();
                        if (resultat > 0)
                        {
                            this.AfficherOuvrage();
                            string message = $"L'ouvrage {ouvrDeleted.NomOuvrage} a ete supprime";
                            MessageBox.Show(message);
                        }
                    }
                }
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn modifier");

            Ouvrage ouvrageSelected = (Ouvrage)ListViewOuvrage.SelectedItem;

            if (ouvrageSelected != null)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    Ouvrage ouvrModifier = dbEntities.Ouvrages.SingleOrDefault(ouvr => ouvr.OuvrageID == ouvrageSelected.OuvrageID);

                    if (ouvrModifier != null)
                    {

                        ouvrModifier.OuvrageID = int.Parse(txtBoxOuvrageID.Text);
                        ouvrModifier.NomOuvrage = txtBoxNomOuvrage.Text;
                        ouvrModifier.Description_Ouvrage = txtBoxDescOuvrage.Text;
                        ouvrModifier.EquipeID = int.Parse(comboBoxEquipeID.SelectedValue.ToString());
                        ouvrModifier.Date_Debut_Ouvrage = datePkrDebutOuvrage.SelectedDate.Value.ToString();
                        ouvrModifier.Date_Fin_Ouvrage = datePkrFinOuvrage.SelectedDate.Value.ToString();

                        int resultat = dbEntities.SaveChanges();
                        if (resultat > 0)
                        {
                            this.AfficherOuvrage();
                            string message = $"L'ouvrage {ouvrModifier.NomOuvrage} a ete modifie";
                            MessageBox.Show(message);
                        }
                    }
                }
            }
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxOuvrageID.Text = "";
            txtBoxNomOuvrage.Text = "";
            txtBoxDescOuvrage.Text = "";
            datePkrDebutOuvrage.Text = "";
            datePkrFinOuvrage.Text = "";
            comboBoxEquipeID.Text = "";

        }

        private void ListViewMateriaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //if (ListViewMateriaux.SelectedItem is Materiaux materiau)
            //{

            //    //txtBoxMateriauxID.Text = materiau.MateriauxID.ToString();
            //    //txtBoxMateriauxNom.Text = materiau.NomMateriaux.ToString();

            //    //txtBoxOuvrageID.Text = ouvrage.OuvrageID.ToString();
            //    //txtBoxNomOuvrage.Text = ouvrage.NomOuvrage;
            //    //txtBoxDescOuvrage.Text = ouvrage.Description_Ouvrage;
            //    //txtBoxEquipeId.Text = ouvrage.EquipeID.ToString();
            //    //txtBoxDebutOuvrage.Text = ouvrage.Date_Debut_Ouvrage.ToString();
            //    //txtBoxFinOuvrage.Text = ouvrage.Date_Fin_Ouvrage.ToString() ;
            //}

        }
    }
}
