using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulZaposleni.Models
{
    [Table("Zaposlenik")]
    public class Zaposlenik : KorisnickiNalog
    {
        public string Slika { get; set; }
        public int ObavljeneNarudzbe { get; set; }
    }
}
