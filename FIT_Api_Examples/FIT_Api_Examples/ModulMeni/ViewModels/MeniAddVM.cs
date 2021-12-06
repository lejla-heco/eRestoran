using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulMeni.ViewModels
{
    public class MeniAddVM
    {
        public string naziv { get; set; }
        public string opis { get; set; }
        public float cijena { get; set; }
        public bool izdvojeno { get; set; }
        public int meniGrupaId { get; set; }
        public IFormFile slikaMeniStavke { set; get; }
    }
}
