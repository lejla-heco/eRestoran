using FIT_Api_Examples.ModulGeneralUser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Models
{
    public class Korisnik
    {
        public int ID { get; set; }
        public string AdresaStanovanja { get; set; }
        public string BrojTelefona { get; set; }
        [ForeignKey("GeneralUserID")]
        public int GeneralUserID { get; set; }
        public GeneralUser GeneralUser { get; set; }
        [ForeignKey("OpstinaID")]
        public int OpstinaID { get; set; }
        public Opstina Opstina { get; set; }
    }
}
