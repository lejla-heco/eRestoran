using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulNarudzba.Models;
using FIT_Api_Examples.ModulNarudzba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulNarudzba.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StatusNarudzbeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StatusNarudzbeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add([FromForm] StatusNarudzbeAddVM statusNarudzbeAddVM)
        {
            StatusNarudzbe statusNarudzbe = new StatusNarudzbe()
            {
                Naziv = statusNarudzbeAddVM.naziv
            };
            _dbContext.StatusNarudzbe.Add(statusNarudzbe);
            _dbContext.SaveChanges();
            return Ok(statusNarudzbe);
        }

        [HttpGet]
        public List<StatusNarudzbe> GetAll()
        {
            return _dbContext.StatusNarudzbe.ToList();
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            StatusNarudzbe statusNarudzbe = _dbContext.StatusNarudzbe.Find(id);
            if (statusNarudzbe == null)
                return BadRequest("Nepostojeci status narudzbe");

            _dbContext.StatusNarudzbe.Remove(statusNarudzbe);
            _dbContext.SaveChanges();
            return Ok(statusNarudzbe);
        }
    }
}
