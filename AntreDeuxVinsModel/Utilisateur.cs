using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public Role Role { get; set; }
    }
}
