using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulMeni.Models;
using FIT_Api_Examples.ModulMeni.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulMeni.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PosebnaPonudaController : Controller
    {
        private ApplicationDbContext _dbContext;
        public PosebnaPonudaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public List<PosebnaPonudaGetAllVM> GetAll()
        {
            List<PosebnaPonudaGetAllVM> posebnaPonuda = _dbContext.MeniStavka
                .Where(ms => ms.Izdvojeno)
                .Select(ms => new PosebnaPonudaGetAllVM() { 
                    id = ms.ID,
                    naziv = ms.Naziv,
                    opis = ms.Opis,
                    snizenaCijena = ms.SnizenaCijena,
                    slika = ms.Slika,
                    ocjena = ms.Ocjena,
                    meniGrupaNaziv = ms.MeniGrupa.Naziv
                }).ToList();
            return posebnaPonuda;
        }

        [HttpPost]
        public IActionResult Izdvoji([FromBody]int id)
        {
            MeniStavka izdvojenaStavka = _dbContext.MeniStavka.Find(id);
            izdvojenaStavka.Izdvojeno = true;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Ukloni([FromBody]int id)
        {
            MeniStavka izdvojenaStavka = _dbContext.MeniStavka.Find(id);
            izdvojenaStavka.Izdvojeno = false;
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
