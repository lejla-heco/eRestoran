using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulZaposleni.ViewModels
{
    public class ZaposlenikGetAllPagedVM
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string slika { get; set; }
        public int obavljeneNarudzbe { get; set; }
    }
}
