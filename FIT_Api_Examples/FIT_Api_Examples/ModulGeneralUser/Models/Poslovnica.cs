using FIT_Api_Examples.ModulKorisnik.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulGeneralUser.Models
{
    public class Poslovnica
    {
        public int ID { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        [ForeignKey("OpstinaID")]
        public int OpstinaID { get; set; }
        public Opstina Opstina { get; set; }
    }
}
