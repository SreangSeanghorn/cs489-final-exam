
using AstronautSatelliteAPI.Models;

namespace AstronautSatelliteAPI.Repositories;

public interface ISatelliteRepository
{
    Task<IEnumerable<Satellite>> GetAllAsync();
    Task<Satellite> GetByIdAsync(long id);
    Task AddAsync(Satellite satellite);
    Task UpdateAsync(Satellite satellite);
    Task DeleteAsync(long id);
}