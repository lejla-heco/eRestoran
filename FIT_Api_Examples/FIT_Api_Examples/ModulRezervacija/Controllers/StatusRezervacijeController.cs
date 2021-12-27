using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulRezervacija.Models;
using FIT_Api_Examples.ModulRezervacija.ViewModels;
using FIT_Api_Examples.ModulZaposleni.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulRezervacija.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StatusRezervacijeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StatusRezervacijeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<StatusRezervacije> GetAll()
        {
            return _dbContext.StatusRezervacije.ToList();
        }

        [HttpPost]
        public ActionResult Add(string naziv)
        {
            StatusRezervacije noviStatusRezervacije = new StatusRezervacije() { Naziv = naziv };
            _dbContext.StatusRezervacije.Add(noviStatusRezervacije);
            _dbContext.SaveChanges();
            return Ok(noviStatusRezervacije.ID);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateStatusZaposlenik(int id, [FromBody] RezervacijaStatusUpdateVM rezervacijaStatusUpdateVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaZaposlenik)
                return BadRequest("nije logiran");

            Zaposlenik zaposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Zaposlenik;

            if (zaposlenik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Rezervacija rezervacija = _dbContext.Rezervacija.Find(id);

            if (rezervacija == null)
                return BadRequest("pogresan ID");


            rezervacija.ID = rezervacijaStatusUpdateVM.id;
            //narudzba.StatusNarudzbe.Naziv = narudzbaStatusUpdateVM.status;
            rezervacija.StatusRezervacijeID = rezervacijaStatusUpdateVM.statusID;

        

            _dbContext.SaveChanges();
            //UcitajNarudzbeDostavljacima();
            return Ok(rezervacija.ID);
        }
    }
}
