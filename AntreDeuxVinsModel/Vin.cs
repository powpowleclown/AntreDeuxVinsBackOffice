using System;
using System.Collections.Generic;

namespace AntreDeuxVinsModel
{
    public class Vin
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public String Type { get; set; }
        public DateTime Millesime { get; set; }
        public int Volume { get; set; }
        public String Image { get; set; }
        public Couleur Couleur { get; set; }
        public Pays Pays { get; set; }
        public Region Region { get; set; }
        public ICollection<Cave> Caves { get; set; }
        public ICollection<Aliment> Aliments { get; set; }
    }
}
