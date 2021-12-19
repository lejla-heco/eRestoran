using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnickiNalog.ViewModels
{
    public class PoslovnicaAddVM
    {
        public string adresa { get; set; }
        public string brojTelefona { get; set; }
        public string radnoVrijemeRedovno { get; set; }
        public string radnoVrijemeVikend { get; set; }
        public int opstinaId { get; set; }
    }
}
