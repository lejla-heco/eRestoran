using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.ModulAutentifikacija.Models;
using FIT_Api_Examples.ModulAutentifikacija.ViewModels;
using FIT_Api_Examples.ModulGeneralUser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulAutentifikacija.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AutentifikacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public AutentifikacijaToken Login([FromBody] LoginVM x)
        {
            //1- provjera logina
            KorisnickiNalog logiraniKorisnik = _dbContext.KorisnickiNalog.Include(kn => kn.Uloga).SingleOrDefault(k => k.Username != null && k.Username == x.korisnickoIme && k.Password == x.lozinka);

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return null;
            }


            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = "1.2.3.4",
                vrijednost = randomString,
                korisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now
            };

            _dbContext.AutentifikacijaToken.Add(noviToken);
            _dbContext.SaveChanges();

            //4- vratiti token string
            return noviToken;
        }


    }
}
