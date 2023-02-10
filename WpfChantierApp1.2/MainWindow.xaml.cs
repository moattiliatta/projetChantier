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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfChantierApp1._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

        }

        //événement qui valide les informations saisies par l'utilisateur dans l'interface,
        //crée un objet du même type et le compare avec la liste des employés, instancie chaque interface selon le choix de l'utilisateur. 
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            string nomUser = txtBoxNomUser.Text;
            string psswrdUser = txtBoxPassword.Text;
            // variable vraie ou fausse selon la vérification de la fonction 
            bool autentifier = authentificateur(nomUser, psswrdUser);
            string nomMessage;

            if ((nomUser != null) && (psswrdUser != null) && (autentifier))
            {
                // connexion à la base de données 
                using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                {

                    // LINKQ : request to get the employe ID from Database
                    int employeID = (from employe in dbEntities.Employes
                                     where employe.Prenom == nomUser && employe.EmployeMotPasse == psswrdUser
                                     select employe.EmployeID).FirstOrDefault();

                    // Instance of employe from DB where Employe ID is equal to the id found from the request
                    Employe employeFound = dbEntities.Employes.FirstOrDefault(emp => emp.EmployeID == employeID);


                    if ((btnAdmin.IsChecked == true) && (employeFound.EquipeID == 6)) //Administration
                    {
                        nomMessage = employeFound.Nom;
                        MessageBox.Show("Option Admin sélectionnée, bienvenue : " + nomMessage + " Equipe id : " + employeFound.EquipeID);

                        // Instanciation de l'AdministrationInterface
                        AdministrationInter admin = new AdministrationInter(employeFound);
                        admin.ShowDialog();

                    }

                    else if ((btnChef.IsChecked == true) && (employeFound.PosteEmploi == "Superviseur")) //Souperviseur
                    {
                        MessageBox.Show("Option Chef sélectionnée");
                        nomMessage = employeFound.Nom;
                        MessageBox.Show("Option Admin sélectionnée, bienvenue : " + nomMessage + " Equipe id : " + employeFound.EquipeID);
                        ChefEquipeInter chef = new ChefEquipeInter(employeFound);
                        chef.ShowDialog();

                    }
                    else if ((btnEmploye.IsChecked == true)) // && (employeFound.EquipeID == 2)) //Travailleurs
                    {

                        MessageBox.Show("Option Employe sélectionnée, bienvenue : " + employeFound.Nom + " Equipe id : " + employeFound.EquipeID);
                        SanteSecuriteInterface sante = new SanteSecuriteInterface(employeFound);
                        // Instanciation de SanteSecuriteInterface
                        sante.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Veuillez entrer les informations correctes");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer les informations correctes");
            }

        }

        // l'événement supprime les informations dans l'interface
        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            btnAdmin.IsChecked = false;
            btnChef.IsChecked = false;
            btnEmploye.IsChecked = false;
            txtBoxNomUser.Text = "";
            txtBoxPassword.Text = "";
        }

        // vérification du nom d'utilisateur et du mot de passe
        private bool authentificateur(string nomUser, string psswrdUser)
        {
            bool existe = false;

            // connexion à la base de données 
            using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
            {
                // Requête qui recherche les valeurs saisies par l'utilisateur dans la base de données. 
                var query = from employe in dbEntities.Employes
                            where employe.Prenom == nomUser && employe.EmployeMotPasse == psswrdUser
                            select employe;
                // si des correspondances sont trouvées, l'employé est validé. 
                if (query.Count() > 0)
                {
                    existe = true;
                }
                else
                {
                    existe = false;
                }
            }
            return existe;
        }

    }
}


