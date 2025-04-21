using AstronautSatelliteAPI.DTOs;
using AstronautSatelliteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AstronautSatelliteAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AstronautsController : ControllerBase
{
    private readonly AstronautService _astronautService;

    public AstronautsController(AstronautService astronautService)
    {
        _astronautService = astronautService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAstronauts([FromQuery] string sort = null, [FromQuery] string order = "asc")
    {
        try
        {
            var astronauts = await _astronautService.GetAllAstronautsAsync(sort, order);
            return Ok(astronauts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving astronauts.", details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAstronaut(long id)
    {
        try
        {
            var astronaut = await _astronautService.GetAstronautByIdAsync(id);
            return Ok(astronaut);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the astronaut.", details = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAstronaut([FromBody] AstronautDto astronautDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _astronautService.CreateAstronautAsync(astronautDto);
            return CreatedAtAction(nameof(GetAstronaut), new { id = astronautDto.Id }, astronautDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the astronaut.", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAstronaut(long id, [FromBody] AstronautDto astronautDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != astronautDto.Id) return BadRequest(new { message = "ID mismatch." });

        try
        {
            await _astronautService.UpdateAstronautAsync(id, astronautDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the astronaut.", details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAstronaut(long id)
    {
        try
        {
            await _astronautService.DeleteAstronautAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the astronaut.", details = ex.Message });
        }
    }
}