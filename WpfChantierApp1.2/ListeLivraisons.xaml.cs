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

       // ProjetChantierEntities dbEntities1 = new ProjetChantierEntities();

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
                txtBoxDateRecept.Text = materiaux.DateReception.ToString();
                txtBoxOuvrageID.Text = materiaux.OuvrageID.ToString();
            }
        }

        public void AfficherMateriaux()
        {
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                ListViewMateriaux.ItemsSource = dbEntities.Materiauxes.ToList();
            }
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxMateriauxID.Text = "";
            txtBoxNomMateriaux.Text = "";
            txtBoxDateRecept.Text = "";
            txtBoxOuvrageID.Text = "";
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
                        // int newID = int.Parse(txtBoxOuvrageID.Text);
                        materiauxModifier.NomMateriaux = txtBoxNomMateriaux.Text;
                        materiauxModifier.DateReception = txtBoxDateRecept.Text;
                        materiauxModifier.OuvrageID = int.Parse(txtBoxOuvrageID.Text);

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

            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                //Verifier et debbugger code
                Materiaux newMateriel = new Materiaux()
                {

                    MateriauxID = dbEntities.Materiauxes.LastOrDefault<Materiaux>().MateriauxID + 1,
                    NomMateriaux = txtBoxNomMateriaux.Text,
                    DateReception = txtBoxDateRecept.Text,
                    OuvrageID = int.Parse(txtBoxOuvrageID.Text),

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


/*
private void AfficherMateriel(Materiaux materiauxSelect)
{
    int materiauxID = materiauxSelect.MateriauxID;
    MessageBox.Show("MAteriel selected : " + materiauxSelect.MateriauxID + " " + materiauxSelect.NomMateriaux + "\n" + "var materiauxID = " + materiauxID);
    using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
    {
        var query = from materiaux in dbEntities.Materiauxes
                    where materiaux.MateriauxID == materiauxID
                    select new
                    {
                        MateriauxID = materiaux.MateriauxID,
                        NomMateriaux = materiaux.NomMateriaux,
                        DateReception = materiaux.DateReception,
                        OuvrageID = materiaux.OuvrageID
                    };

        ListViewMateriaux.ItemsSource = query.ToList();

    }
}
*/