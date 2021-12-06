using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulMeni.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulMeni.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MeniGrupaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public MeniGrupaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<MeniGrupa> GetAll()
        {
            return _dbContext.MeniGrupa.ToList();
        }

        [HttpPost]
        public ActionResult Add(string naziv)
        {
            MeniGrupa novaGrupa = new MeniGrupa() { Naziv = naziv };
            _dbContext.MeniGrupa.Add(novaGrupa);
            _dbContext.SaveChanges();
            return Ok(novaGrupa.ID);
        }
    }
}
