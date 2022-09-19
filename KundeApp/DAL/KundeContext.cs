using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KundeApp.Models
{
    public class Kunder
    {
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }

        virtual public Poststeder Poststed { get; set; }
    }

    public class Poststeder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Postnr { get; set; }
        public string Poststed { get; set; }
    }

    public class KundeContext :DbContext
    {
        public KundeContext (DbContextOptions<KundeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Kunder> Kunder { get; set; }

        public DbSet<Poststeder> Poststeder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
       
    }
}
