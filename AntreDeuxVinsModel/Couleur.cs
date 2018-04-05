using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Couleur
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public ICollection<Vin> Vins { get; set; }
        public Couleur()
        {

        }
    }
}
