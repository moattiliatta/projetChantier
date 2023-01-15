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

        //TEST USING USER CLASS
        //liste temporaire d'utilisateurs pour vérifier les informations saisies par l'utilisateur 
        //public List<User> userList = new List<User>();
        //User user1 = new User("Nelson", "123");
        //User user2 = new User("Alfonso", "321");
        //User user3 = new User("Cuervo", "963");


        public MainWindow()
        {
            InitializeComponent();
            //userList.Add(user1);
            //userList.Add(user2);
            //userList.Add(user3);
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

            // Employe employeFinder = new Employe(nomUser, psswrdUser);

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
                    else if ((btnChef.IsChecked == true) && (employeFound.EquipeID == 2)) //Souperviseur
                    {
                        MessageBox.Show("Option Chef sélectionnée");

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

            //Employe employeFinder = new Employe(nomUser, psswrdUser);

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

        //private IEnumerable<Employe> GetEmploye(string nomEmp, string psswrdEmp)
        //{          
        //    using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
        //    {
        //        return (from empl in dbEntities.Set<Employe>()
        //                where empl.Nom == nomEmp && empl.EmployeMotPasse == psswrdEmp
        //                select new
        //                {
        //                    EmployeID = empl.EmployeID,
        //                    Nom = empl.Nom,
        //                    Prenom = empl.Prenom,
        //                    DateEmbauche = empl.DateEmbauche,
        //                    Telephone = empl.Telephone,
        //                    EquipeID = empl.EquipeID,
        //                    PosteEmploi = empl.PosteEmploi,
        //                    EmployeMotPasse = empl.EmployeMotPasse,
        //                    ValiderSanteSecuritaire = empl.ValiderSanteSecuritaire,
        //                    Equipe = empl.Equipe

        //                }).ToList().Select(x => new Employe
        //                {
        //                    EmployeID = x.EmployeID,
        //                    Nom = x.Nom,
        //                    Prenom = x.Prenom,
        //                    DateEmbauche = x.DateEmbauche,
        //                    Telephone = x.Telephone,
        //                    EquipeID = x.EquipeID,
        //                    PosteEmploi = x.PosteEmploi,
        //                    EmployeMotPasse = x.EmployeMotPasse,
        //                    ValiderSanteSecuritaire = x.ValiderSanteSecuritaire,
        //                    Equipe = x.Equipe
        //                });
        //    }
        //}

        /*
                private Employe GetEmploye(string nomEmp, string psswrdEmp)
                {
                     Employe getEmploye;

                    using (ProjetChantierEntities dbEntities = new ProjetChantierEntities())
                    {                
                      return  getEmploye = (from empl in dbEntities.Employes
                                      where empl.Nom == nomEmp && empl.EmployeMotPasse == psswrdEmp
                                      select new Employe
                                      {
                                          EmployeID = empl.EmployeID,
                                          Nom = empl.Nom,
                                          Prenom = empl.Prenom,
                                          DateEmbauche = empl.DateEmbauche,
                                          Telephone = empl.Telephone,
                                          EquipeID = empl.EquipeID,
                                          PosteEmploi = empl.PosteEmploi,
                                          EmployeMotPasse = empl.EmployeMotPasse,
                                          ValiderSanteSecuritaire = empl.ValiderSanteSecuritaire,
                                          Equipe = empl.Equipe

                                      }).ToList().Select(x => new Employe
                                      {
                                          EmployeID = x.EmployeID,
                                          Nom = x.Nom,
                                          Prenom = x.Prenom,
                                          DateEmbauche = x.DateEmbauche,
                                          Telephone = x.Telephone,
                                          EquipeID = x.EquipeID,
                                          PosteEmploi = x.PosteEmploi,
                                          EmployeMotPasse = x.EmployeMotPasse,
                                          ValiderSanteSecuritaire = x.ValiderSanteSecuritaire,
                                          Equipe = x.Equipe

                                      }).FirstOrDefault();

                    }

                }
        */
    }
}


//test d'authentification avec la classe User de test 
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


//var query = (Employe)(from empl in dbEntities.Employes
//            where empl.Nom == nomUser && empl.EmployeMotPasse == psswrdUser
//            select new Employe
//            {
//                EmployeID = empl.EmployeID,
//                Nom = empl.Nom,
//                Prenom = empl.Prenom,
//                DateEmbauche = empl.DateEmbauche,
//                Telephone = empl.Telephone,
//                EquipeID = empl.EquipeID,
//                PosteEmploi = empl.PosteEmploi,
//                EmployeMotPasse = empl.EmployeMotPasse,
//                ValiderSanteSecuritaire = empl.ValiderSanteSecuritaire,
//                Equipe = empl.Equipe
//            }).FirstOrDefault();

//Employe employeFound = query;
//Employe employeFound = GetEmploye(nomUser, psswrdUser);


/*
 var employe = (from empl in dbEntities.Employes
                where empl.Nom == nomEmp && empl.EmployeMotPasse == psswrdEmp
                select new
                {
                    EmployeID = empl.EmployeID,
                    Nom = empl.Nom,
                    Prenom = empl.Prenom,
                    DateEmbauche = empl.DateEmbauche,
                    Telephone = empl.Telephone,
                    EquipeID = empl.EquipeID,
                    PosteEmploi = empl.PosteEmploi,
                    EmployeMotPasse = empl.EmployeMotPasse,
                    ValiderSanteSecuritaire = empl.ValiderSanteSecuritaire,
                    Equipe = empl.Equipe

                }).ToList().Select(x => new Employe()
                {
                    EmployeID = x.EmployeID,
                    Nom = x.Nom,
                    Prenom = x.Prenom,
                    DateEmbauche = x.DateEmbauche,
                    Telephone = x.Telephone,
                    EquipeID = x.EquipeID,
                    PosteEmploi = x.PosteEmploi,
                    EmployeMotPasse = x.EmployeMotPasse,
                    ValiderSanteSecuritaire = x.ValiderSanteSecuritaire,
                    Equipe = x.Equipe
                });

 return View(employe.ToList());
}

*/


//Employe employeFound = dbEntities.Employes.FirstOrDefault(emp => emp.Nom == employeFinder.Nom && emp.EmployeMotPasse == employeFinder.EmployeMotPasse);
//Employe employeFound = dbEntities.Employes.FirstOrDefault(emp => emp.Nom.Equals(nomUser) && emp.EmployeMotPasse.Equals(psswrdUser));
//Employe employeFound = dbEntities.Employes.FirstOrDefault(emp => emp.Nom.Equals(nomUser) && emp.EmployeMotPasse.Equals(psswrdUser));
//Employe employeFound = dbEntities.Employes.SingleOrDefault(emp => emp.Nom.Equals(nomUser) && emp.EmployeMotPasse.Equals(psswrdUser));

//int result = (from p in dbEntities.Employes
//              where p.Nom == nomUser && p.EmployeMotPasse == psswrdUser
//              orderby p.EmployeID descending
//              select p.EmployeID).FirstOrDefault();

