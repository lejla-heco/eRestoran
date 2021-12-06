using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Models
{
    public class Kupon
    {
        public int ID { get; set; }
        public string Kod { get; set; }
        public int Popust { get; set; }
        public int MaksimalniBrojKorisnika { get; set; }
    }
}
