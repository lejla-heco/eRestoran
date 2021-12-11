using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulMeni.Models;
using FIT_Api_Examples.ModulNarudzba.Models;
using FIT_Api_Examples.ModulNarudzba.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult AddStavka([FromBody] StavkaAddVM stavkaAddVM)
        {
            Korisnik korisnik = _dbContext.Korisnik.Find(stavkaAddVM.korisnikId);
            if (korisnik == null)
                return BadRequest("Nepostojeci korisnik");

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == stavkaAddVM.korisnikId && n.Zakljucena == false).SingleOrDefault();
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

        [HttpGet("{id}")]
        public GetNarudzbaVM GetNarudzba(int id)
        {
            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == id && n.Zakljucena == false).FirstOrDefault();
            if (narudzba == null) return null;

            GetNarudzbaVM getNarudzbaVM = new GetNarudzbaVM()
            {
                id = narudzba.ID,
                cijena = narudzba.Cijena,
                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == narudzba.ID).Select(sn => new GetNarudzbaVM.Stavka()
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

            return getNarudzbaVM;
        }

        [HttpGet("{id}")]
        public int GetBrojStavki(int id)
        {
            Korisnik korisnik = _dbContext.Korisnik.Find(id);
            if (korisnik == null) return 0;

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == id && n.Zakljucena == false).FirstOrDefault();
            if (narudzba == null) return 0;

            return narudzba.BrojStavki;
        }

        [HttpGet("{id}")]
        public IActionResult UkloniStavku(int id)
        {
            StavkaNarudzbe stavkaNarudzbe = _dbContext.StavkaNarudzbe.Where(sn => sn.ID == id).FirstOrDefault();

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.ID == stavkaNarudzbe.NarudzbaID).SingleOrDefault();
            if (narudzba == null)
                return BadRequest("Nepostojeca narudzba");

            _dbContext.StavkaNarudzbe.Remove(stavkaNarudzbe);
            narudzba.Cijena -= stavkaNarudzbe.Iznos * stavkaNarudzbe.Kolicina;
            narudzba.BrojStavki -= stavkaNarudzbe.Kolicina;
            _dbContext.SaveChanges();

            GetNarudzbaVM getNarudzbaVM = new GetNarudzbaVM()
            {
                id = narudzba.ID,
                cijena = narudzba.Cijena,
                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == narudzba.ID).Select(sn => new GetNarudzbaVM.Stavka()
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
    }
}
