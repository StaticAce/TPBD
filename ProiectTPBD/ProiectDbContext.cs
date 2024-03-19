using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProiectTPBD
{
    internal class ProiectDbContext : DbContext
    {
        public DbSet<Angajat> Angajati { get; set; }
        public DbSet<Calculare> Calculare { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=WALTER;Database=ProiectTPBD;Trusted_Connection=True;TrustServerCertificate=True");
    }

    [Table("Angajat")]
    public class Angajat
    {
        [Key]
        public int NrCrt { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Functie { get; set; }
        public int Salar_baza { get; set; }
        public int Spor { get; set; }
        public int Premii_brute { get; set; }
        public int Total_brut { get; set; }
        public int Brut_Impozitabil { get; set; }
        public int Impozit { get; set; }
        public int Cas { get; set; }
        public int Cass { get; set; }
        public int Retineri { get; set; }
        public int Virat_Card { get; set; }
    }


    [Table("Calculare")]
    public class Calculare
    {
        [Key]
        public int Id { get; set; }
        public int Impozit { get; set; }
        public int Cas { get; set; }
        public int Cass { get; set; }
    }


}
