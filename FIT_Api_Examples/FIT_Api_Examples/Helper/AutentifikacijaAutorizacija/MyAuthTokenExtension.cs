using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulAutentifikacija.Models;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FIT_Api_Examples.Helper.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }
            [JsonIgnore]
            public KorisnickiNalog korisnickiNalog => autentifikacijaToken?.korisnickiNalog;
            public AutentifikacijaToken autentifikacijaToken { get; set; }
            public bool isLogiran => korisnickiNalog != null;
            public bool isPermisijaKorisnik => isLogiran && (korisnickiNalog.isKorisnik);
            public bool isPermisijaAdministrator => isLogiran && (korisnickiNalog.isAdministrator);
            public bool isPermisijaZaposlenik => isLogiran && (korisnickiNalog.isZaposlenik);
            public bool isPermisijaDostavljac => isLogiran && (korisnickiNalog.isDostavljac);
            public bool isPermisijaGost => !isLogiran;
        }

        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            AutentifikacijaToken token = httpContext.GetAuthToken();
            return new LoginInformacije(token);
        }

        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            AutentifikacijaToken korisnickiNalog = db.AutentifikacijaToken
                .Include(s => s.korisnickiNalog)
                .SingleOrDefault(x => token != null && x.vrijednost == token);

            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
