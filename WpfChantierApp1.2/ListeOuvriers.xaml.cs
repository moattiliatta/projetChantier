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

        public void AfficherEmployes()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewOuvriers.ItemsSource = dbEntities.Employes.ToList();
                // remplir notre combobox avec les ID des appareils existants dans la base de données 
                comboBoxEquipeID.ItemsSource = dbEntities.Equipes.ToList();

               
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn ajouter");
            // rechercher et enregistrer l'identifiant de l'équipement qui a été sélectionné
            string equipeIdCombo = comboBoxEquipeID.SelectedValue.ToString();
            int equipeSelectedId = int.Parse(equipeIdCombo);

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
             /* trouver le dernier enregistrement de l'employé dans la base de données, 
                enregistrer son ID + 1 afin que nous puissions instancier notre nouvel objet en itérant
                la valeur des enregistrements et faire correspondre les valeurs de la BD et de nos objets.  */
                Employe lastEmploye = dbEntities.Employes.ToArray().LastOrDefault();
               
                Equipe equipeCherche = dbEntities.Equipes.SingleOrDefault(x => x.EquipeID == equipeSelectedId);

                //  contrôle d'exception, vérifiez que tous les champs d'information de l'interface sont correctement remplis. 
                try
                {
                    // instance de notre nouvel objet 
                    Employe newEmploye = new Employe()
                    {
                        EmployeID = lastEmploye.EmployeID + 1,
                        Nom = txtBoxEmployeNom.Text,
                        Prenom = txtBoxEmployePreNom.Text,
                        DateEmbauche = datePkrDateEmbauche.SelectedDate.Value,
                        Telephone = txtBoxTelephone.Text,
                        EquipeID = int.Parse(comboBoxEquipeID.SelectedValue.ToString()),
                        PosteEmploi = txtBoxPosteEmploi.Text,
                        EmployeMotPasse = txtBoxMotPasse.Text,
                        Equipe = equipeCherche,
                    };

                    if (newEmploye != null)
                    {
                        dbEntities.Employes.Add(newEmploye);

                        int resultat = dbEntities.SaveChanges();
                        if (resultat > 0)
                        {
                            this.AfficherEmployes();
                            string message = $"L'employé {newEmploye.Nom} a été enregistré dans le système";
                            MessageBox.Show(message);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n\nATTENTION: \nVérifiez que tous les champs sont correctement remplis.  ");
                }
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn supprimer");

            Employe employeSelected = (Employe)ListViewOuvriers.SelectedItem;

            if (employeSelected != null)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    Employe emplDeleted = dbEntities.Employes.SingleOrDefault(empl => empl.EmployeID == employeSelected.EmployeID);

                    if (emplDeleted != null)
                    {
                        dbEntities.Employes.Remove(emplDeleted);
                        int resultat = dbEntities.SaveChanges();
                        if (resultat > 0)
                        {
                            this.AfficherEmployes();
                            string message = $"L'employe {emplDeleted.Nom} a ete supprime";
                            MessageBox.Show(message);
                        }
                    }
                }
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn modifier");

            Employe employeSelected = (Employe)ListViewOuvriers.SelectedItem;

            if (employeSelected != null)
            {
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {
                    Employe emplModifier = dbEntities.Employes.FirstOrDefault(empl => empl.EmployeID == employeSelected.EmployeID); // **** LINQ  **** 

                    if (emplModifier != null)
                    {                     
                        emplModifier.DateEmbauche = datePkrDateEmbauche.SelectedDate.Value;
                        emplModifier.Nom = txtBoxEmployeNom.Text;
                        emplModifier.Prenom = txtBoxEmployePreNom.Text;
                        emplModifier.EmployeID = int.Parse(txtBoxEmployeID.Text);
                        emplModifier.Telephone = txtBoxTelephone.Text;
                        emplModifier.PosteEmploi = txtBoxPosteEmploi.Text;

                        /* Reference object error when update */
                        emplModifier.EquipeID = int.Parse(comboBoxEquipeID.SelectedValue.ToString());
                        emplModifier.EmployeMotPasse = txtBoxMotPasse.Text;
              
                        int resultat = dbEntities.SaveChanges();
                        if (resultat > 0)
                        {
                            this.AfficherEmployes();
                            string message = $"L'employe {emplModifier.Nom} a ete modifie";
                            MessageBox.Show(message);
                        }
                    }
                }
            }
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            datePkrDateEmbauche.Text = "";
            txtBoxEmployeID.Text = "";
            txtBoxEmployeNom.Text = "";
            txtBoxEmployePreNom.Text = "";
            txtBoxTelephone.Text = "";
            txtBoxMotPasse.Text = "";
            txtBoxPosteEmploi.Text = "";
        }

        private void ListViewOuvriers_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewOuvriers.SelectedItem is Employe employe)
            {
                datePkrDateEmbauche.Text = employe.DateEmbauche.ToString();
                txtBoxEmployeID.Text = employe.EmployeID.ToString();
                txtBoxEmployeNom.Text = employe.Nom;
                txtBoxEmployePreNom.Text = employe.Prenom;
                txtBoxTelephone.Text = employe.Telephone;
                comboBoxEquipeID.Text = employe.EquipeID.ToString();
                txtBoxMotPasse.Text = employe.EmployeMotPasse.ToString();
                txtBoxPosteEmploi.Text = employe.PosteEmploi;
            }

        }
    }
}


