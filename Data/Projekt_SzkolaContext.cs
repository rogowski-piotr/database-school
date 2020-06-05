using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projekt_Szkola.Models;

namespace Projekt_Szkola.Data
{
    public class Projekt_SzkolaContext : DbContext
    {
        public Projekt_SzkolaContext (DbContextOptions<Projekt_SzkolaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // jeden do wielu (nauczyciel i uprawnienie)
            modelBuilder.Entity<Uprawnienie>()
                .HasOne(p => p.Nauczyciel)
                .WithMany(p => p.Uprawnienies);

            // jeden do wielu (nauczyciel i zajecia)
            modelBuilder.Entity<Zajecia>()
                .HasOne(p => p.Nauczyciel)
                .WithMany(p => p.Zajecias);

            // jeden do wielu (klasa i zajecia)
            modelBuilder.Entity<Zajecia>()
                .HasOne(p => p.Klasa)
                .WithMany(p => p.Zajecias);

            // jeden do wielu (przedmiot i zajecia)
            modelBuilder.Entity<Zajecia>()
                .HasOne(p => p.Przedmiot)
                .WithMany(p => p.Zajecias);

            // jeden do jednego (nauczyciel i klasa)
            modelBuilder.Entity<Nauczyciel>()
                .HasOne(b => b.Klasa)
                .WithOne(b => b.Nauczyciel);

        }

        public DbSet<Projekt_Szkola.Models.Nauczyciel> Nauczyciel { get; set; }

        public DbSet<Projekt_Szkola.Models.Uprawnienie> Uprawnienie { get; set; }

        public DbSet<Projekt_Szkola.Models.Zajecia> Zajecia { get; set; }

        public DbSet<Projekt_Szkola.Models.Przedmiot> Przedmiot { get; set; }

        public DbSet<Projekt_Szkola.Models.Klasa> Klasa { get; set; }
    }
}
