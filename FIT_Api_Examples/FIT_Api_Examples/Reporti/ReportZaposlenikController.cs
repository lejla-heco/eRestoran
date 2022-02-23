using AspNetCore.Reporting;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsReportsTemp;

namespace FIT_Api_Examples.Reporti
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportZaposlenikController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ReportZaposlenikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<ReportZaposleniciRow> getZaposlenici()
        {

            List<ReportZaposleniciRow> zaposlenici = _dbContext.Zaposlenik.Select(
                z => new ReportZaposleniciRow()
                {
                    ImePrezime = $"{z.Ime} {z.Prezime}",
                    KorisnickoIme = z.KorisnickoIme,
                    Email = z.Email,
                    ObavljeneNarudzbe = z.ObavljeneNarudzbe,
                    OstvarenaZarada = _dbContext.Narudzba.Where(n => n.StatusNarudzbeID == 6 && n.ZaposlenikID == z.ID).Sum(n => n.Cijena),
                }
                ).ToList();

            return zaposlenici;
        }

        [HttpGet]
        public ActionResult PreuzmiIzvjestaj()
        {
            try
            {
                LocalReport localReport = new("Reporti/ReportZaposlenici.rdlc");
                List<ReportZaposleniciRow> podaci = getZaposlenici();
                localReport.AddDataSource("DataSetZaposlenici", podaci);


                //KorisnickiNalog korisnik = HttpContext.GetLoginInfo().autentifikacijaToken.korisnickiNalog;

                var poslovnice = _dbContext.Poslovnica.ToList();
                string nazivi = "";
                string brojeviTelefona = "";
                poslovnice.ForEach(p =>
                {
                    nazivi += p.Adresa + " | ";
                    brojeviTelefona += p.BrojTelefona + " | ";
                });

                Dictionary<string, string> parametri = new Dictionary<string, string>();
                parametri.Add("AutorIzvjestaja", $"Lejla Hećo");
                parametri.Add("UkupnaZarada", podaci.Sum(p => p.OstvarenaZarada).ToString());
                parametri.Add("Adresa", $"Adrese: {nazivi}");
                parametri.Add("BrojTelefona", $"{brojeviTelefona}");

                ReportResult result = localReport.Execute(RenderType.Pdf, parameters: parametri);

                return File(result.MainStream, "application/pdf");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message + " " + e.InnerException);
            };
        }
    }
}
