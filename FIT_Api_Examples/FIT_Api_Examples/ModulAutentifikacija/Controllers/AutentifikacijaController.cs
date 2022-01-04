using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.ModulAutentifikacija.Models;
using FIT_Api_Examples.ModulAutentifikacija.ViewModels;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using FIT_Api_Examples.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FIT_Api_Examples.Helper.AutentifikacijaAutorizacija.MyAuthTokenExtension;

namespace FIT_Api_Examples.ModulAutentifikacija.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        private IHubContext<NotificationHub> _hubContext;

        public AutentifikacijaController(ApplicationDbContext dbContext, IHubContext<NotificationHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        [HttpPost]
        public ActionResult<LoginInformacije> Login([FromBody] LoginVM loginVM)
        {
            KorisnickiNalog logiraniKorisnik = _dbContext.KorisnickiNalog
                .FirstOrDefault(k => k.KorisnickoIme != null && k.KorisnickoIme == loginVM.korisnickoIme && k.Lozinka == loginVM.lozinka);
            
            if (logiraniKorisnik == null)
            {
                return new LoginInformacije(null);
            }

            string randomString = TokenGenerator.Generate(10);

            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                korisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            return new LoginInformacije(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();
            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();
            return autentifikacijaToken;
        }

        [HttpGet("{id}")]
        public KorisnickiNalog GetUser(int id)
        {
            return _dbContext.KorisnickiNalog.Find(id);
        }


    }
}
