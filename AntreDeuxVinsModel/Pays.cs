using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Pays
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public ICollection<Region> Regions { get; set; }
        public Pays()
        {

        }
    }
}
