﻿using System;
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
                comboBoxEquipeID.ItemsSource = dbEntities.Equipes.ToList();
            }
        }

        // Verifier intanciation de Employe dans l'atribut Employe ID
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btn ajouter");
            MessageBox.Show("quipe id from combobox : " + comboBoxEquipeID.SelectedValue.ToString());
            string equipeIdCombo = comboBoxEquipeID.SelectedValue.ToString();
            MessageBox.Show("quipe id from combobox : " + equipeIdCombo);

            int equipeSelectedId = int.Parse(equipeIdCombo);

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                Employe lastEmploye = dbEntities.Employes.ToArray().LastOrDefault();
                int lastnumber = lastEmploye.EmployeID + 1;
                MessageBox.Show("last id employe from database : " + lastnumber);
                Equipe equipeCherche = dbEntities.Equipes.SingleOrDefault(x => x.EquipeID == equipeSelectedId);
                

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
                    // EquipeID = int.Parse(txtBoxEquipeID.Text),
                };


                if (newEmploye != null)
                {

                   // newEmploye.EmployeID = lastEmploye.EmployeID + 1;


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
                        //emplModifier.EquipeID = int.Parse(txtBoxEquipeID.Text);
                        emplModifier.EquipeID = int.Parse(comboBoxEquipeID.SelectedValue.ToString());
                        emplModifier.EmployeMotPasse = txtBoxMotPasse.Text;
                       // EquipeID = int.Parse(comboBoxEquipeID.SelectedValue.ToString()),

               
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
           // txtBoxEquipeID.Text = "";
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
               // txtBoxEquipeID.Text = employe.EquipeID.ToString();
                comboBoxEquipeID.SelectedValue = employe.EquipeID.ToString();
                txtBoxMotPasse.Text = employe.EmployeMotPasse.ToString();
                txtBoxPosteEmploi.Text = employe.PosteEmploi;
            }

        }
    }
}


