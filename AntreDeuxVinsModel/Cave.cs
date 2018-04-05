using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Cave
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public ICollection<CaveVin> CaveVins { get; set; }

        public Cave()
        {

        }
    }
}
