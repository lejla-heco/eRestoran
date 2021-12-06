using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Helper
{
    public class Config
    {
        public static string AplikacijaURL = "https://erestoran-api.p2102.app.fit.ba/";
        public static string Slike => "uploads/";
        public static string SlikeURL => AplikacijaURL + Slike;
        public static string SlikeFolder => "wwwroot/" + Slike;
    }
}
