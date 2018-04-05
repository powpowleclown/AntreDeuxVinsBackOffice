using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Aliment
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public ICollection<VinAliment> AlimentVins { get; set; }

        public Aliment()
        {

        }
    }
}
