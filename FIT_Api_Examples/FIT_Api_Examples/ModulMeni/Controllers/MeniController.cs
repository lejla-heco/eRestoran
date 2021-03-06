using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulMeni.Models;
using FIT_Api_Examples.ModulMeni.ViewModels;
using FIT_Api_Examples.ModulNarudzba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulMeni.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MeniController : Controller
    {
        private ApplicationDbContext _dbContext;

        public MeniController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<MeniStavka> GetAll()
        {
            return _dbContext.MeniStavka.ToList();
        }

        [HttpPost]
        public ActionResult Add([FromBody] MeniAddVM meniAddVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");

            MeniStavka meniStavkaNova = new MeniStavka()
            {
                Naziv = meniAddVM.naziv,
                Opis = meniAddVM.opis,
                Cijena = meniAddVM.cijena,
                SnizenaCijena = meniAddVM.snizenaCijena,
                MeniGrupaID = meniAddVM.meniGrupaId
            };
            _dbContext.MeniStavka.Add(meniStavkaNova);
            _dbContext.SaveChanges();
            return Ok(meniStavkaNova.ID);
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] MeniUpdateVM meniUpdateVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");

            MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);

            if (meniStavka == null)
                return BadRequest("pogresan ID");

            meniStavka.Naziv = meniUpdateVM.naziv.RemoveTags();
            meniStavka.Opis =meniUpdateVM.opis.RemoveTags();
            meniStavka.Cijena = meniUpdateVM.cijena;
            meniStavka.SnizenaCijena = meniUpdateVM.snizenaCijena;
            meniStavka.MeniGrupaID = meniUpdateVM.meniGrupaId;
            _dbContext.SaveChanges();
            return Ok(meniStavka.ID);
        }





    

        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
               return BadRequest("Nije logiran");


            MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);

            if (meniStavka == null)
                return BadRequest("pogresan ID");

            List<StavkaNarudzbe> stavkaNarudzbe = _dbContext.StavkaNarudzbe.Where(sn => sn.MeniStavkaID == meniStavka.ID).ToList();

            List<Narudzba> narudzba = new List<Narudzba>();
            
            foreach (StavkaNarudzbe stavka in stavkaNarudzbe)
            {
                narudzba = _dbContext.Narudzba.Where(n => n.ID == stavka.NarudzbaID).ToList();
                
                   
            }
           
            List<OmiljenaStavka> omiljeneStavke = _dbContext.OmiljenaStavka.Where(os => os.MeniStavkaID == meniStavka.ID).ToList();
          
            _dbContext.StavkaNarudzbe.RemoveRange(stavkaNarudzbe);
            _dbContext.Narudzba.RemoveRange(narudzba);
            _dbContext.OmiljenaStavka.RemoveRange(omiljeneStavke);
           
            
            _dbContext.MeniStavka.Remove(meniStavka);

            _dbContext.SaveChanges();
            return Ok(meniStavka);
        }
        [HttpPost("{id}")]
        public ActionResult AddSlika(int id, [FromForm] MeniAddSlikaVM meniAddSlikaVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdministrator)
                return BadRequest("nije logiran");
            try
            {
                MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);

                if (meniAddSlikaVM.slikaMeniStavke != null && meniStavka != null)
                {
                    if (meniAddSlikaVM.slikaMeniStavke.Length > 250 * 1000)
                        return BadRequest("max velicina fajla je 250 KB");

                    string ekstenzija = Path.GetExtension(meniAddSlikaVM.slikaMeniStavke.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    meniAddSlikaVM.slikaMeniStavke.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                    meniStavka.Slika = Config.SlikeURL + filename;
                    _dbContext.SaveChanges();
                }

                return Ok(meniStavka);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
        [HttpPost("{id}")]
        public ActionResult AddOcjena(int id,[FromBody] MeniAddOcjenaVM meniAddOcjenaVM)
        {
             MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);
                if ( meniStavka != null)
                {
                     if(meniStavka.Ocjena==0) meniStavka.Ocjena = meniAddOcjenaVM.ocjena; //ukoliko je nemi stavka tek dodana
                    meniStavka.Ocjena += meniAddOcjenaVM.ocjena;
                    meniStavka.Ocjena = (float)Math.Round(meniStavka.Ocjena / 2, 2);
                    _dbContext.SaveChanges();
                }

                return Ok(meniStavka);
            }
            

        [HttpGet]
        public List<MeniGetAllPagedVM> GetAllPaged(string nazivKategorije)
        {
            List<MeniGetAllPagedVM> pagedStavke = _dbContext.MeniStavka
                                            .Where(ms => ms.MeniGrupa.Naziv == nazivKategorije)
                                            .Select(ms => new MeniGetAllPagedVM() {
                                                id = ms.ID,
                                                naziv = ms.Naziv,
                                                opis = ms.Opis,
                                                cijena = ms.Cijena,
                                                slika = ms.Slika,
                                                izdvojeno = ms.Izdvojeno,
                                                snizenaCijena = ms.SnizenaCijena,
                                                ocjena = ms.Ocjena,
                                                nazivGrupe = ms.MeniGrupa.Naziv
                                            }).ToList();
            return pagedStavke;
        }

        [HttpPost]
        public IActionResult GetAllPagedLog([FromBody] MeniGAPLogInfoVM meniGAPLogInfoVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;
            if (korisnik == null)
                return null;
            List<MeniGetAllPagedLogVM> pagedStavke = _dbContext.MeniStavka
                                            .Where(ms => ms.MeniGrupa.Naziv == meniGAPLogInfoVM.kategorija)
                                            .Select(ms => new MeniGetAllPagedLogVM()
                                            {
                                                id = ms.ID,
                                                naziv = ms.Naziv,
                                                opis = ms.Opis,
                                                cijena = ms.Cijena,
                                                slika = ms.Slika,
                                                izdvojeno = ms.Izdvojeno,
                                                snizenaCijena = ms.SnizenaCijena,
                                                ocjena = ms.Ocjena,
                                                nazivGrupe = ms.MeniGrupa.Naziv,
                                                omiljeno =  _dbContext.OmiljenaStavka
                                                            .Where(os => os.KorisnikID == korisnik.ID && os.MeniStavkaID == ms.ID)
                                                            .SingleOrDefault() != null ? true : false
                                            }).ToList();
            return Ok(pagedStavke);
        }

        [HttpGet("{id}")]
        public MeniUpdateVM GetById(int id)
        {
            MeniStavka meniStavka = _dbContext.MeniStavka.Find(id);
            if (meniStavka != null)
            {
                MeniUpdateVM odabranaStavka = new MeniUpdateVM()
                {
                    id = meniStavka.ID,
                    naziv = meniStavka.Naziv,
                    opis = meniStavka.Opis,
                    cijena = meniStavka.Cijena,
                    snizenaCijena = meniStavka.SnizenaCijena,
                    meniGrupaId = meniStavka.MeniGrupaID,
                    slika = meniStavka.Slika
                };
                return odabranaStavka;
            }
            return null;
        }
    }
}
