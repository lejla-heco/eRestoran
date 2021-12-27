using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnik.Models;
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
    public class RezervacijaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public RezervacijaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add([FromBody] RezervacijaAddVM rezervacijaAddVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");
            int statusid = _dbContext.StatusRezervacije.Where(s => s.Naziv == "Na čekanju").SingleOrDefault().ID;
            
            Rezervacija novaRezervacija = new Rezervacija()
            {

                KorisnikID = korisnik.ID,
                DatumRezerviranja = rezervacijaAddVM.datumRezerviranja,
                PrigodaID = rezervacijaAddVM.prigodaID,
                StatusRezervacijeID = statusid,
                BrojOsoba = rezervacijaAddVM.brojOsoba,
                BrojStolova = rezervacijaAddVM.brojStolova,
                Obavljena = false,
                Poruka = rezervacijaAddVM.poruka,
               
            };

            

            _dbContext.Rezervacija.Add(novaRezervacija);
            _dbContext.SaveChanges();
            return Ok(novaRezervacija);
        }

       
        [HttpGet]
        public List<RezervacijaGetAllVM> GetAll()
        {
            List<RezervacijaGetAllVM> rezervacije = _dbContext.Rezervacija
                                          .Select(z => new RezervacijaGetAllVM()
                                          {
                                              id=z.ID,
                                              brojOsoba=z.BrojOsoba,
                                              brojStolova=z.BrojStolova,
                                              datumRezerviranja=z.DatumRezerviranja,
                                              prigodaID=z.PrigodaID,
                                              nazivPrigode=z.Prigoda.Naziv,
                                               poruka=z.Poruka
                    


                                          }).ToList();
            return rezervacije;
        }

        [HttpGet("{pageNumber}")]
        public IActionResult GetAllPagedZaposlenik(int pageNumber)
        {

            if (!HttpContext.GetLoginInfo().isPermisijaZaposlenik)
                return BadRequest("nije logiran");

            Zaposlenik zaposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Zaposlenik;

            if (zaposlenik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");



            var data = _dbContext.Rezervacija.Select(z => new RezervacijaGetAllPagedZaposlenik()
            {
                id = z.ID,
                brojOsoba = z.BrojOsoba,
                brojStolova = z.BrojStolova,
                datumRezerviranja = z.DatumRezerviranja.ToString("dd/MM/yyyy hh:mm"),
                prigodaID = z.PrigodaID,
                statusID=z.StatusRezervacijeID,
                nazivStatusa=z.StatusRezervacije.Naziv,
                nazivPrigode = z.Prigoda.Naziv,
                poruka = z.Poruka
            }).AsQueryable();

            var mojeNarudzbe = PagedList<RezervacijaGetAllPagedZaposlenik>.Create(data, pageNumber, 6);


            return Ok(mojeNarudzbe);
           
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Rezervacija rezervacija = _dbContext.Rezervacija.Find(id);

            if (rezervacija == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(rezervacija);

            _dbContext.SaveChanges();
            return Ok(rezervacija);
        }
    }
}
