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
    /// Interaction logic for AdministrationInter.xaml
    /// </summary>
    public partial class AdministrationInter : Window
    {
        public AdministrationInter()
        {
            InitializeComponent();
        }

        //private void btnListMacons_Click(object sender, RoutedEventArgs e)
        //{
        //    //AdministrationInter admin = new AdministrationInter();
        //    //admin.ShowDialog();
        //    MessageBox.Show("Liste macons sélectionnée");
        //}

        private void btnListPeintres_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Peintres sélectionnée");
            ListePeintres peintres = new ListePeintres();
            peintres.ShowDialog();
           
        }

        private void btnListSoutraitent_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Soutraitent sélectionnée");
            ListeSousTraitants soustraitent = new ListeSousTraitants();
            soustraitent.ShowDialog();
            
        }

        private void btnLivraison_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Livraison sélectionnée");
            ListeLivraisons livraisons = new ListeLivraisons();
            livraisons.ShowDialog();
            
        }

        private void btnRegleSecurite_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Regle Securite sélectionnée, est en cours de développement ");
        }

        private void btnListOuvriers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Liste Ouvriers sélectionnée");
            ListeOuvriers ouvriers = new ListeOuvriers();
            ouvriers.ShowDialog();
           
        }
    }
}
