
using AstronautSatelliteAPI.DataPersistence;
using AstronautSatelliteAPI.DTOs;
using AstronautSatelliteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AstronautSatelliteAPI.Services;

public class SatelliteService
{
    private readonly ApplicationDbContext _context;

    public SatelliteService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all satellites with their assigned astronauts
    public async Task<IEnumerable<SatelliteDto>> GetAllSatellitesAsync()
    {
        return await _context.Satellites
            .Include(s => s.Astronauts)
            .Select(s => new SatelliteDto
            {
                Id = s.Id,
                Name = s.Name,
                LaunchDate = s.LaunchDate,
                OrbitType = s.OrbitType,
                Decommissioned = s.Decommissioned,
                AstronautIds = s.Astronauts.Select(a => a.Id).ToList()
            })
            .ToListAsync();
    }

    // Get a single satellite by ID with its assigned astronauts
    public async Task<SatelliteDto> GetSatelliteByIdAsync(long id)
    {
        var satellite = await _context.Satellites
            .Include(s => s.Astronauts)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (satellite == null)
        {
            throw new KeyNotFoundException($"Satellite with ID {id} not found.");
        }

        return new SatelliteDto
        {
            Id = satellite.Id,
            Name = satellite.Name,
            LaunchDate = satellite.LaunchDate,
            OrbitType = satellite.OrbitType,
            Decommissioned = satellite.Decommissioned,
            AstronautIds = satellite.Astronauts.Select(a => a.Id).ToList()
        };
    }

    // Create a new satellite
    public async Task CreateSatelliteAsync(SatelliteDto satelliteDto)
    {
        // Validate that the launch date is in the past
        if (satelliteDto.LaunchDate >= DateTime.Now)
        {
            throw new ArgumentException("Launch date must be in the past.");
        }

        var satellite = new Satellite
        {
            Name = satelliteDto.Name,
            LaunchDate = satelliteDto.LaunchDate,
            OrbitType = satelliteDto.OrbitType,
            Decommissioned = satelliteDto.Decommissioned
        };

        _context.Satellites.Add(satellite);
        await _context.SaveChangesAsync();
    }

    // Update an existing satellite
    public async Task UpdateSatelliteAsync(long id, SatelliteDto satelliteDto)
    {
        var satellite = await _context.Satellites.Include(s => s.Astronauts).FirstOrDefaultAsync(s => s.Id == id);
        if (satellite == null)
        {
            throw new KeyNotFoundException($"Satellite with ID {id} not found.");
        }

        // Prevent updates if the satellite is decommissioned
        if (satellite.Decommissioned)
        {
            throw new InvalidOperationException("Cannot update a decommissioned satellite.");
        }

        // Update satellite details
        satellite.Name = satelliteDto.Name;
        satellite.LaunchDate = satelliteDto.LaunchDate;
        satellite.OrbitType = satelliteDto.OrbitType;
        satellite.Decommissioned = satelliteDto.Decommissioned;

        _context.Satellites.Update(satellite);
        await _context.SaveChangesAsync();
    }

    // Delete a satellite
    public async Task DeleteSatelliteAsync(long id)
    {
        var satellite = await _context.Satellites.FindAsync(id);
        if (satellite == null)
        {
            throw new KeyNotFoundException($"Satellite with ID {id} not found.");
        }

        _context.Satellites.Remove(satellite);
        await _context.SaveChangesAsync();
    }
}