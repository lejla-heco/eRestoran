using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulKorisnik.ViewModels;
using FIT_Api_Examples.ModulNarudzba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OmiljenaNarudzbaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public OmiljenaNarudzbaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{pageNumber}")]
        public ActionResult<PagedList<OmiljenaNarudzbaGetAllPagedVM>> GetAllPaged(int pageNumber)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            var data = _dbContext.Narudzba.Where(n=> n.KorisnikID == korisnik.ID && n.Omiljeno)
                                                            .Select(n => new OmiljenaNarudzbaGetAllPagedVM()
                                                            {
                                                                id = n.ID,
                                                                cijena = n.Cijena,
                                                                datumNarucivanja = n.DatumNarucivanja.ToString("dd/MM/yyyyy hh:mm"),
                                                                status = n.StatusNarudzbe.Naziv,
                                                                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == n.ID).Select(sn=> new OmiljenaNarudzbaGetAllPagedVM.Stavka()
                                                                {
                                                                    naziv = sn.MeniStavka.Naziv,
                                                                    kolicina = sn.Kolicina
                                                                }).ToList()
                                                            }).AsQueryable();

            var omiljeneStavke = PagedList<OmiljenaNarudzbaGetAllPagedVM>.Create(data, pageNumber, 3);
            return Ok(omiljeneStavke);
        }

        [HttpGet("{id}")]
        public ActionResult Naruci(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);
            narudzba.StatusNarudzbeID = _dbContext.StatusNarudzbe.Where(s => s.Naziv == "Poslano").SingleOrDefault().ID;
            _dbContext.SaveChanges();
            string statusNaziv = _dbContext.Narudzba.Include(n => n.StatusNarudzbe).Where(n => n.ID == id).SingleOrDefault().StatusNarudzbe.Naziv;
            var response = new
            {
                status = statusNaziv,
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);

            List<StavkaNarudzbe> stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == id).ToList();
            _dbContext.RemoveRange(stavke);
            _dbContext.Narudzba.Remove(narudzba);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
