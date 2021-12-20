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
    }
}
