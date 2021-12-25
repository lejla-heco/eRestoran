using FIT_Api_Examples.ModulNarudzba.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Models
{
    public class KorisnikKupon
    {
        public int ID { get; set; }
        public bool Iskoristen { get; set; }
        [ForeignKey("KorisnikID")]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [ForeignKey("KuponID")]
        public int KuponID { get; set; }
        public Kupon Kupon { get; set; }
        [ForeignKey("NarudzbaID")]
        public int? NarudzbaID { get; set; }
        public Narudzba Narudzba { get; set; }
    }
}
