using FIT_Api_Examples.ModulMeni.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulNarudzba.Models
{
    public class StavkaNarudzbe
    {
        public int ID { get; set; }
        public int Kolicina { get; set; }
        public float Iznos { get; set; }
        [ForeignKey("MeniStavkaID")]
        public int MeniStavkaID { get; set; }
        public MeniStavka MeniStavka { get; set; }
        [ForeignKey("NarudzbaID")]
        public int NarudzbaID { get; set; }
        public Narudzba Narudzba { get; set; }
    }
}
