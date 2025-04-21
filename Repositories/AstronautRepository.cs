
using AstronautSatelliteAPI.DataPersistence;
using AstronautSatelliteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AstronautSatelliteAPI.Repositories;

public class AstronautRepository : IAstronautRepository
{
    private readonly ApplicationDbContext _context;

    public AstronautRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Astronaut>> GetAllAsync()
    {
        return await _context.Astronauts.Include(a => a.Satellites).ToListAsync();
    }

    public async Task<Astronaut> GetByIdAsync(long id)
    {
        return await _context.Astronauts.Include(a => a.Satellites).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Astronaut astronaut)
    {
        _context.Astronauts.Add(astronaut);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Astronaut astronaut)
    {
        _context.Astronauts.Update(astronaut);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var astronaut = await _context.Astronauts.FindAsync(id);
        if (astronaut != null)
        {
            _context.Astronauts.Remove(astronaut);
            await _context.SaveChangesAsync();
        }
    }
}