using FIT_Api_Examples.ModulAutentifikacija.Models;
using FIT_Api_Examples.ModulKorisnickiNalog.Models;
using FIT_Api_Examples.ModulKorisnik.Models;
using FIT_Api_Examples.ModulMeni.Models;
using FIT_Api_Examples.ModulNarudzba.Models;
using FIT_Api_Examples.ModulRezervacija.Models;
using FIT_Api_Examples.ModulZaposleni.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FIT_Api_Examples.Data
{
    public class ApplicationDbContext : DbContext
    { 

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Poslovnica> Poslovnica { get; set; }
        public DbSet<Uloga> Uloga { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Kupon> Kupon { get; set; }
        public DbSet<KorisnikKupon> KorisnikKupon { get; set; }
        public DbSet<OmiljenaStavka> OmiljenaStavka { get; set; }
        public DbSet<Opstina> Opstina { get; set; }
        public DbSet<MeniGrupa> MeniGrupa { get; set; }
        public DbSet<MeniStavka> MeniStavka { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<StatusNarudzbe> StatusNarudzbe { get; set; }
        public DbSet<StavkaNarudzbe> StavkaNarudzbe { get; set; }
        public DbSet<Prigoda> Prigoda { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Dostavljac> Dostavljac { get; set; }
        public DbSet<Zaposlenik> Zaposlenik { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
