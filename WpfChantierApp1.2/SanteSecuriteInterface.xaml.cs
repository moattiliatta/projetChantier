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
    /// Interaction logic for SanteSecuriteInterface.xaml
    /// </summary>
    public partial class SanteSecuriteInterface : Window
    {
        Employe employeSession;
        int employeID;
        string message = "Bienvenue : ";

        public SanteSecuriteInterface(Employe employeSession)
        {
            this.employeSession = employeSession;
            this.employeID = employeSession.EmployeID;

            InitializeComponent();
            AfficherEmployeSession();

        }



        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            chkBoxBottes.IsChecked = false;
            chkBoxCasque.IsChecked = false;
            chkBoxLunettes.IsChecked = false;
        }

        private void AfficherEmployeSession()
        {
            //string message = "Bienvenue : ";
            txtBlockPrenom.Text = message + employeSession.Prenom + " " + employeSession.Nom;
            // MessageBox.Show(message + " " +employeSession.Prenom + " "+employeSession.EmployeID);

        }

        private void btnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            //MISSING : establecer coneccion entre mi instancia in la informacion real en mi base de datos 
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                Employe emploAsistence = dbEntities.Employes.FirstOrDefault(empl => empl.EmployeID == employeSession.EmployeID);

                if (chkBoxBottes.IsChecked != true || chkBoxCasque.IsChecked != true || chkBoxLunettes.IsChecked != true)
                {

                    message = "l'employé : " + employeSession.Prenom + " " + employeSession.EmployeID + "\n" +
                               "ne respectent pas les règles de sécurité\n" + "présence quotidienne refusée";
                    MessageBox.Show(message);

                    //emploAsistence.EmplAssistence = "Absent";

                }
                else
                {

                    //Employe employeAsistence
                    message = "l'employé : " + employeSession.Prenom + " " + employeSession.EmployeID + "\n" +
                         "respecte toutes les règles de sécurité\n" + "Présence quotidienne OK ";
                    MessageBox.Show(message);

                   // emploAsistence.EmplAssistence = "Present";
                }

            }

            //Application.Current.Shutdown();

        }
    }
}

