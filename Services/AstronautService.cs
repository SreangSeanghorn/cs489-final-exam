
using AstronautSatelliteAPI.DTOs;
using AstronautSatelliteAPI.Models;
using AstronautSatelliteAPI.Repositories;

namespace AstronautSatelliteAPI.Services;

public class AstronautService
{
    private readonly IAstronautRepository _astronautRepository;
    private readonly ISatelliteRepository _satelliteRepository;

    public AstronautService(IAstronautRepository astronautRepository, ISatelliteRepository satelliteRepository)
    {
        _astronautRepository = astronautRepository;
        _satelliteRepository = satelliteRepository;
    }

    public async Task<IEnumerable<AstronautDto>> GetAllAstronautsAsync(string sort, string order)
    {
        var astronauts = await _astronautRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(sort))
        {
            astronauts = sort.ToLower() switch
            {
                "experienceyears" => order.ToLower() == "desc" ? astronauts.OrderByDescending(a => a.ExperienceYears) : astronauts.OrderBy(a => a.ExperienceYears),
                _ => astronauts
            };
        }

        return astronauts.Select(a => new AstronautDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            ExperienceYears = a.ExperienceYears,
            SatelliteIds = a.Satellites.Select(s => s.Id).ToList()
        });
    }

    public async Task<AstronautDto> GetAstronautByIdAsync(long id)
    {
        var astronaut = await _astronautRepository.GetByIdAsync(id);
        if (astronaut == null) throw new KeyNotFoundException($"Astronaut with ID {id} not found.");

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
        var satellites = await _satelliteRepository.GetAllAsync();
        var selectedSatellites = satellites.Where(s => astronautDto.SatelliteIds.Contains(s.Id)).ToList();

        if (selectedSatellites.Count != astronautDto.SatelliteIds.Count)
        {
            throw new ArgumentException("One or more satellite IDs are invalid.");
        }

        var astronaut = new Astronaut
        {
            FirstName = astronautDto.FirstName,
            LastName = astronautDto.LastName,
            ExperienceYears = astronautDto.ExperienceYears,
            Satellites = selectedSatellites
        };

        await _astronautRepository.AddAsync(astronaut);
    }

    public async Task UpdateAstronautAsync(long id, AstronautDto astronautDto)
    {
        var astronaut = await _astronautRepository.GetByIdAsync(id);
        if (astronaut == null) throw new KeyNotFoundException($"Astronaut with ID {id} not found.");

        var satellites = await _satelliteRepository.GetAllAsync();
        var selectedSatellites = satellites.Where(s => astronautDto.SatelliteIds.Contains(s.Id)).ToList();

        if (selectedSatellites.Count != astronautDto.SatelliteIds.Count)
        {
            throw new ArgumentException("One or more satellite IDs are invalid.");
        }

        astronaut.FirstName = astronautDto.FirstName;
        astronaut.LastName = astronautDto.LastName;
        astronaut.ExperienceYears = astronautDto.ExperienceYears;
        astronaut.Satellites = selectedSatellites;

        await _astronautRepository.UpdateAsync(astronaut);
    }

        public async Task DeleteAstronautAsync(long id)
    {
        var astronaut = await _astronautRepository.GetByIdAsync(id);
        if (astronaut == null)
        {
            throw new KeyNotFoundException($"Astronaut with ID {id} not found.");
        }
    
        await _astronautRepository.DeleteAsync(id);
    }
}