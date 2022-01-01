using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulKorisnik.ViewModels;
using FIT_Api_Examples.ModulZaposleni.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnickiNalog.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnickiNalogController : Controller
    {
        private ApplicationDbContext _dbContext;

        public KorisnickiNalogController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            KorisnikGetVM korisnikGetVM = new KorisnikGetVM();
            KorisnickiNalog defaultniNalog = new KorisnickiNalog();

            if (HttpContext.GetLoginInfo().isPermisijaKorisnik)
            {
                Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;
                if (korisnik == null)
                    return BadRequest("Nepostojeci korisnik");

                defaultniNalog = korisnik;
                korisnikGetVM.brojTelefona = korisnik.BrojTelefona;
                korisnikGetVM.adresaStanovanja = korisnik.AdresaStanovanja;
                korisnikGetVM.opstinaId = korisnik.OpstinaID;
            }
            else if (HttpContext.GetLoginInfo().isPermisijaAdministrator)
            {
                Administrator admin = HttpContext.GetLoginInfo().korisnickiNalog.Administrator;
                if (admin == null)
                    return BadRequest("Nepostojeci administrator");
                defaultniNalog = admin;
            }
            else if (HttpContext.GetLoginInfo().isPermisijaZaposlenik)
            {
                Zaposlenik zaposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Zaposlenik;
                if (zaposlenik == null)
                    return BadRequest("Nepostojeci zaposlenik");
                defaultniNalog = zaposlenik;
                korisnikGetVM.slika = zaposlenik.Slika;
            }
            else if (HttpContext.GetLoginInfo().isPermisijaDostavljac)
            {
                Dostavljac dostavljac = HttpContext.GetLoginInfo().korisnickiNalog.Dostavljac;
                if (dostavljac == null)
                    return BadRequest("Nepostojeci dostavljac");
                defaultniNalog = dostavljac;
                korisnikGetVM.slika = dostavljac.Slika;
            }

            korisnikGetVM.ime = defaultniNalog.Ime;
            korisnikGetVM.prezime = defaultniNalog.Prezime;
            korisnikGetVM.email = defaultniNalog.Email;
            korisnikGetVM.korisnickoIme = defaultniNalog.KorisnickoIme;
            korisnikGetVM.lozinka = defaultniNalog.Lozinka;

            return Ok(korisnikGetVM);
        }
    }
}
