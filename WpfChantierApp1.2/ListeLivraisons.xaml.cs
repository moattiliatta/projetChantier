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

        private void ListViewMateriaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Materiaux materiauxSelected = (Materiaux)ListViewMateriaux.SelectedItem;

            if (ListViewMateriaux.SelectedItem is Materiaux materiaux)
            {
                txtBoxMateriauxID.Text = materiaux.MateriauxID.ToString();
                txtBoxNomMateriaux.Text = materiaux.NomMateriaux;

            }
        }

        public void AfficherMateriaux()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewMateriaux.ItemsSource = dbEntities.Materiauxes.ToList();
                comboBoxOuvrageID.ItemsSource = dbEntities.Ouvrages.ToList();
            }
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxMateriauxID.Text = "";
            txtBoxNomMateriaux.Text = "";

        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
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

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            //string ouvrageIdCombo = comboBoxOuvrageID.SelectedValue.ToString();
            //int ouvrageSelectedId = int.Parse(ouvrageIdCombo);

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {

                Materiaux lastMateriaux = dbEntities.Materiauxes.ToArray().LastOrDefault();
                int lastnumber = lastMateriaux.MateriauxID + 1;


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
        }
    }
}


