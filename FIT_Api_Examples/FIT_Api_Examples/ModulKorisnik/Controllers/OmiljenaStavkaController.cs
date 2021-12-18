using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
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
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            MeniStavka meniStavka = _dbContext.MeniStavka.Find(omiljenaStavkaAddVM.meniStavkaId);
            if (meniStavka == null)
                return BadRequest("Nepostojeca meni stavka!");

            OmiljenaStavka omiljenaStavka = new OmiljenaStavka()
            {
                KorisnikID = korisnik.ID,
                MeniStavkaID = omiljenaStavkaAddVM.meniStavkaId,
            };

            _dbContext.OmiljenaStavka.Add(omiljenaStavka);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public ActionResult<PagedList<OmiljenaStavkaGetAllVM>> GetAllPaged([FromBody] OmiljenaStavkaGetAllPagedVM omiljenaStavkaGetAllPagedVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            var data = _dbContext.OmiljenaStavka.Where(os => os.KorisnikID == korisnik.ID)
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
                                                            }).Where(os => os.nazivGrupe == omiljenaStavkaGetAllPagedVM.kategorija).AsQueryable();

            var omiljeneStavke = PagedList<OmiljenaStavkaGetAllVM>.Create(data, omiljenaStavkaGetAllPagedVM.pageNumber, omiljenaStavkaGetAllPagedVM.itemsPerPage);
            return Ok(omiljeneStavke);
        }

        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            OmiljenaStavka omiljenaStavka = _dbContext.OmiljenaStavka.Find(id);
            if (omiljenaStavka == null)
                return BadRequest("Nepostojeca omiljena stavka!");

            _dbContext.OmiljenaStavka.Remove(omiljenaStavka);
            _dbContext.SaveChanges();
            return Ok(omiljenaStavka);
        }

        [HttpPost]
        public IActionResult DeleteById([FromBody] OmiljenaStavkaInfoVM omiljenaStavkaInfoVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            OmiljenaStavka omiljenaStavka = _dbContext.OmiljenaStavka.Where(os => os.KorisnikID == korisnik.ID && os.MeniStavkaID == omiljenaStavkaInfoVM.stavkaId).SingleOrDefault();
            if (omiljenaStavka == null)
                return BadRequest("Nepostojeca omiljena stavka!");

            _dbContext.OmiljenaStavka.Remove(omiljenaStavka);
            _dbContext.SaveChanges();
            return Ok(omiljenaStavka);
        }
    }
}
