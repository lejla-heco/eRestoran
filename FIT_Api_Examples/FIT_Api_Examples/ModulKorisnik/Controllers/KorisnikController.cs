using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulKorisnik.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController : Controller
    {
        private ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Add([FromBody] RegistracijaVM registracijaVM)
        {
            Korisnik noviKorisnik = new Korisnik()
            {
                Ime = registracijaVM.ime,
                Prezime = registracijaVM.prezime,
                Email = registracijaVM.email,
                AdresaStanovanja = registracijaVM.adresaStanovanja,
                BrojTelefona = registracijaVM.brojTelefona,
                KorisnickoIme = registracijaVM.username,
                Lozinka = registracijaVM.password,
                OpstinaID = registracijaVM.opstinaId
            };
            _dbContext.Korisnik.Add(noviKorisnik);
            _dbContext.SaveChanges();
            return Ok(noviKorisnik.ID);
        }
        [HttpGet]
        public List<Korisnik> GetAll()
        {
            return _dbContext.Korisnik.ToList();
        }
    }
}
