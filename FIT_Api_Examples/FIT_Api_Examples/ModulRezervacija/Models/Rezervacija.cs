using FIT_Api_Examples.ModulKorisnik.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulRezervacija.Models
{
    public class Rezervacija
    {
        public int ID { get; set; }
        public DateTime DatumRezerviranja { get; set; }
        public int BrojOsoba { get; set; }
        public int BrojStolova { get; set; }
        public bool Obavljena { get; set; }
        public string Poruka { get; set; }
        [ForeignKey("PrigodaID")]
        public int PrigodaID { get; set; }
        public Prigoda Prigoda { get; set; }
        [ForeignKey("KorisnikID")]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [ForeignKey("StatusRezervacijeID")]
        public int StatusRezervacijeID { get; set; }
        public StatusRezervacije StatusRezervacije { get; set; }
    }
}
