using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulZaposleni.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnickiNalog.Models
{
    public class KorisnickiNalog
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public Korisnik Korisnik  => this as Korisnik;
        [JsonIgnore]
        public Administrator Administrator => this as Administrator;
        [JsonIgnore]
        public Zaposlenik Zaposlenik => this as Zaposlenik;
        [JsonIgnore]
        public Dostavljac Dostavljac => this as Dostavljac;
        public bool isKorisnik  => Korisnik != null;
        public bool isAdministrator => Administrator != null;
        public bool isZaposlenik => Zaposlenik != null;
        public bool isDostavljac => Dostavljac != null;
    }
}
