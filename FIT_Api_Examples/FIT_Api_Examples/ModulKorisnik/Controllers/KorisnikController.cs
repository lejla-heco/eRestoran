using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulAutentifikacija.Models;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulKorisnik.ViewModels;
using FIT_Api_Examples.ModulNarudzba.Models;
using FIT_Api_Examples.ModulRezervacija.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulKorisnik.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController : Controller
    {
        private ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Add([FromBody] RegistracijaVM registracijaVM)
        {
            Korisnik noviKorisnik = new Korisnik()
            {
                Ime = registracijaVM.ime,
                Prezime = registracijaVM.prezime,
                Email = registracijaVM.email,
                AdresaStanovanja = registracijaVM.adresaStanovanja,
                BrojTelefona = registracijaVM.brojTelefona,
                KorisnickoIme = registracijaVM.username,
                Lozinka = registracijaVM.password,
                OpstinaID = registracijaVM.opstinaId
            };
            _dbContext.Korisnik.Add(noviKorisnik);
            _dbContext.SaveChanges();
            return Ok(noviKorisnik.ID);
        }
        [HttpGet]
        public ActionResult Delete()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;
            List<Narudzba> trenutneNarudzbe = _dbContext.Narudzba.Where(n => n.KorisnikID == korisnik.ID && n.StatusNarudzbeID != 6 && n.Zakljucena).ToList();
            if (trenutneNarudzbe != null && trenutneNarudzbe?.Count != 0)
                return BadRequest("Trenutno ne mozete deaktivirati profil jer su Vase narudzbe u izradi");

            List<KorisnikKupon> kuponiKorisnika = _dbContext.KorisnikKupon.Where(kk => kk.KorisnikID == korisnik.ID).ToList();
            List<Narudzba> narudzbe = _dbContext.Narudzba.Where(n => n.KorisnikID == korisnik.ID).ToList();
            List<StavkaNarudzbe> stavkeNarudzbi = new List<StavkaNarudzbe>();
            foreach(Narudzba narudzba in narudzbe)
            {
                stavkeNarudzbi.AddRange(_dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == narudzba.ID).ToList());
            }
            List<Rezervacija> rezervacije = _dbContext.Rezervacija.Where(r => r.KorisnikID == korisnik.ID).ToList();
            List<OmiljenaStavka> omiljeneStavke = _dbContext.OmiljenaStavka.Where(os => os.KorisnikID == korisnik.ID).ToList();
            List<AutentifikacijaToken> logovi = _dbContext.AutentifikacijaToken.Where(at => at.KorisnickiNalogId == korisnik.ID).ToList();

            _dbContext.KorisnikKupon.RemoveRange(kuponiKorisnika);
            _dbContext.StavkaNarudzbe.RemoveRange(stavkeNarudzbi);
            _dbContext.Narudzba.RemoveRange(narudzbe);
            _dbContext.Rezervacija.RemoveRange(rezervacije);
            _dbContext.OmiljenaStavka.RemoveRange(omiljeneStavke);
            _dbContext.AutentifikacijaToken.RemoveRange(logovi);
            _dbContext.Korisnik.Remove(korisnik);
            _dbContext.KorisnickiNalog.Remove(korisnik);
            _dbContext.SaveChanges();

            return Ok();
        }
        [HttpGet]
        public List<Korisnik> GetAll()
        {
            return _dbContext.Korisnik.ToList();
        }
    }
}
