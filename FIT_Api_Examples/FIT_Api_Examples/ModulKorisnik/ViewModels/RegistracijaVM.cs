using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.ViewModels
{
    public class RegistracijaVM
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string adresaStanovanja { get; set; }
        public string brojTelefona { get; set; }
        public int opstinaId { get; set; }

    }
}
