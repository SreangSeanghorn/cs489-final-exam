using AstronautSatelliteAPI.Models;

namespace AstronautSatelliteAPI.Repositories;

public interface IAstronautRepository
{
    Task<IEnumerable<Astronaut>> GetAllAsync();
    Task<Astronaut> GetByIdAsync(long id);
    Task AddAsync(Astronaut astronaut);
    Task UpdateAsync(Astronaut astronaut);
    Task DeleteAsync(long id);
}