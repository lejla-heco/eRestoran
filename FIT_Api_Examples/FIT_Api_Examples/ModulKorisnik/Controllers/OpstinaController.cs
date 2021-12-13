using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulKorisnik.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OpstinaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public OpstinaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Opstina> GetAll()
        {
            return _dbContext.Opstina.ToList();
        }

        [HttpPost]
        public ActionResult Add(string naziv)
        {
            Opstina novaOpstina = new Opstina() { Naziv = naziv };
            _dbContext.Opstina.Add(novaOpstina);
            _dbContext.SaveChanges();
            return Ok(novaOpstina.ID);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Opstina opstina = _dbContext.Opstina.Find(id);

            if (opstina == null )//|| id == 1
                return BadRequest("pogresan ID");

            _dbContext.Remove(opstina);

            _dbContext.SaveChanges();
            return Ok(opstina);
        }
    }
}
