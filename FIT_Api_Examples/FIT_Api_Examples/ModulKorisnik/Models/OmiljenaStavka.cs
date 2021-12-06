using FIT_Api_Examples.ModulMeni.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Models
{
    public class OmiljenaStavka
    {
        public int ID { get; set; }
        [ForeignKey("KorisnikID")]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey("MeniStavkaID")]
        public int MeniStavkaID { get; set; }
        public MeniStavka MeniStavka { get; set; }
    }
}
