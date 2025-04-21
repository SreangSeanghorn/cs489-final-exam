namespace AstronautSatelliteAPI.Controllers;

using AstronautSatelliteAPI.DataPersistence;
using AstronautSatelliteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class SatellitesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SatellitesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSatellites()
    {
        var satellites = await _context.Satellites.Include(s => s.Astronauts).ToListAsync();
        return Ok(satellites);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSatellite(int id)
    {
        var satellite = await _context.Satellites.Include(s => s.Astronauts).FirstOrDefaultAsync(s => s.Id == id);
        if (satellite == null) return NotFound();
        return Ok(satellite);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSatellite(Satellite satellite)
    {
        _context.Satellites.Add(satellite);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSatellite), new { id = satellite.Id }, satellite);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSatellite(int id, Satellite satellite)
    {
        if (id != satellite.Id) return BadRequest();
        _context.Entry(satellite).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSatellite(int id)
    {
        var satellite = await _context.Satellites.FindAsync(id);
        if (satellite == null) return NotFound();
        _context.Satellites.Remove(satellite);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}