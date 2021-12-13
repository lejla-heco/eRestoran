using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.ModulZaposleni.Models;
using FIT_Api_Examples.ModulZaposleni.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulZaposleni.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ZaposlenikController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ZaposlenikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Add([FromBody] ZaposlenikAddVM zaposlenikAddVM)
        {
            int ulogaid = _dbContext.Uloga.Where(u => u.Naziv == "Zaposlenik").SingleOrDefault().ID;
            Zaposlenik noviZaposlenik = new Zaposlenik()
            {
                Ime=zaposlenikAddVM.ime,
                Prezime=zaposlenikAddVM.prezime,
                Email=zaposlenikAddVM.email,
                Username=zaposlenikAddVM.username,
                Password=zaposlenikAddVM.password,
                UlogaID=ulogaid
                
            };
            _dbContext.Zaposlenik.Add(noviZaposlenik);
            _dbContext.SaveChanges();
            return Ok(noviZaposlenik.ID);
        }
        [HttpGet]
        public List<Zaposlenik> GetAll()
        {
            return _dbContext.Zaposlenik.ToList();
        }
        [HttpPost("{id}")]
        public ActionResult AddSlika(int id, [FromForm] ZaposlenikAddSlikaVM zaposlenikAddSlikaVM)
        {
            try
            {
                Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);

                if (zaposlenikAddSlikaVM.slikaZaposlenika != null && zaposlenik != null)
                {
                    if (zaposlenikAddSlikaVM.slikaZaposlenika.Length > 250 * 1000)
                        return BadRequest("max velicina fajla je 250 KB");

                    string ekstenzija = Path.GetExtension(zaposlenikAddSlikaVM.slikaZaposlenika.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    zaposlenikAddSlikaVM.slikaZaposlenika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                    zaposlenik.Slika = Config.SlikeURL + filename;
                    _dbContext.SaveChanges();
                }

                return Ok(zaposlenik);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
        [HttpGet]
        public List<ZaposlenikGetAllPagedVM> GetAllPaged()
        {
            List<ZaposlenikGetAllPagedVM> pagedStavke = _dbContext.Zaposlenik
                                          .Select(z => new ZaposlenikGetAllPagedVM()
                                            {
                                                id = z.ID,
                                                ime = z.Ime,
                                                prezime = z.Prezime,
                                                email = z.Email,
                                                slika = z.Slika,
                                                username = z.Username,
                                                password = z.Password,
                                                obavljeneNarudzbe = z.ObavljeneNarudzbe,
                                                
                                            }).ToList();
            return pagedStavke;
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);

            if (zaposlenik == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(zaposlenik);

            _dbContext.SaveChanges();
            return Ok(zaposlenik);
        }

        [HttpGet("{id}")]
        public ZaposlenikUpdateVM GetById(int id)
        {
            Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);
            if (zaposlenik != null)
            {
                ZaposlenikUpdateVM odabraniZaposlenik = new ZaposlenikUpdateVM()
                {
                    id = zaposlenik.ID,
                    ime = zaposlenik.Ime,
                    prezime = zaposlenik.Prezime,
                    email = zaposlenik.Email,
                    username = zaposlenik.Username,
                    password = zaposlenik.Password,
                    slika = zaposlenik.Slika
                };
                return odabraniZaposlenik;
            }
            return null;
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] ZaposlenikUpdateVM zaposlenikUpdateVM)
        {

            Zaposlenik zaposlenik = _dbContext.Zaposlenik.Find(id);

            if (zaposlenik == null)
                return BadRequest("pogresan ID");

            zaposlenik.Ime = zaposlenikUpdateVM.ime.RemoveTags();
            zaposlenik.Prezime = zaposlenikUpdateVM.prezime.RemoveTags();
            zaposlenik.Email = zaposlenikUpdateVM.email.RemoveTags();
            zaposlenik.Username = zaposlenikUpdateVM.username.RemoveTags();
            zaposlenik.Password = zaposlenikUpdateVM.password.RemoveTags();
           
            //Console.WriteLine(5);
            _dbContext.SaveChanges();
            return Ok(zaposlenik.ID);
        }
    }
}
