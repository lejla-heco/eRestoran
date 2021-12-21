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
    public class StatusRezervacijeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StatusRezervacijeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<StatusRezervacije> GetAll()
        {
            return _dbContext.StatusRezervacije.ToList();
        }

        [HttpPost]
        public ActionResult Add(string naziv)
        {
            StatusRezervacije noviStatusRezervacije = new StatusRezervacije() { Naziv = naziv };
            _dbContext.StatusRezervacije.Add(noviStatusRezervacije);
            _dbContext.SaveChanges();
            return Ok(noviStatusRezervacije.ID);
        }
    }
}
