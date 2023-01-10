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

        //TEST FOR BDD

            //liste temporaire d'utilisateurs pour vérifier les informations saisies par l'utilisateur 
            public List<User> userList = new List<User>();
            User user1 = new User("Nelson", "123");
            User user2 = new User("Alfonso", "321");
            User user3 = new User("Cuervo", "963");

        ProjetChantierEntities dbEntities = new ProjetChantierEntities();

        public MainWindow()
            {
                InitializeComponent();
                userList.Add(user1);
                userList.Add(user2);
                userList.Add(user3);
            }

            //événement qui valide les informations saisies par l'utilisateur dans l'interface,
            //crée un objet du même type et le compare avec la liste des employés, instancie chaque interface selon le choix de l'utilisateur. 
            private void btnValider_Click(object sender, RoutedEventArgs e)
            {
                string nomUser = txtBoxNomUser.Text;
                string psswrdUser = txtBoxPassword.Text;
                bool autentifier = authentificateur(nomUser, psswrdUser);

                //MessageBox.Show(nomUser);
                //MessageBox.Show(psswrdUser);
                //MessageBox.Show(" autentifier  = "+autentifier);

                if ((btnAdmin.IsChecked == true) && (autentifier))
                {
                    MessageBox.Show("Option Admin sélectionnée");
                    // Instanciation de l'AdministrationInterface
                    AdministrationInter admin = new AdministrationInter();
                    admin.ShowDialog();

                }
                else if ((btnChef.IsChecked == true) && (autentifier))
                {
                    MessageBox.Show("Option Chef sélectionnée");

                }
                else if ((btnEmploye.IsChecked == true) && (autentifier))
                {
                    MessageBox.Show("Option Employe sélectionnée");
                    SanteSecuriteInterface sante = new SanteSecuriteInterface();
                    // Instanciation de SanteSecuriteInterface
                    sante.ShowDialog();

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
            //bool existe = false;

            //User userVerif = new User(nomUser, psswrdUser);

            //foreach (User user in userList)
            //{
            //    if (user.nom == userVerif.nom && user.password == userVerif.password)
            //    {
            //        return existe = true;
            //    }
            //    else
            //    {
            //        return existe = false;
            //    }
            //}
            //return existe;

            bool existe = false;
            
            Employe userVerif = new Employe(nomUser, psswrdUser );

            if (userVerif != null)
            {
                //dbEntities
                Employe emplFinder = dbEntities.Employes.FirstOrDefault(empl => empl.Nom == userVerif.Nom); // *** LINKQ *******
                if (emplFinder != null) { existe = true; }
                else { existe = false; }

            }

            return existe;
        }

    }

}
   

