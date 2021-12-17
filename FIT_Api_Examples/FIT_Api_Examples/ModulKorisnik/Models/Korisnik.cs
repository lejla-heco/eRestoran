using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Models
{
    [Table("Korisnik")]
    public class Korisnik : KorisnickiNalog
    {
        public string AdresaStanovanja { get; set; }
        public string BrojTelefona { get; set; }
        [ForeignKey("OpstinaID")]
        public int OpstinaID { get; set; }
        public Opstina Opstina { get; set; }
    }
}
