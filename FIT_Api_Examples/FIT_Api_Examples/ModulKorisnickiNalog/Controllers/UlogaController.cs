using FIT_Api_Examples.Data;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using FIT_Api_Examples.ModulKorisnickiNalog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnickiNalog.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UlogaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public UlogaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add([FromForm] UlogaAddVM ulogaAddVM)
        {
            Uloga uloga = new Uloga()
            {
                Naziv = ulogaAddVM.naziv
            };
            _dbContext.Uloga.Add(uloga);
            _dbContext.SaveChanges();
            return Ok(uloga);
        }

        [HttpGet]
        public List<Uloga> GetAll()
        {
            return _dbContext.Uloga.ToList();
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            Uloga uloga = _dbContext.Uloga.Find(id);
            if (uloga == null)
                return BadRequest("Nepostojeca uloga");

            _dbContext.Uloga.Remove(uloga);
            _dbContext.SaveChanges();
            return Ok(uloga);
        }
    }
}
