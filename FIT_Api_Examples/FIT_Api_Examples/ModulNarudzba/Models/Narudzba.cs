using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulZaposleni.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulNarudzba.Models
{
    public class Narudzba
    {
        public int ID { get; set; }
        public bool Omiljeno { get; set; }
        public float Cijena { get; set; }
        public DateTime DatumNarucivanja { get; set; }
        public bool Zakljucena { get; set; }
        public int BrojStavki { get; set; }
        [ForeignKey("KorisnikID")]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [ForeignKey("StatusNarudzbeID")]
        public int StatusNarudzbeID { get; set; }
        public StatusNarudzbe StatusNarudzbe { get; set; }
        [ForeignKey("ZaposlenikID")]
        public int ZaposlenikID { get; set; }
        public Zaposlenik Zaposlenik { get; set; }
        [ForeignKey("DostavljacID")]
        public int DostavljacID { get; set; }
        public Dostavljac Dostavljac { get; set; }
    }
}
