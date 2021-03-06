using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulMeni.Models;
using FIT_Api_Examples.ModulNarudzba.Models;
using FIT_Api_Examples.ModulNarudzba.ViewModels;
using FIT_Api_Examples.ModulZaposleni.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_Api_Examples.ModulNarudzba.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NarudzbaController : Controller
    {
        private ApplicationDbContext _dbContext;

        public NarudzbaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddStavka([FromBody] NarudzbaAddStavkaVM stavkaAddVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nepostojeci korisnik");

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == korisnik.ID && n.Zakljucena == false).SingleOrDefault();
            if (narudzba == null)
            {
                narudzba = new Narudzba()
                {
                    DatumNarucivanja = DateTime.Now,
                    Zakljucena = false,
                    Korisnik = korisnik,
                    BrojStavki = 0,
                    Cijena = 0,
                };
                _dbContext.Narudzba.Add(narudzba);
                _dbContext.SaveChanges();
            }

            MeniStavka meniStavka = _dbContext.MeniStavka.Find(stavkaAddVM.meniStavkaId);

            StavkaNarudzbe stavkaNarudzbe = new StavkaNarudzbe()
            {
                Kolicina = 1,
                MeniStavkaID = stavkaAddVM.meniStavkaId,
                NarudzbaID = narudzba.ID,
                Iznos = meniStavka.Izdvojeno ? meniStavka.SnizenaCijena : meniStavka.Cijena
            };

            _dbContext.StavkaNarudzbe.Add(stavkaNarudzbe);
            narudzba.BrojStavki++;
            narudzba.Cijena += stavkaNarudzbe.Iznos;
            _dbContext.SaveChanges();

            return Ok(narudzba.BrojStavki);
        }

        [HttpGet]
        public IActionResult GetNarudzba()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            int id = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik.ID;

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == id && n.Zakljucena == false).FirstOrDefault();
            if (narudzba == null)
            {
                narudzba = new Narudzba()
                {
                    DatumNarucivanja = DateTime.Now,
                    Zakljucena = false,
                    KorisnikID = id,
                    BrojStavki = 0,
                    Cijena = 0,
                    Omiljeno = false,
                };
                _dbContext.Narudzba.Add(narudzba);
                _dbContext.SaveChanges();
            }

            NarudzbaGetNarudzbaVM getNarudzbaVM = new NarudzbaGetNarudzbaVM()
            {
                id = narudzba.ID,
                cijena = narudzba.Cijena,
                omiljeno = narudzba.Omiljeno,
                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == narudzba.ID).Select(sn => new NarudzbaGetNarudzbaVM.Stavka()
                {
                    id = sn.ID,
                    naziv = sn.MeniStavka.Naziv,
                    opis = sn.MeniStavka.Opis,
                    cijena = sn.MeniStavka.Cijena,
                    slika = sn.MeniStavka.Slika,
                    izdvojeno = sn.MeniStavka.Izdvojeno,
                    snizenaCijena = sn.MeniStavka.SnizenaCijena,
                    ocjena = sn.MeniStavka.Ocjena,
                    kolicina = sn.Kolicina
                }).ToList(),
            };

            return Ok(getNarudzbaVM);
        }

        [HttpGet]
        public IActionResult GetBrojStavki()
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            int id = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik.ID;

            Korisnik korisnik = _dbContext.Korisnik.Find(id);
            if (korisnik == null) return Ok(0);

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == id && n.Zakljucena == false).FirstOrDefault();
            if (narudzba == null) return Ok(0);

            return Ok(narudzba.BrojStavki);
        }

        [HttpGet("{id}")]
        public IActionResult UkloniStavku(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            StavkaNarudzbe stavkaNarudzbe = _dbContext.StavkaNarudzbe.Where(sn => sn.ID == id).FirstOrDefault();

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.ID == stavkaNarudzbe.NarudzbaID).SingleOrDefault();
            if (narudzba == null)
                return BadRequest("Nepostojeca narudzba");

            _dbContext.StavkaNarudzbe.Remove(stavkaNarudzbe);
            narudzba.Cijena -= stavkaNarudzbe.Iznos;
            narudzba.BrojStavki -= stavkaNarudzbe.Kolicina;
            _dbContext.SaveChanges();

            NarudzbaGetNarudzbaVM getNarudzbaVM = new NarudzbaGetNarudzbaVM()
            {
                id = narudzba.ID,
                cijena = narudzba.Cijena,
                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == narudzba.ID).Select(sn => new NarudzbaGetNarudzbaVM.Stavka()
                {
                    id = sn.ID,
                    naziv = sn.MeniStavka.Naziv,
                    opis = sn.MeniStavka.Opis,
                    cijena = sn.MeniStavka.Cijena,
                    slika = sn.MeniStavka.Slika,
                    izdvojeno = sn.MeniStavka.Izdvojeno,
                    snizenaCijena = sn.MeniStavka.SnizenaCijena,
                    ocjena = sn.MeniStavka.Ocjena,
                    kolicina = sn.Kolicina
                }).ToList(),
            };

            return Ok(getNarudzbaVM);
        }
    
        [HttpPost]
        public IActionResult UpdateKolicina(NarudzbaUpdateKolicinaVM narudzbaUpdateKolicinaVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            StavkaNarudzbe stavkaNarudzbe = _dbContext.StavkaNarudzbe.Include(sn => sn.MeniStavka)
                                            .Where(sn => sn.ID == narudzbaUpdateKolicinaVM.id).SingleOrDefault();
            if (stavkaNarudzbe == null)
                return BadRequest("Nepostojeca stavka narudzbe");

            Narudzba narudzba = _dbContext.Narudzba.Find(stavkaNarudzbe.NarudzbaID);
            narudzba.Cijena -= stavkaNarudzbe.Iznos;
            narudzba.BrojStavki -= stavkaNarudzbe.Kolicina;

            stavkaNarudzbe.Kolicina = narudzbaUpdateKolicinaVM.kolicina;

            if (!stavkaNarudzbe.MeniStavka.Izdvojeno)
                stavkaNarudzbe.Iznos = stavkaNarudzbe.MeniStavka.Cijena * narudzbaUpdateKolicinaVM.kolicina;
            else
                stavkaNarudzbe.Iznos = stavkaNarudzbe.MeniStavka.SnizenaCijena * narudzbaUpdateKolicinaVM.kolicina;

            narudzba.Cijena += stavkaNarudzbe.Iznos;
            narudzba.BrojStavki += stavkaNarudzbe.Kolicina;

            _dbContext.SaveChanges();

            var response = new { 
                cijena = narudzba.Cijena,
                kolicina = narudzba.BrojStavki
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult OmiljenaNarudzba(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);
            if (narudzba == null)
                return BadRequest("Nepostojeca narudzba");

            narudzba.Omiljeno = true;
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult UkloniOmiljenu(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);
            if (narudzba == null)
                return BadRequest("Nepostojeca narudzba");

            narudzba.Omiljeno = false;
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Zakljuci(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            Narudzba narudzba = _dbContext.Narudzba.Where(n => n.KorisnikID == korisnik.ID && n.Zakljucena == false).SingleOrDefault();
            if (narudzba == null)
                return BadRequest("Ne postoji aktivna narudzba!");

            narudzba.Zakljucena = true;

            if (id != 0)
            {
                try
                {
                    Kupon kupon = _dbContext.Kupon.Find(id);
                    double novaCijena = narudzba.Cijena - (narudzba.Cijena * kupon.Popust / 100);
                    narudzba.Cijena = (float)Math.Round(novaCijena, 2);
                    KorisnikKupon korisnikKupon = _dbContext.KorisnikKupon.Where(kk => kk.KorisnikID == korisnik.ID && kk.KuponID == id && !kk.Iskoristen).FirstOrDefault();
                    if (korisnikKupon == null)
                        return BadRequest("Ne postoji kupon");
                    korisnikKupon.Iskoristen = true;
                    korisnikKupon.NarudzbaID = narudzba.ID;
                    narudzba.KuponKoristen = true;
                }
                catch(SystemException err)
                {
                    return BadRequest(err.Message + " inn " + err.InnerException);
                }
            }
            narudzba.StatusNarudzbeID = _dbContext.StatusNarudzbe.Where(s => s.Naziv == "Poslano").SingleOrDefault().ID;

            Zaposlenik odabraniZaposlenik = _dbContext.Zaposlenik
                .Where(z => z.AktivneNarudzbe == _dbContext.Zaposlenik.Min<Zaposlenik>(w => w.AktivneNarudzbe)).FirstOrDefault();
            odabraniZaposlenik.AktivneNarudzbe++;
            narudzba.Zaposlenik = odabraniZaposlenik;
            if (odabraniZaposlenik == null)
                return BadRequest("Nemamo zaposlenika!");
            _dbContext.SaveChanges();

            return Ok(narudzba.Cijena);
        }


        [HttpGet("{pageNumber}")]
        public IActionResult GetAllPaged(int pageNumber)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            var data = _dbContext.Narudzba.Where(n => n.KorisnikID == korisnik.ID && !n.Omiljeno && n.Zakljucena)
                                                            .Select(n => new NarudzbaGetAllPagedVM()
                                                            {
                                                                id = n.ID,
                                                                cijena = n.Cijena,
                                                                datumNarucivanja = n.DatumNarucivanja.ToString("dd/MM/yyyy hh:mm"),
                                                                status = n.StatusNarudzbe.Naziv,
                                                                isKoristenKupon = n.KuponKoristen,
                                                                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == n.ID).Select(sn => new NarudzbaGetAllPagedVM.Stavka()
                                                                {
                                                                    naziv = sn.MeniStavka.Naziv,
                                                                    kolicina = sn.Kolicina
                                                                }).ToList()
                                                            }).AsQueryable();

            var mojeNarudzbe = PagedList<NarudzbaGetAllPagedVM>.Create(data, pageNumber, 6);

            return Ok(mojeNarudzbe);
        }

        [HttpGet("{pageNumber}")]
        public IActionResult GetAllPagedZaposlenik(int pageNumber)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaZaposlenik)
                return BadRequest("nije logiran");

            Zaposlenik zaposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Zaposlenik;

            if (zaposlenik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

           

            var data = _dbContext.Narudzba.Where(n => n.ZaposlenikID == zaposlenik.ID && n.StatusNarudzbe.Naziv != "Spremljeno" && n.StatusNarudzbe.Naziv != "U dostavi" && n.StatusNarudzbe.Naziv != "Preuzeto")
                                                            .Select(n => new NarudzbaGetAllPagedZapolsenikVM()
                                                            {
                                                                id = n.ID,
                                                                cijena = n.Cijena,
                                                                datumNarucivanja = n.DatumNarucivanja.ToString("dd/MM/yyyy hh:mm"),
                                                                status = n.StatusNarudzbe.Naziv,
                                                                statusID=n.StatusNarudzbe.ID,
                                                                isKoristenKupon = n.KuponKoristen,
                                                                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == n.ID).Select(sn => new NarudzbaGetAllPagedZapolsenikVM.Stavka()
                                                                {
                                                                    naziv = sn.MeniStavka.Naziv,
                                                                    kolicina = sn.Kolicina
                                                                }).ToList()
                                                            }).AsQueryable();

            var mojeNarudzbe = PagedList<NarudzbaGetAllPagedZapolsenikVM>.Create(data, pageNumber, 6);

         
            return Ok(mojeNarudzbe);
        }
        [HttpPost("{id}")]
        public ActionResult UpdateStatusZaposlenik(int id, [FromBody] NarudzbaStatusUpdateVM narudzbaStatusUpdateVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaZaposlenik)
                return BadRequest("nije logiran");

            Zaposlenik zaposlenik = HttpContext.GetLoginInfo().korisnickiNalog.Zaposlenik;

            if (zaposlenik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);

            if (narudzba == null)
                return BadRequest("pogresan ID");
           
            
            narudzba.ID = narudzbaStatusUpdateVM.id;
            //narudzba.StatusNarudzbe.Naziv = narudzbaStatusUpdateVM.status;
            narudzba.StatusNarudzbeID= narudzbaStatusUpdateVM.statusID;

            if (narudzba.StatusNarudzbeID == 4 )// Spremljeno
            {
                zaposlenik.AktivneNarudzbe--;
                zaposlenik.ObavljeneNarudzbe++;
                if(_dbContext.Dostavljac.Count()==0)
                    return BadRequest("Trenutno nemamo dostavljača kojim bi dodijelili ovu narudžbu");
                Dostavljac odabraniDostavljac = _dbContext.Dostavljac
          .Where(d => d.AktivneNarudzbe == _dbContext.Dostavljac.Min<Dostavljac>(w => w.AktivneNarudzbe)).FirstOrDefault();
                odabraniDostavljac.AktivneNarudzbe++;
               
                    narudzba.Dostavljac = odabraniDostavljac;
              
                 if (odabraniDostavljac == null)
                     return BadRequest("Nemamo dostavljaca!");
                _dbContext.SaveChanges();
            }
           

            _dbContext.SaveChanges();
            //UcitajNarudzbeDostavljacima();
            return Ok(narudzba.ID);
        }

       

        [HttpGet("{pageNumber}")]
        public IActionResult GetAllPagedDostavljac(int pageNumber)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaDostavljac)
                return BadRequest("nije logiran");

            Dostavljac dostavljac = HttpContext.GetLoginInfo().korisnickiNalog.Dostavljac;

            if (dostavljac == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");



            var data = _dbContext.Narudzba.Where(n => n.DostavljacID == dostavljac.ID && n.StatusNarudzbe.Naziv != "Preuzeto" && n.StatusNarudzbe.Naziv != "Poslano" && n.StatusNarudzbe.Naziv != "U pripremi")
                                                            .Select(n => new NarudzbaGetAllPagedDostavljacVM()
                                                            {
                                                                id = n.ID,
                                                                cijena = n.Cijena,
                                                                datumNarucivanja = n.DatumNarucivanja.ToString("dd/MM/yyyy hh:mm"),
                                                                status = n.StatusNarudzbe.Naziv,
                                                                statusID = n.StatusNarudzbe.ID,
                                                                brojTelefona=n.Korisnik.BrojTelefona,
                                                                adresaStanovanja= n.Korisnik.AdresaStanovanja,
                                                                imeKupca=n.Korisnik.Ime + " " +n.Korisnik.Prezime,
                                                                isKoristenKupon = n.KuponKoristen,
                                                                stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == n.ID).Select(sn => new NarudzbaGetAllPagedDostavljacVM.Stavka()
                                                                {
                                                                    naziv = sn.MeniStavka.Naziv,
                                                                    kolicina = sn.Kolicina
                                                                }).ToList()
                                                            }).AsQueryable();

            var mojeNarudzbe = PagedList<NarudzbaGetAllPagedDostavljacVM>.Create(data, pageNumber, 6);

          
            return Ok(mojeNarudzbe);
        }
        [HttpPost("{id}")]
        public ActionResult UpdateStatusDostavljac(int id, [FromBody] NarudzbaStatusUpdateVM narudzbaStatusUpdateVM)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaDostavljac)
                return BadRequest("nije logiran");

            Dostavljac dostavljac = HttpContext.GetLoginInfo().korisnickiNalog.Dostavljac;

            if (dostavljac == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);

            if (narudzba == null)
                return BadRequest("pogresan ID");


            narudzba.ID = narudzbaStatusUpdateVM.id;
            //narudzba.StatusNarudzbe.Naziv = narudzbaStatusUpdateVM.status;
            narudzba.StatusNarudzbeID = narudzbaStatusUpdateVM.statusID;
            if (narudzba.StatusNarudzbeID == 6)
            {
                dostavljac.AktivneNarudzbe--;
                dostavljac.DostavljeneNarudzbe++;
            }
          


            _dbContext.SaveChanges();

          

            return Ok(narudzba.ID);
        }

        [HttpGet("{id}")]
        public ActionResult Naruci(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);
            narudzba.StatusNarudzbeID = _dbContext.StatusNarudzbe.Where(s => s.Naziv == "Poslano").SingleOrDefault().ID;
            Zaposlenik odabraniZaposlenik = _dbContext.Zaposlenik
                .Where(z => z.AktivneNarudzbe == _dbContext.Zaposlenik.Min<Zaposlenik>(w => w.AktivneNarudzbe)).FirstOrDefault();
            odabraniZaposlenik.AktivneNarudzbe++;
            narudzba.Zaposlenik = odabraniZaposlenik;
            if (odabraniZaposlenik == null)
                return BadRequest("Nemamo zaposlenika!");

            _dbContext.SaveChanges();
            string statusNaziv = _dbContext.Narudzba.Include(n => n.StatusNarudzbe).Where(n => n.ID == id).SingleOrDefault().StatusNarudzbe.Naziv;
            var response = new
            {
                status = statusNaziv,
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaKorisnik)
                return BadRequest("nije logiran");

            Korisnik korisnik = HttpContext.GetLoginInfo().korisnickiNalog.Korisnik;

            if (korisnik == null)
                return BadRequest("Nemate ovlasti za trazenu akciju!");

            Narudzba narudzba = _dbContext.Narudzba.Find(id);

            List<StavkaNarudzbe> stavke = _dbContext.StavkaNarudzbe.Where(sn => sn.NarudzbaID == id).ToList();
            KorisnikKupon korisnikKupon = _dbContext.KorisnikKupon.Where(kk => kk.KorisnikID == korisnik.ID && kk.NarudzbaID == id).SingleOrDefault();
            if (korisnikKupon != null)
            {
                _dbContext.KorisnikKupon.Remove(korisnikKupon);
            }
            _dbContext.RemoveRange(stavke);
            _dbContext.Narudzba.Remove(narudzba);
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
