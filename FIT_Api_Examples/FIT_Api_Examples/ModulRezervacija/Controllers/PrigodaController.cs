using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulRezervacija.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulRezervacija.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PrigodaController : Controller
    {
      
        private ApplicationDbContext _dbContext;

        public PrigodaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Prigoda> GetAll()
        {
            return _dbContext.Prigoda.ToList();
        }

        [HttpPost]
        public ActionResult Add(string naziv)
        {
            Prigoda novaPrigoda = new Prigoda() { Naziv = naziv };
            _dbContext.Prigoda.Add(novaPrigoda);
            _dbContext.SaveChanges();
            return Ok(novaPrigoda.ID);
        }
    }
}
