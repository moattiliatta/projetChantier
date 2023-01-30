using System;
using System.Collections;
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
    /// Interaction logic for ChefEquipeInter.xaml
    /// </summary>
    public partial class ChefEquipeInter : Window
    {
        Employe employeSession;
        int sessionEmployeEquipeID = 0;
        Equipe equipeSession;
        Ouvrage ouvrageSession;
        int equipeID = 0;

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
                this.ouvrageSession = dbEntities.Ouvrages.SingleOrDefault(ouvr => ouvr.OuvrageID == sessionEmployeEquipeID);
                // set value with equipe ID session 
                equipeID = equipeSession.EquipeID;

                txtBlockNomEquipe.Text = "Equipe : " + equipeSession.NomDepartement;

                afficherOuvrage();
            }

        }

        private void btnAfficher_Click(object sender, RoutedEventArgs e)
        {
            afficherOuvrage();
        }

        private void afficherOuvrage()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                var query = from ouvr in dbEntities.Ouvrages
                            where ouvr.EquipeID == this.equipeID
                            select new
                            {
                                OuvrageID = ouvr.OuvrageID,
                                NomOuvrage = ouvr.NomOuvrage,
                                Description_Ouvrage = ouvr.Description_Ouvrage,
                                EquipeID = ouvr.EquipeID,
                                Date_Debut_Ouvrage = ouvr.Date_Debut_Ouvrage,
                                Date_Fin_Ouvrage = ouvr.Date_Fin_Ouvrage
                            };

                ListViewOuvrage.ItemsSource = query.ToList();

            }
        }

        private void ListViewOuvrage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedEquipeID = 0;

            if (ListViewOuvrage.SelectedItem is Ouvrage ouvrage)
            {
                //ffiche dans les zones de texte les valeurs sélectionnées.
                txtBoxOuvrageID.Text = ouvrage.OuvrageID.ToString();
                txtBoxNomOuvrage.Text = ouvrage.NomOuvrage;
                txtBoxDescOuvrage.Text = ouvrage.Description_Ouvrage;
                //récupère l'Id de l'équipe pour chaque sélection 
                selectedEquipeID = ouvrage.EquipeID.Value;
            }
        }
    }
}





//private void AfficherMateriaux(Ouvrage ouvrageSelected)
//{
//    int ouvrageID = ouvrageSelected.OuvrageID;

//    using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
//    {
//        comboBoxOuvrageID.ItemsSource = dbEntities.Ouvrages.ToList();
//        var query = from materiau in dbEntities.Materiauxes
//                    where materiau.OuvrageID == ouvrageID
//                    select new
//                    {
//                        MateriauxID = materiau.OuvrageID,
//                        NomMateriaux = materiau.NomMateriaux,
//                        DateReception = materiau.DateReception,
//                        OuvrageID = materiau.OuvrageID,
//                    };
//        ListViewMateriaux.ItemsSource = query.ToList();
//    }
//}