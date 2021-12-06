using FIT_Api_Examples.ModulKorisnik.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulMeni.Models
{
    public class MeniStavka
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public string Slika { get; set; }
        public bool Izdvojeno { get; set; }
        public float SnizenaCijena { get; set; }
        public float Ocjena { get; set; }
        [ForeignKey("MeniGrupaID")]
        public int MeniGrupaID { get; set; }
        public MeniGrupa MeniGrupa { get; set; }
    }
}
