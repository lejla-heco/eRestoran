using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulMeni.Models;
using FIT_Api_Examples.ModulNarudzba.Models;
using FIT_Api_Examples.ModulNarudzba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulNarudzba.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NarudzbaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public NarudzbaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddStavka([FromBody] NarudzbaAddStavkaVM stavkaAddVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nepostojeci korisnik");

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == korisnik.ID && n.Zakljucena == false).SingleOrDefault();
            if (narudzba == null)
            {
                narudzba = new Narudzba()
                {
                    DatumNarucivanja = DateTime.Now,
                    Zakljucena = false,
                    Korisnik = korisnik,
                    BrojStavki = 0,
                    Cijena = 0,
                };
                _dbContext.Narudzba.Add(narudzba);
                _dbContext.SaveChanges();
            }

            MeniStavka meniStavka = _dbContext.MeniStavka.Find(stavkaAddVM.meniStavkaId);

            StavkaNarudzbe stavkaNarudzbe = new StavkaNarudzbe()
            {
                Kolicina = 1,
                MeniStavkaID = stavkaAddVM.meniStavkaId,
                NarudzbaID = narudzba.ID,
                Iznos = meniStavka.Izdvojeno ? meniStavka.SnizenaCijena : meniStavka.Cijena
            };

            _dbContext.StavkaNarudzbe.Add(stavkaNarudzbe);
            narudzba.BrojStavki++;
            narudzba.Cijena += stavkaNarudzbe.Iznos;
            _dbContext.SaveChanges();

            return Ok(narudzba.BrojStavki);
        }

        [HttpGet]
        public IActionResult GetNarudzba()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            int id = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik.ID;

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == id && n.Zakljucena == false).FirstOrDefault();
            if (narudzba == null) return null;

            NarudzbaGetNarudzbaVM getNarudzbaVM = new NarudzbaGetNarudzbaVM()
            {
                id = narudzba.ID,
                cijena = narudzba.Cijena,
                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == narudzba.ID).Select(sn => new NarudzbaGetNarudzbaVM.Stavka()
                {
                    id = sn.ID,
                    naziv = sn.MeniStavka.Naziv,
                    opis = sn.MeniStavka.Opis,
                    cijena = sn.MeniStavka.Cijena,
                    slika = sn.MeniStavka.Slika,
                    izdvojeno = sn.MeniStavka.Izdvojeno,
                    snizenaCijena = sn.MeniStavka.SnizenaCijena,
                    ocjena = sn.MeniStavka.Ocjena,
                    kolicina = sn.Kolicina
                }).ToList(),
            };

            return Ok(getNarudzbaVM);
        }

        [HttpGet]
        public IActionResult GetBrojStavki()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            int id = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik.ID;

            Korisnik korisnik = _dbContext.Korisnik.Find(id);
            if (korisnik == null) return Ok(0);

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == id && n.Zakljucena == false).FirstOrDefault();
            if (narudzba == null) return Ok(0);

            return Ok(narudzba.BrojStavki);
        }

        [HttpGet("{id}")]
        public IActionResult UkloniStavku(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            StavkaNarudzbe stavkaNarudzbe = _dbContext.StavkaNarudzbe.Where(sn => sn.ID == id).FirstOrDefault();

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.ID == stavkaNarudzbe.NarudzbaID).SingleOrDefault();
            if (narudzba == null)
                return BadRequest("Nepostojeca narudzba");

            _dbContext.StavkaNarudzbe.Remove(stavkaNarudzbe);
            narudzba.Cijena -= stavkaNarudzbe.Iznos;
            narudzba.BrojStavki -= stavkaNarudzbe.Kolicina;
            _dbContext.SaveChanges();

            NarudzbaGetNarudzbaVM getNarudzbaVM = new NarudzbaGetNarudzbaVM()
            {
                id = narudzba.ID,
                cijena = narudzba.Cijena,
                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == narudzba.ID).Select(sn => new NarudzbaGetNarudzbaVM.Stavka()
                {
                    id = sn.ID,
                    naziv = sn.MeniStavka.Naziv,
                    opis = sn.MeniStavka.Opis,
                    cijena = sn.MeniStavka.Cijena,
                    slika = sn.MeniStavka.Slika,
                    izdvojeno = sn.MeniStavka.Izdvojeno,
                    snizenaCijena = sn.MeniStavka.SnizenaCijena,
                    ocjena = sn.MeniStavka.Ocjena,
                    kolicina = sn.Kolicina
                }).ToList(),
            };

            return Ok(getNarudzbaVM);
        }
    
        [HttpPost]
        public IActionResult UpdateKolicina(NarudzbaUpdateKolicinaVM narudzbaUpdateKolicinaVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            StavkaNarudzbe stavkaNarudzbe = _dbContext.StavkaNarudzbe.Include(sn => sn.MeniStavka)
                                            .Where(sn => sn.ID == narudzbaUpdateKolicinaVM.id).SingleOrDefault();
            if (stavkaNarudzbe == null)
                return BadRequest("Nepostojeca stavka narudzbe");

            Narudzba narudzba = _dbContext.Narudzba.Find(stavkaNarudzbe.NarudzbaID);
            narudzba.Cijena -= stavkaNarudzbe.Iznos;
            narudzba.BrojStavki -= stavkaNarudzbe.Kolicina;

            stavkaNarudzbe.Kolicina = narudzbaUpdateKolicinaVM.kolicina;

            if (!stavkaNarudzbe.MeniStavka.Izdvojeno)
                stavkaNarudzbe.Iznos = stavkaNarudzbe.MeniStavka.Cijena * narudzbaUpdateKolicinaVM.kolicina;
            else
                stavkaNarudzbe.Iznos = stavkaNarudzbe.MeniStavka.SnizenaCijena * narudzbaUpdateKolicinaVM.kolicina;

            narudzba.Cijena += stavkaNarudzbe.Iznos;
            narudzba.BrojStavki += stavkaNarudzbe.Kolicina;

            _dbContext.SaveChanges();

            var response = new { 
                cijena = narudzba.Cijena,
                kolicina = narudzba.BrojStavki
            };

            return Ok(response);
        }
    }
}
