using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ChefEquipeInter.xaml
    /// </summary>
    public partial class ChefEquipeInter : Window
    {
        Employe employeSession;
        int sessionEmployeEquipeID = 0;
        Equipe equipeSession;
        //Ouvrage ouvrageSession;
        int equipeID = 0;
        //List<Ouvrage> ouvrlist = new List<Ouvrage>();
        ObservableCollection<Ouvrage> ouvrlist = new ObservableCollection<Ouvrage>();

        public ChefEquipeInter(Employe employeSession)
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                InitializeComponent();
                // Session = user found in a database
                this.employeSession = employeSession;

                txtBlockNomUser.Text = "Nom du superviseur : " + this.employeSession.Prenom;
                sessionEmployeEquipeID = (int)this.employeSession.EquipeID;
                // instance of Equipe
                this.equipeSession = dbEntities.Equipes.SingleOrDefault(eq => eq.EquipeID == sessionEmployeEquipeID);
                //instance of Ouvrage
                //this.ouvrageSession = dbEntities.Ouvrages.SingleOrDefault(ouvr => ouvr.OuvrageID == sessionEmployeEquipeID);
                // set value with equipe ID session 
                equipeID = equipeSession.EquipeID;

                txtBlockNomEquipe.Text = "Equipe : " + this.equipeSession.NomDepartement;
                //txtBoxEquipeID.Text = this.employeSession.EquipeID.ToString();

                AfficherOuvrage();
            }

        }

        private void btnAfficher_Click(object sender, RoutedEventArgs e)
        {
            AfficherOuvrage();
        }

        private void AfficherOuvrage()
        {
            Ouvrage monOuvrage = null;

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                foreach (Ouvrage o in dbEntities.Ouvrages)
                {
                    if (o.EquipeID == this.equipeID)
                    {
                        monOuvrage = o;
                        ouvrlist.Add(monOuvrage);
                    }
                }

                if (ouvrlist.Count > 0) { ListViewOuvrage.ItemsSource = ouvrlist; }
                else { MessageBox.Show("Actuellement, votre équipe n'a aucun ouvrage en cours"); }
            }
        }


        private void ListViewOuvrage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ouvrage ouvrageSelected = (Ouvrage)ListViewOuvrage.SelectedItem;
            int selectedEquipeID = 0;

            if (ListViewOuvrage.SelectedItem is Ouvrage ouvrage)
            {
                //ffiche dans les zones de texte les valeurs sélectionnées.
                txtBoxOuvrageID.Text = ouvrage.OuvrageID.ToString();
                txtBoxNomOuvrage.Text = ouvrage.NomOuvrage;
                txtBoxDescOuvrage.Text = ouvrage.Description_Ouvrage;
                txtBoxEquipeID.Text = ouvrage.EquipeID.ToString();
                //récupère l'Id de l'équipe pour chaque sélection 
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

        // Rechercher la création et l'ajout d'un nouvel enregistrement dans la BD
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            string equipeIdCombo = txtBoxEquipeID.Text;
            int equipeSelectedId = int.Parse(equipeIdCombo);

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                // recherche le dernier élément stocké dans la table d'Ouvrages 
                Ouvrage lastOuvrage = dbEntities.Ouvrages.ToArray().LastOrDefault();
                // sauvegarde le dernier ID enregistré dans la table + 1 
                int lastOuvrageID = lastOuvrage.OuvrageID + 1;

                Equipe equipeCherche = dbEntities.Equipes.SingleOrDefault(x => x.EquipeID == equipeSelectedId); // requête LINQ

                Ouvrage newOuvrage = new Ouvrage()
                {
                    OuvrageID = lastOuvrageID,
                    NomOuvrage = txtBoxNomOuvrage.Text,
                    Description_Ouvrage = txtBoxDescOuvrage.Text,

                    EquipeID = this.equipeID,
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

        // Il modifie  un objet de type Ouvrage , recherche dans la BD les correspondances d'identifiants et modifie les informations de l'enregistrement.
        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
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
                        ouvrModifier.EquipeID = int.Parse(txtBoxEquipeID.Text);
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

        // Réinitialise les zones de texte de l'interface avec une chaîne vide.
        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxOuvrageID.Text = "";
            txtBoxNomOuvrage.Text = "";
            txtBoxDescOuvrage.Text = "";
            datePkrDebutOuvrage.Text = "";
            datePkrFinOuvrage.Text = "";
            //comboBoxEquipeID.Text = "";
        }

        // Crée un objet de type Ouvrage selon la sélection de l'utilisateur, fait une recherche dans la BD et s'il trouve des correspondances d'ID, le supprime. 
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
