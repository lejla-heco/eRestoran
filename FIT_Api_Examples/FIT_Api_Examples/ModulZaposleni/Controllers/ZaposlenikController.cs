using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulAutentifikacija.Models;
using FIT_Api_Examples.ModulNarudzba.Models;
using FIT_Api_Examples.ModulZaposleni.Models;
using FIT_Api_Examples.ModulZaposleni.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulZaposleni.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ZaposlenikController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ZaposlenikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Add([FromBody] ZaposlenikAddVM zaposlenikAddVM)
        {
            Zaposlenik noviZaposlenik = new Zaposlenik()
            {
                Ime=zaposlenikAddVM.ime,
                Prezime=zaposlenikAddVM.prezime,
                Email=zaposlenikAddVM.email,
                KorisnickoIme=zaposlenikAddVM.username,
                Lozinka=zaposlenikAddVM.password
                
            };
            noviZaposlenik.AktivneNarudzbe = 0;
            noviZaposlenik.ObavljeneNarudzbe = 0;
            _dbContext.Zaposlenik.Add(noviZaposlenik);
            _dbContext.SaveChanges();
            return Ok(noviZaposlenik.ID);
        }
        [HttpPost("{id}")]
        public ActionResult AddSlika(int id, [FromForm] ZaposlenikAddSlikaVM zaposlenikAddSlikaVM)
        {
            try
            {
                Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);

                if (zaposlenikAddSlikaVM.slikaZaposlenika != null && zaposlenik != null)
                {
                    if (zaposlenikAddSlikaVM.slikaZaposlenika.Length > 250 * 1000)
                        return BadRequest("max velicina fajla je 250 KB");

                    string ekstenzija = Path.GetExtension(zaposlenikAddSlikaVM.slikaZaposlenika.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    zaposlenikAddSlikaVM.slikaZaposlenika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                    zaposlenik.Slika = Config.SlikeURL + filename;
                    _dbContext.SaveChanges();
                }

                return Ok(zaposlenik);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
        [HttpGet]
        public List<ZaposlenikGetAllPagedVM> GetAll()
        {
            List<ZaposlenikGetAllPagedVM> pagedStavke = _dbContext.Zaposlenik
                                          .Select(z => new ZaposlenikGetAllPagedVM()
                                            {
                                                id = z.ID,
                                                ime = z.Ime,
                                                prezime = z.Prezime,
                                                email = z.Email,
                                                slika = z.Slika,
                                                username = z.KorisnickoIme,
                                                password = z.Lozinka,
                                                obavljeneNarudzbe = z.ObavljeneNarudzbe,
                                                
                                            }).ToList();
            return pagedStavke;
        }
        [HttpGet("{id}")]
        public ActionResult Delete(int id)
        { 

            Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);

                if (zaposlenik == null)
                    return BadRequest("pogresan ID");

                if (zaposlenik.AktivneNarudzbe > 0)
                    return BadRequest("Zaposlenik trenutno obavlja narudžbe");

                List<Narudzba> narudzbeZaposlenika = _dbContext.Narudzba.Where(n => n.ZaposlenikID == zaposlenik.ID).ToList();
            foreach (Narudzba narudzba in narudzbeZaposlenika)
            {
                narudzba.ZaposlenikID = null;
                narudzba.Zaposlenik = null;
            }
                _dbContext.SaveChanges();

            List<AutentifikacijaToken> logovi = _dbContext.AutentifikacijaToken.Where(at => at.KorisnickiNalogId == id).ToList();

            _dbContext.AutentifikacijaToken.RemoveRange(logovi);
            _dbContext.Zaposlenik.Remove(zaposlenik);
            _dbContext.KorisnickiNalog.Remove(zaposlenik);

            _dbContext.SaveChanges();

            return Ok(zaposlenik);
        }

        [HttpGet("{id}")]
        public ZaposlenikUpdateVM GetById(int id)
        {
            Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);
            if (zaposlenik != null)
            {
                ZaposlenikUpdateVM odabraniZaposlenik = new ZaposlenikUpdateVM()
                {
                    id = zaposlenik.ID,
                    ime = zaposlenik.Ime,
                    prezime = zaposlenik.Prezime,
                    email = zaposlenik.Email,
                    korisnickoIme = zaposlenik.KorisnickoIme,
                    lozinka = zaposlenik.Lozinka,
                    slika = zaposlenik.Slika
                };
                return odabraniZaposlenik;
            }
            return null;
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] ZaposlenikUpdateVM zaposlenikUpdateVM)
        {

            Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);

            if (zaposlenik == null)
                return BadRequest("pogresan ID");

            zaposlenik.Ime = zaposlenikUpdateVM.ime.RemoveTags();
            zaposlenik.Prezime = zaposlenikUpdateVM.prezime.RemoveTags();
            zaposlenik.Email = zaposlenikUpdateVM.email.RemoveTags();
            zaposlenik.KorisnickoIme = zaposlenikUpdateVM.korisnickoIme.RemoveTags();
            zaposlenik.Lozinka = zaposlenikUpdateVM.lozinka.RemoveTags();
           
            _dbContext.SaveChanges();
            return Ok(zaposlenik.ID);
        }

        [HttpPost]
        public ActionResult UpdatePostavkaProfila([FromBody] ZaposlenikUpdateVM zaposlenikUpdateVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaZaposlenik)
                return BadRequest("nije logiran");

            Zaposlenik zaposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Zaposlenik;


            zaposlenik.Ime = zaposlenikUpdateVM.ime;
            zaposlenik.Prezime = zaposlenikUpdateVM.prezime;
            zaposlenik.Email = zaposlenikUpdateVM.email;
            zaposlenik.KorisnickoIme = zaposlenikUpdateVM.korisnickoIme;
            zaposlenik.Lozinka = zaposlenikUpdateVM.lozinka;
           

            _dbContext.SaveChanges();
            return Ok(zaposlenik);
        }
        [HttpPost]
        public ActionResult UpdateSlika( [FromForm] ZaposlenikAddSlikaVM zaposlenikAddSlikaVM)
        {
           // try
           // {
                if (!HttpContext.GetLoginInfo().isPermisijaZaposlenik)
                    return BadRequest("nije logiran");

                Zaposlenik zaposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Zaposlenik;

                if (zaposlenikAddSlikaVM.slikaZaposlenika != null && zaposlenik != null)
                {
                    if (zaposlenikAddSlikaVM.slikaZaposlenika.Length > 250 * 1000)
                        return BadRequest("max velicina fajla je 250 KB");

                    string ekstenzija = Path.GetExtension(zaposlenikAddSlikaVM.slikaZaposlenika.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    zaposlenikAddSlikaVM.slikaZaposlenika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                    zaposlenik.Slika = Config.SlikeURL + filename;
                    _dbContext.SaveChanges();
                }

                return Ok(zaposlenik);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message + ex.InnerException);
            //}
        }
    }
}
