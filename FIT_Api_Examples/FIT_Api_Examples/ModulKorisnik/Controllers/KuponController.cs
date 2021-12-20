using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulKorisnik.ViewModels;
using FIT_Api_Examples.ModulNarudzba.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KuponController : Controller
    {
        private ApplicationDbContext _dbContext;
        public KuponController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult GenerisiKupon([FromBody] KuponGenerisiKuponVM kuponGenerisiKuponVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");

            Kupon kupon = new Kupon()
            {
                Kod = Guid.NewGuid().ToString().Substring(0,12),
                Popust = kuponGenerisiKuponVM.popust,
                MaksimalniBrojKorisnika = kuponGenerisiKuponVM.maksimalniBrojKorisnika,
            };
            _dbContext.Kupon.Add(kupon);
            _dbContext.SaveChanges();

            List<Korisnik> korisnici = _dbContext.Korisnik.ToList();
            Random nasumicanBroj = new Random();

            for(int i =0; i<kuponGenerisiKuponVM.maksimalniBrojKorisnika; i++)
            {
                int indeks = nasumicanBroj.Next(0, korisnici.Count);
                Korisnik korisnik = korisnici[indeks];
                KorisnikKupon korisnikKupon = new KorisnikKupon()
                {
                    Korisnik = korisnik,
                    Kupon = kupon,
                    Iskoristen = false,
                };
                _dbContext.KorisnikKupon.Add(korisnikKupon);
                _dbContext.SaveChanges();
            }

            return Ok(kupon);   
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            int id = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik.ID;

            List<Kupon> kuponi = _dbContext.KorisnikKupon.Where(kk => kk.KorisnikID == id && !kk.Iskoristen).Select(kk => new Kupon()
            {
                ID = kk.KuponID,
                Kod = kk.Kupon.Kod,
                Popust = kk.Kupon.Popust
            }).ToList();

            return Ok(kuponi);
        }

        [HttpGet("{id}")]
        public IActionResult PrimijeniKupon(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            int korisnikId = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik.ID;

            Narudzba trenutnaNarudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == korisnikId && n.Zakljucena == false).FirstOrDefault();
            Kupon kupon = _dbContext.Kupon.Find(id);

            float novaCijena = trenutnaNarudzba.Cijena - (trenutnaNarudzba.Cijena * kupon.Popust / 100);

            return Ok(novaCijena);
        }
    }
}
