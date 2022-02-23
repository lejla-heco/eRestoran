using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class ReportDostavljaciRow
    {
        public string ImePrezime { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public int DostavljeneNarudzbe { get; set; }
        public float OstvarenaZarada { get; set; }

        public static List<ReportDostavljaciRow> Get()
        {
            return new List<ReportDostavljaciRow> { };
        }
    }
}
