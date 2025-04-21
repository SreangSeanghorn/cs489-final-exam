using AstronautSatelliteAPI.DataPersistence;
using AstronautSatelliteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AstronautSatelliteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstronautsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AstronautsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAstronauts()
    {
        var astronauts = await _context.Astronauts.Include(a => a.Satellites).ToListAsync();
        return Ok(astronauts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAstronaut(int id)
    {
        var astronaut = await _context.Astronauts.Include(a => a.Satellites).FirstOrDefaultAsync(a => a.Id == id);
        if (astronaut == null) return NotFound();
        return Ok(astronaut);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAstronaut(int id)
    {
        var astronaut = await _context.Astronauts.FindAsync(id);
        if (astronaut == null) return NotFound();
        _context.Astronauts.Remove(astronaut);
        await _context.SaveChangesAsync();
        return NoContent();
    }
        [HttpPost]
    public async Task<IActionResult> CreateAstronaut([FromBody] Astronaut astronaut)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
    
        _context.Astronauts.Add(astronaut);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAstronaut), new { id = astronaut.Id }, astronaut);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAstronaut(int id, [FromBody] Astronaut astronaut)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != astronaut.Id) return BadRequest();
    
        _context.Entry(astronaut).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}