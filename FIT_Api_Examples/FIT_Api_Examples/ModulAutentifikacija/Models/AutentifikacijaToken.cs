using FIT_Api_Examples.ModulGeneralUser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulAutentifikacija.Models
{
    public class AutentifikacijaToken
    {
        [Key]
        public int id { get; set; }
        public string vrijednost { get; set; }
        public int korisnikId { get; set; }
        public string uloga { get; set; }
        public DateTime vrijemeEvidentiranja { get; set; }
        public string ipAdresa { get; set; }
    }
}
