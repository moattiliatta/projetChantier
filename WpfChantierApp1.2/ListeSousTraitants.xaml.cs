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
    /// Interaction logic for ListeSousTraitants.xaml
    /// </summary>
    public partial class ListeSousTraitants : Window
    {
        public ListeSousTraitants()
        {
            InitializeComponent();
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
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            txtBoxSousTraitantID.Text = "";
            txtBoxDomainSousTraitant.Text = "";
            txtBoxOuvrageID.Text = "";
            txtBoxDebutSousTraitant.Text = "";
            txtBoxFinSousTraitant.Text = "";
        }
    }
}
