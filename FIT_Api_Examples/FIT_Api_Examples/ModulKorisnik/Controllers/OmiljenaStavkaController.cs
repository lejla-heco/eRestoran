using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulKorisnik.ViewModels;
using FIT_Api_Examples.ModulMeni.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class OmiljenaStavkaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public OmiljenaStavkaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add([FromBody] OmiljenaStavkaAddVM omiljenaStavkaAddVM)
        {
            if (_dbContext.Korisnik.Find(omiljenaStavkaAddVM.korisnikId) == null) 
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            MeniStavka meniStavka = _dbContext.MeniStavka.Find(omiljenaStavkaAddVM.meniStavkaId);
            if (meniStavka == null)
                return BadRequest("Nepostojeca meni stavka!");

            OmiljenaStavka omiljenaStavka = new OmiljenaStavka()
            {
                KorisnikID = omiljenaStavkaAddVM.korisnikId,
                MeniStavkaID = omiljenaStavkaAddVM.meniStavkaId,
            };

            _dbContext.OmiljenaStavka.Add(omiljenaStavka);
            _dbContext.SaveChanges();
            return Ok(omiljenaStavka);
        }
    }
}
