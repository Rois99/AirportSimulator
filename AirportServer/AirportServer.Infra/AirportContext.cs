using AirportServer.Domain.Models;
using AirportServer.Infra.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportServer.Infra
{
    public class AirportContext : DbContext
    {
        public virtual DbSet<StationDto> Stations { get; set; }
        public virtual DbSet<Airplane> Flights { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }


        public AirportContext(DbContextOptions<AirportContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
        }
    }

    public static class DataSeedExtantion
    {
        internal static void SeedData(this ModelBuilder builder)
        {
            Station station1 = new Station(1);
            Station station2 = new Station(2);
            Station station3 = new Station(3);
            Station station4 = new Station(4);
            Station station5 = new Station(5);
            Station station6 = new Station(6);
            Station station7 = new Station(7);
            Station station8 = new Station(8);
            builder.Entity<Station>().HasData(station1, station2, station3, station4, station5, station6, station7, station8);
        }
    }
}
