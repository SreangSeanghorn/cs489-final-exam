
using AstronautSatelliteAPI.DataPersistence;
using AstronautSatelliteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AstronautSatelliteAPI.Repositories;

public class SatelliteRepository : ISatelliteRepository
{
    private readonly ApplicationDbContext _context;

    public SatelliteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Satellite>> GetAllAsync()
    {
        return await _context.Satellites.Include(s => s.Astronauts).ToListAsync();
    }

    public async Task<Satellite> GetByIdAsync(long id)
    {
        return await _context.Satellites.Include(s => s.Astronauts).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Satellite satellite)
    {
        _context.Satellites.Add(satellite);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Satellite satellite)
    {
        _context.Satellites.Update(satellite);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var satellite = await _context.Satellites.FindAsync(id);
        if (satellite != null)
        {
            _context.Satellites.Remove(satellite);
            await _context.SaveChangesAsync();
        }
    }
}