using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulRezervacija.ViewModels
{
    public class RezervacijaGetAllVM
    {
        public DateTime datumRezerviranja { get; set; }
        public int brojOsoba { get; set; }
        public int brojStolova { get; set; }
        public string poruka { get; set; }
        public int prigodaID { get; set; }
        public string nazivPrigode { get; set; }

    }
}
