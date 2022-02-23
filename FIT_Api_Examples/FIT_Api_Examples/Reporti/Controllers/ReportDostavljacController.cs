using AspNetCore.Reporting;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsFormsApp2;

namespace FIT_Api_Examples.Reporti.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportDostavljacController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ReportDostavljacController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<ReportDostavljaciRow> getDostavljaci()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return null;

            List<ReportDostavljaciRow> dostavljaci = _dbContext.Dostavljac.Select(
                z => new ReportDostavljaciRow()
                {
                    ImePrezime = $"{z.Ime} {z.Prezime}",
                    KorisnickoIme = z.KorisnickoIme,
                    Email = z.Email,
                    DostavljeneNarudzbe = z.DostavljeneNarudzbe,
                    OstvarenaZarada = _dbContext.Narudzba.Where(n => n.StatusNarudzbeID == 6 && n.DostavljacID == z.ID).Sum(n => n.Cijena),
                }
                ).ToList();

            return dostavljaci;
        }

        [HttpGet]
        public ActionResult PreuzmiIzvjestaj()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("Nemate ovlasti za generisanje izvještaja!");
            try
            {
                LocalReport localReport = new("Reporti/ReportDostavljaci.rdlc");
                List<ReportDostavljaciRow> podaci = getDostavljaci();
                localReport.AddDataSource("DataSetDostavljaci", podaci);


                KorisnickiNalog korisnik = HttpContext.GetLoginInfo().autentifikacijaToken.korisnickiNalog;

                var poslovnice = _dbContext.Poslovnica.ToList();
                string nazivi = "";
                string brojeviTelefona = "";
                poslovnice.ForEach(p =>
                {
                    nazivi += p.Adresa + " | ";
                    brojeviTelefona += p.BrojTelefona + " | ";
                });

                Dictionary<string, string> parametri = new Dictionary<string, string>();
                parametri.Add("AutorIzvjestaja", $"{korisnik.Ime} {korisnik.Prezime}");
                parametri.Add("UkupnaZarada", podaci.Sum(p => p.OstvarenaZarada).ToString());
                parametri.Add("Adresa", $"Adrese: {nazivi}");
                parametri.Add("BrojTelefona", $"{brojeviTelefona}");

                ReportResult result = localReport.Execute(RenderType.Pdf, parameters: parametri);

                return File(result.MainStream, "application/pdf");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " " + e.InnerException);
            };
        }
    }
}
