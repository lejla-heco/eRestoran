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
                Izdvojeno = meniAddVM.izdvojeno,
                MeniGrupaID = meniAddVM.meniGrupaId
            };

            if (meniAddVM.slikaMeniStavke != null)
            {
                if (meniAddVM.slikaMeniStavke.Length > 400 * 1000) 
                    return BadRequest("Maksimalna velicina slike je 400 KB");

                string ekstenzijaSlike = Path.GetExtension(meniAddVM.slikaMeniStavke.FileName);
                string noviNaziv = $"{Guid.NewGuid()}{ekstenzijaSlike}";
                meniAddVM.slikaMeniStavke.CopyTo(new FileStream(Config.SlikeFolder + noviNaziv, FileMode.Create));
                meniStavkaNova.Slika = Config.SlikeURL + noviNaziv;
            }
            _dbContext.MeniStavka.Add(meniStavkaNova);
            _dbContext.SaveChanges();
            return Ok(meniStavkaNova);
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
