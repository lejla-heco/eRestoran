using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnickiNalog.Models
{
    [Table("Administrator")]
    public class Administrator : KorisnickiNalog
    {
        public DateTime DatumKreiranja { get; set; }
    }
}
