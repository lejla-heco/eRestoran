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
        public ActionResult Update(int id, [FromBody] MeniUpdateVM meniUpdateVM)
        {
            
            MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);

            if (meniStavka == null)
                return BadRequest("pogresan ID");

            meniStavka.Naziv = meniUpdateVM.naziv.RemoveTags();
            meniStavka.Opis =meniUpdateVM.opis.RemoveTags();
            meniStavka.Cijena = meniUpdateVM.cijena;
            meniStavka.SnizenaCijena = meniUpdateVM.snizenaCijena;
           meniStavka.MeniGrupaID = meniUpdateVM.meniGrupaId;

            _dbContext.SaveChanges();
            return Ok(meniStavka.ID);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);

            if (meniStavka == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(meniStavka);

            _dbContext.SaveChanges();
            return Ok(meniStavka);
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
        [HttpPost("{id}")]
        public ActionResult AddOcjena(int id, [FromBody] MeniAddOcjenaVM meniAddOcjenaVM)
        {
             MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);
            //int ocjena = 0;
                //int brojac = 0;
                if ( meniStavka != null)
                {
                     
                    meniStavka.Ocjena += meniAddOcjenaVM.ocjena;
                  if(meniStavka.Ocjena>5)
                    meniStavka.Ocjena =meniStavka.Ocjena/2;
                     
                    _dbContext.SaveChanges();
                }

                return Ok(meniStavka);
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
                                                ocjena = ms.Ocjena,
                                                nazivGrupe = ms.MeniGrupa.Naziv
                                            }).ToList();
            return pagedStavke;
        }

        [HttpGet("{id}")]
        public MeniUpdateVM GetById(int id)
        {
            MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);
            if (meniStavka != null)
            {
                MeniUpdateVM odabranaStavka = new MeniUpdateVM()
                {
                    id = meniStavka.ID,
                    naziv = meniStavka.Naziv,
                    opis = meniStavka.Opis,
                    cijena = meniStavka.Cijena,
                    snizenaCijena = meniStavka.SnizenaCijena,
                    meniGrupaId = meniStavka.MeniGrupaID,
                    slika = meniStavka.Slika
                };
                return odabranaStavka;
            }
            return null;
        }
    }
}
