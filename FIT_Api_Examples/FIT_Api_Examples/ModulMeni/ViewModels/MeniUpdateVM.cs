using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulMeni.ViewModels
{
    public class MeniUpdateVM
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public float cijena { get; set; }
        public float snizenaCijena { get; set; }
        public int meniGrupaId { get; set; }
        public string slika { get; set; }
    }
}
