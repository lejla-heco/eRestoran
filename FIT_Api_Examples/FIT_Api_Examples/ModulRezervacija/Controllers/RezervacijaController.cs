using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulRezervacija.Models;
using FIT_Api_Examples.ModulRezervacija.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulRezervacija.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervacijaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public RezervacijaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add([FromBody] RezervacijaAddVM rezervacijaAddVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");
            int statusid = _dbContext.StatusRezervacije.Where(s => s.Naziv == "Default").SingleOrDefault().ID;
            
            Rezervacija novaRezervacija = new Rezervacija()
            {

                KorisnikID = korisnik.ID,
                DatumRezerviranja = rezervacijaAddVM.datumRezerviranja,
                PrigodaID = rezervacijaAddVM.prigodaID,
                StatusRezervacijeID = statusid,
                BrojOsoba = rezervacijaAddVM.brojOsoba,
                BrojStolova = rezervacijaAddVM.brojStolova,
                Obavljena = false,
                Poruka = rezervacijaAddVM.poruka,
               
            };

            _dbContext.Rezervacija.Add(novaRezervacija);
            _dbContext.SaveChanges();
            return Ok(novaRezervacija);
        }

        [HttpGet]
        public List<Rezervacija> GetAll()
        {
            return _dbContext.Rezervacija.ToList();
        }
      
    }
}
