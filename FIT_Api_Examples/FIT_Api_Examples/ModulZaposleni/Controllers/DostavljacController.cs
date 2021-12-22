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
    public class DostavljacController : Controller
    {
        private ApplicationDbContext _dbContext;

        public DostavljacController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Add([FromBody] DostavljacAddVM dostavljacAddVM)
        {
            Dostavljac noviDostavljac = new Dostavljac()
            {
                Ime = dostavljacAddVM.ime,
                Prezime = dostavljacAddVM.prezime,
                Email = dostavljacAddVM.email,
                KorisnickoIme = dostavljacAddVM.username,
                Lozinka = dostavljacAddVM.password

            };
            noviDostavljac.AktivneNarudzbe = 0;
            noviDostavljac.DostavljeneNarudzbe = 0;
            _dbContext.Dostavljac.Add(noviDostavljac);
            _dbContext.SaveChanges();
            return Ok(noviDostavljac.ID);
        }
        [HttpPost("{id}")]
        public ActionResult AddSlika(int id, [FromForm] DostavljacAddSlikaVM dostavljacAddSlikaVM)
        {
            try
            {
                Dostavljac dostavljac = _dbContext.Dostavljac.Find(id);

                if (dostavljacAddSlikaVM.slikaDostavljaca != null && dostavljac != null)
                {
                    if (dostavljacAddSlikaVM.slikaDostavljaca.Length > 250 * 1000)
                        return BadRequest("max velicina fajla je 250 KB");

                    string ekstenzija = Path.GetExtension(dostavljacAddSlikaVM.slikaDostavljaca.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    dostavljacAddSlikaVM.slikaDostavljaca.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                    dostavljac.Slika = Config.SlikeURL + filename;
                    _dbContext.SaveChanges();
                }

                return Ok(dostavljac);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
        [HttpGet]
        public List<Dostavljac> GetAll()
        {
            return _dbContext.Dostavljac.ToList();
        }
        [HttpGet("{id}")]
        public DostavljacUpdateVM GetById(int id)
        {
            Dostavljac dostavljac = _dbContext.Dostavljac.Find(id);
            if (dostavljac != null)
            {
                DostavljacUpdateVM odabraniDostavljac = new DostavljacUpdateVM()
                {
                    id = dostavljac.ID,
                    ime = dostavljac.Ime,
                    prezime = dostavljac.Prezime,
                    email = dostavljac.Email,
                    username = dostavljac.KorisnickoIme,
                    password = dostavljac.Lozinka,
                    slika = dostavljac.Slika
                };
                return odabraniDostavljac;
            }
            return null;
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] DostavljacUpdateVM dostavljacUpdateVM)
        {

            Dostavljac dostavljac = _dbContext.Dostavljac.Find(id);

            if (dostavljac == null)
                return BadRequest("pogresan ID");

            dostavljac.Ime = dostavljacUpdateVM.ime.RemoveTags();
            dostavljac.Prezime = dostavljacUpdateVM.prezime.RemoveTags();
            dostavljac.Email = dostavljacUpdateVM.email.RemoveTags();
            dostavljac.KorisnickoIme = dostavljacUpdateVM.username.RemoveTags();
            dostavljac.Lozinka = dostavljacUpdateVM.password.RemoveTags();

            
            _dbContext.SaveChanges();
            return Ok(dostavljac.ID);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Dostavljac dostavljac = _dbContext.Dostavljac.Find(id);

            if (dostavljac == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(dostavljac);

            _dbContext.SaveChanges();
            return Ok(dostavljac);
        }
    }
}
