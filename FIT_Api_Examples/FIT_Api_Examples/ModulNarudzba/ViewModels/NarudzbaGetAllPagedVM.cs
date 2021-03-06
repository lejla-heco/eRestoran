using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulNarudzba.ViewModels
{
    public class NarudzbaGetAllPagedVM
    {
        public int id { get; set; }
        public float cijena { get; set; }
        public string datumNarucivanja { get; set; }
        public string status { get; set; }
        public bool isKoristenKupon { get; set; }

        public class Stavka
        {
            public string naziv { get; set; }
            public int kolicina { get; set; }
        }

        public List<Stavka> stavke { get; set; }
    }
}
