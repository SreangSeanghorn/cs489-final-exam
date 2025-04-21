using AstronautSatelliteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AstronautSatelliteAPI.DataPersistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Astronaut> Astronauts { get; set; }
    public DbSet<Satellite> Satellites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Satellites
        modelBuilder.Entity<Satellite>().HasData(
            new Satellite
            {
                Id = 1,
                Name = "Hubble",
                LaunchDate = new DateTime(1990, 4, 24),
                OrbitType = "LEO",
                Decommissioned = false
            },
            new Satellite
            {
                Id = 2,
                Name = "Starlink-17",
                LaunchDate = new DateTime(2023, 8, 14),
                OrbitType = "MEO",
                Decommissioned = false
            },
            new Satellite
            {
                Id = 3,
                Name = "Sentinel-6",
                LaunchDate = new DateTime(2020, 11, 21),
                OrbitType = "LEO",
                Decommissioned = true
            }
        );

        // Seed Astronauts
        modelBuilder.Entity<Astronaut>().HasData(
            new Astronaut
            {
                Id = 1,
                FirstName = "Neil",
                LastName = "Armstrong",
                ExperienceYears = 12
            },
            new Astronaut
            {
                Id = 2,
                FirstName = "Sally",
                LastName = "Ride",
                ExperienceYears = 8
            },
            new Astronaut
            {
                Id = 3,
                FirstName = "Chris",
                LastName = "Hadfield",
                ExperienceYears = 15
            }
        );
    }
}