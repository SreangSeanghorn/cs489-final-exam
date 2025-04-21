using AstronautSatelliteAPI.Models;

namespace AstronautSatelliteAPI.DataPersistence;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Astronaut> Astronauts { get; set; }
    public DbSet<Satellite> Satellites { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Astronaut>()
            .HasMany(a => a.Satellites)
            .WithMany(s => s.Astronauts)
            .UsingEntity(j => j.ToTable("AstronautSatellites"));
    }
}