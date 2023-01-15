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
    /// Interaction logic for SanteSecuriteInterface.xaml
    /// </summary>
    public partial class SanteSecuriteInterface : Window
    {
        Employe employeSession;
        int employeID;

        public SanteSecuriteInterface(Employe employeSession)
        {
            this.employeSession = employeSession;
            this.employeID = employeSession.EmployeID;


            //MISSING : establecer coneccion entre mi instancia in la informacion real en mi base de datos 
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {

                

                InitializeComponent();
                AfficherEmployeSession();
            }

        }



        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {

            chkBoxBottes.IsChecked = false;
            chkBoxCasque.IsChecked = false;
            chkBoxLunettes.IsChecked = false;
        }

        private void AfficherEmployeSession()
        {
            string message = "Bienvenue : ";
            txtBlockPrenom.Text = message + employeSession.Prenom + " " + employeSession.Nom;
           // MessageBox.Show(message + " " +employeSession.Prenom + " "+employeSession.EmployeID);

        }

    }
}
