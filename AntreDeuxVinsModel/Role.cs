using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Role
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public ICollection<Utilisateur> Utilisateurs { get; set; }
        public Role()
        {

        }

    }
}
