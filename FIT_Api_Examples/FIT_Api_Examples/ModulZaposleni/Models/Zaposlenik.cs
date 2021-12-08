using FIT_Api_Examples.ModulGeneralUser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulZaposleni.Models
{
    public class Zaposlenik : KorisnickiNalog
    {
        public int ID { get; set; }
        public string Slika { get; set; }
        public int ObavljeneNarudzbe { get; set; }
    }
}
