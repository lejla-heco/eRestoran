using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulKorisnik.ViewModels;
using FIT_Api_Examples.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private IHubContext<NotificationHub> _hubContext;
        public KuponController(ApplicationDbContext dbContext, IHubContext<NotificationHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult GenerisiKupon([FromBody] KuponGenerisiKuponVM kuponGenerisiKuponVM)
        {
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
    }
}
