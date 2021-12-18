using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
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
    public class PoslovnicaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public PoslovnicaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add([FromBody] PoslovnicaAddVM poslovnicaAddVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");

            Poslovnica poslovnica = new Poslovnica()
            {
                Adresa = poslovnicaAddVM.adresa,
                BrojTelefona = poslovnicaAddVM.brojTelefona,
                RadnoVrijeme = poslovnicaAddVM.radnoVrijeme,
                OpstinaID = poslovnicaAddVM.opstinaId,
            };
            _dbContext.Poslovnica.Add(poslovnica);
            _dbContext.SaveChanges();
            return Ok(poslovnica);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dbContext.Poslovnica.ToList());
        }
    }
}
