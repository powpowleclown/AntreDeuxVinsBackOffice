using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class VinAliment
    {
        public int VinId { get; set; }
        public Vin Vin { get; set; }
        public int AlimentId { get; set; }
        public Aliment Aliment { get; set; }
    }
}
