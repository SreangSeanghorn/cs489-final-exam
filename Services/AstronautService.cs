using AstronautSatelliteAPI.DTOs;
using AstronautSatelliteAPI.Models;
using AstronautSatelliteAPI.DataPersistence;
using Microsoft.EntityFrameworkCore;
using AstronautSatelliteAPI.Exceptions;

namespace AstronautSatelliteAPI.Services;

public class AstronautService
{
    private readonly ApplicationDbContext _context;

    public AstronautService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AstronautDto>> GetAllAstronautsAsync(string sort, string order)
    {
        var query = _context.Astronauts.Include(a => a.Satellites).AsQueryable();

        if (!string.IsNullOrEmpty(sort))
        {
            query = sort.ToLower() switch
            {
                "experienceyears" => order.ToLower() == "desc" ? query.OrderByDescending(a => a.ExperienceYears) : query.OrderBy(a => a.ExperienceYears),
                _ => query
            };
        }

        return await query.Select(a => new AstronautDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            ExperienceYears = a.ExperienceYears,
            SatelliteIds = a.Satellites.Select(s => s.Id).ToList()
        }).ToListAsync();
    }

    public async Task<AstronautDto> GetAstronautByIdAsync(long id)
    {
        var astronaut = await _context.Astronauts.Include(a => a.Satellites).FirstOrDefaultAsync(a => a.Id == id);
        if (astronaut == null) throw new AstronautNotFoundException($"Astronaut with ID {id} not found.");

        return new AstronautDto
        {
            Id = astronaut.Id,
            FirstName = astronaut.FirstName,
            LastName = astronaut.LastName,
            ExperienceYears = astronaut.ExperienceYears,
            SatelliteIds = astronaut.Satellites.Select(s => s.Id).ToList()
        };
    }

    public async Task CreateAstronautAsync(AstronautDto astronautDto)
    {
        var satellites = await _context.Satellites.Where(s => astronautDto.SatelliteIds.Contains(s.Id)).ToListAsync();
        if (satellites.Count != astronautDto.SatelliteIds.Count)
        {
            throw new ArgumentException("One or more satellite IDs are invalid.");
        }

        var astronaut = new Astronaut
        {
            FirstName = astronautDto.FirstName,
            LastName = astronautDto.LastName,
            ExperienceYears = astronautDto.ExperienceYears,
            Satellites = satellites
        };

        _context.Astronauts.Add(astronaut);
        await _context.SaveChangesAsync();
    }
        public async Task UpdateAstronautAsync(long id, AstronautDto astronautDto)
    {
        var astronaut = await _context.Astronauts.Include(a => a.Satellites).FirstOrDefaultAsync(a => a.Id == id);
        if (astronaut == null)
        {
            throw new KeyNotFoundException($"Astronaut with ID {id} not found.");
        }
    
        // Update astronaut details
        astronaut.FirstName = astronautDto.FirstName;
        astronaut.LastName = astronautDto.LastName;
        astronaut.ExperienceYears = astronautDto.ExperienceYears;
    
        // Update satellite assignments
        var satellites = await _context.Satellites.Where(s => astronautDto.SatelliteIds.Contains(s.Id)).ToListAsync();
        if (satellites.Count != astronautDto.SatelliteIds.Count)
        {
            throw new ArgumentException("One or more satellite IDs are invalid.");
        }
        astronaut.Satellites = satellites;
    
        _context.Astronauts.Update(astronaut);
        await _context.SaveChangesAsync();
    }
public async Task DeleteAstronautAsync(long id)
{
    var astronaut = await _context.Astronauts.FindAsync(id);
    if (astronaut == null)
    {
        throw new KeyNotFoundException($"Astronaut with ID {id} not found.");
    }

    _context.Astronauts.Remove(astronaut);
    await _context.SaveChangesAsync();
}

}