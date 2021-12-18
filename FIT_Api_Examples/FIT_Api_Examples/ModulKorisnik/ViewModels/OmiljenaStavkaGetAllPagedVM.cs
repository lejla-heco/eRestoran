using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.ViewModels
{
    public class OmiljenaStavkaGetAllPagedVM
    {
        public string kategorija { get; set; }
        public int itemsPerPage { get; set; } = 8;
        public int pageNumber { get; set; } = 1;
    }
}
