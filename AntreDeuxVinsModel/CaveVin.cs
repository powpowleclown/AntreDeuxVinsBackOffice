using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class CaveVin
    {
        public int CaveId { get; set; }
        public Cave Cave { get; set; }
        public int VinId { get; set; }
        public Vin Vin { get; set; }
        public int Quantite { get; set; }
        public CaveVin()
        {

        }

    }
}
