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
            return Ok();
        }

        [HttpPost]
        public IActionResult GetAllPaged([FromBody] OmiljenaStavkaInfoVM omiljenaStavkaInfoVM)
        {
            if (_dbContext.Korisnik.Find(omiljenaStavkaInfoVM.id) == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            List<OmiljenaStavkaGetAllVM> omiljeneStavke = _dbContext.OmiljenaStavka.Where(os => os.KorisnikID == omiljenaStavkaInfoVM.id)
                                                            .Select(os => new OmiljenaStavkaGetAllVM()
                                                            {
                                                                omiljenaStavkaId = os.ID,
                                                                id = os.MeniStavkaID,
                                                                naziv = os.MeniStavka.Naziv,
                                                                opis = os.MeniStavka.Opis,
                                                                cijena = os.MeniStavka.Cijena,
                                                                slika = os.MeniStavka.Slika,
                                                                izdvojeno = os.MeniStavka.Izdvojeno,
                                                                snizenaCijena = os.MeniStavka.SnizenaCijena,
                                                                ocjena = os.MeniStavka.Ocjena,
                                                                nazivGrupe = os.MeniStavka.MeniGrupa.Naziv
                                                            }).Where(os => os.nazivGrupe == omiljenaStavkaInfoVM.kategorija).ToList();
            return Ok(omiljeneStavke);
        }

        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            OmiljenaStavka omiljenaStavka = _dbContext.OmiljenaStavka.Find(id);
            if (omiljenaStavka == null)
                return BadRequest("Nepostojeca omiljena stavka!");

            _dbContext.OmiljenaStavka.Remove(omiljenaStavka);
            _dbContext.SaveChanges();
            return Ok(omiljenaStavka);
        }

        [HttpGet]
        public IActionResult DeleteById(int id, int stavkaId)
        {
            OmiljenaStavka omiljenaStavka = _dbContext.OmiljenaStavka.Where(os => os.KorisnikID == id && os.MeniStavkaID == stavkaId).SingleOrDefault();
            if (omiljenaStavka == null)
                return BadRequest("Nepostojeca omiljena stavka!");

            _dbContext.OmiljenaStavka.Remove(omiljenaStavka);
            _dbContext.SaveChanges();
            return Ok(omiljenaStavka);
        }
    }
}
