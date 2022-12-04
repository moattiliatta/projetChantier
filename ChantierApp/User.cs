using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChantierApp
{
    internal class User
    {

        string? nom;
        string? password;

        public User(string? nom, string? password)
        {
            this.nom = nom;
            this.password = password;
        }
    }
}
