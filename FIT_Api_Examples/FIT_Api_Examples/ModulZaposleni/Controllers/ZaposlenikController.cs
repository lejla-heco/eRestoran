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
    }
}
