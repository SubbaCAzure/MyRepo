using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBMS.Repository.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<BarBeers> BarBeers { get; set; }
        public DbSet<BreweryBeers> BreweryBeers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BMSDB10; user id = LAPTOP-JT3UVOHA\\Subba Reddy; password=Chaganties; Trusted_Connection=true;Encrypt=false;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             

            modelBuilder.Entity<BreweryBeers>()
                .HasNoKey();
            //modelBuilder.Entity<BarBeers>()
            //    .HasNoKey();

            modelBuilder.Entity<BarBeers>()
                .HasKey(bb => new { bb.BarId, bb.BeerId });

        

            //     modelBuilder.Entity<BarBeers>()
            //.HasKey(bb => bb.BarId);

            //     modelBuilder.Entity<BarBeers>()
            //         .Property(bb => bb.BarName)
            //         .IsRequired();

            //     modelBuilder.Entity<BarBeers>()
            //         .HasMany(bb => bb.Beers)
            //         .WithOne()
            //         .HasForeignKey(b => b.Id)
            //         .OnDelete(DeleteBehavior.Cascade);

            //     modelBuilder.Entity<BreweryBeers>()
            //.HasKey(bb => bb.BreweryId);

            //     modelBuilder.Entity<BreweryBeers>()
            //         .Property(bb => bb.BreweryName)
            //         .IsRequired();

            //     modelBuilder.Entity<BreweryBeers>()
            //         .HasMany(bb => bb.Beers)
            //         .WithOne()
            //         .HasForeignKey(b => b.Id)
            //         .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);


        }


    }
}
