using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsReportsTemp
{
    public class ReportZaposleniciRow
    {
        public string ImePrezime { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public int ObavljeneNarudzbe { get; set; }
        public float OstvarenaZarada { get; set; }

        public static List<ReportZaposleniciRow> Get()
        {
            return new List<ReportZaposleniciRow> { };
        }
    }
}
