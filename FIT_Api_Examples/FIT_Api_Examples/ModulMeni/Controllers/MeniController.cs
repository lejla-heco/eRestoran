using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.ModulMeni.Models;
using FIT_Api_Examples.ModulMeni.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulMeni.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MeniController : Controller
    {
        private ApplicationDbContext _dbContext;

        public MeniController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<MeniStavka> GetAll()
        {
            return _dbContext.MeniStavka.ToList();
        }

        [HttpPost]
        public ActionResult Add([FromBody] MeniAddVM meniAddVM)
        {
            MeniStavka meniStavkaNova = new MeniStavka()
            {
                Naziv = meniAddVM.naziv,
                Opis = meniAddVM.opis,
                Cijena = meniAddVM.cijena,
                SnizenaCijena = meniAddVM.snizenaCijena,
                MeniGrupaID = meniAddVM.meniGrupaId
            };
            _dbContext.MeniStavka.Add(meniStavkaNova);
            _dbContext.SaveChanges();
            return Ok(meniStavkaNova.ID);
        }

        [HttpPost("{id}")]
        public ActionResult AddSlika(int id, [FromForm] MeniAddSlikaVM meniAddSlikaVM)
        {
            try
            {
                MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);

                if (meniAddSlikaVM.slikaMeniStavke != null && meniStavka != null)
                {
                    if (meniAddSlikaVM.slikaMeniStavke.Length > 250 * 1000)
                        return BadRequest("max velicina fajla je 250 KB");

                    string ekstenzija = Path.GetExtension(meniAddSlikaVM.slikaMeniStavke.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    meniAddSlikaVM.slikaMeniStavke.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                    meniStavka.Slika = Config.SlikeURL + filename;
                    _dbContext.SaveChanges();
                }

                return Ok(meniStavka);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpGet]
        public List<MeniGetAllPagedVM> GetAllPaged(string nazivKategorije)
        {
            List<MeniGetAllPagedVM> pagedStavke = _dbContext.MeniStavka
                                            .Where(ms => ms.MeniGrupa.Naziv == nazivKategorije)
                                            .Select(ms => new MeniGetAllPagedVM() {
                                                id = ms.ID,
                                                naziv = ms.Naziv,
                                                opis = ms.Opis,
                                                cijena = ms.Cijena,
                                                slika = ms.Slika,
                                                izdvojeno = ms.Izdvojeno,
                                                snizenaCijena = ms.SnizenaCijena,
                                                ocjena = ms.Ocjena
                                            }).ToList();
            return pagedStavke;
        }

        [HttpGet]
        public string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
