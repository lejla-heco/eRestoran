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
                RadnoVrijemeRedovno = poslovnicaAddVM.radnoVrijemeRedovno,
                RadnoVrijemeVikend = poslovnicaAddVM.radnoVrijemeVikend,
                lat = poslovnicaAddVM.lat,
                lng = poslovnicaAddVM.lng,
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

        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");

            Poslovnica poslovnica = _dbContext.Poslovnica.Find(id);
            _dbContext.Poslovnica.Remove(poslovnica);
            _dbContext.SaveChanges();
            return Ok(poslovnica);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");

            Poslovnica odabranaPoslovnica = _dbContext.Poslovnica.Find(id);
            if (odabranaPoslovnica == null)
                return BadRequest("Poslovnica ne postoji");

            PoslovnicaGetByIdVM poslovnica = new PoslovnicaGetByIdVM()
            {
                id = odabranaPoslovnica.ID,
                adresa = odabranaPoslovnica.Adresa,
                brojTelefona = odabranaPoslovnica.BrojTelefona,
                radnoVrijemeRedovno = odabranaPoslovnica.RadnoVrijemeRedovno,
                radnoVrijemeVikend = odabranaPoslovnica.RadnoVrijemeVikend,
                lat = odabranaPoslovnica.lat,
                lng = odabranaPoslovnica.lng,
                opstinaId = odabranaPoslovnica.OpstinaID
            };

            return Ok(poslovnica);
        }

        [HttpPost]
        public IActionResult Update([FromBody] PoslovnicaGetByIdVM poslovnicaGetByIdVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");

            Poslovnica poslovnica = _dbContext.Poslovnica.Find(poslovnicaGetByIdVM.id);
            if (poslovnica == null)
                return BadRequest("Poslovnica ne postoji");

            poslovnica.Adresa = poslovnicaGetByIdVM.adresa;
            poslovnica.BrojTelefona = poslovnicaGetByIdVM.brojTelefona;
            poslovnica.RadnoVrijemeRedovno = poslovnicaGetByIdVM.radnoVrijemeRedovno;
            poslovnica.RadnoVrijemeVikend = poslovnicaGetByIdVM.radnoVrijemeVikend;
            poslovnica.lat = poslovnicaGetByIdVM.lat;
            poslovnica.lng = poslovnicaGetByIdVM.lng;
            poslovnica.OpstinaID = poslovnicaGetByIdVM.opstinaId;

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
