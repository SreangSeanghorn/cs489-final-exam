namespace AstronautSatelliteAPI.Controllers;

using AstronautSatelliteAPI.DataPersistence;
using AstronautSatelliteAPI.DTOs;
using AstronautSatelliteAPI.Models;
using AstronautSatelliteAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/v1/[controller]")]
public class SatellitesController : ControllerBase
{
    private readonly SatelliteService _satelliteService;

    public SatellitesController(SatelliteService satelliteService)
    {
        _satelliteService = satelliteService;
    }
        [HttpGet]
    public async Task<IActionResult> GetSatellites()
    {
        var satellites = await _satelliteService.GetAllSatellitesAsync();
        return Ok(satellites);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSatellite(long id)
    {
        try
        {
            var satellite = await _satelliteService.GetSatelliteByIdAsync(id);
            return Ok(satellite);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSatellite([FromBody] SatelliteDto satelliteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
    
        try
        {
            await _satelliteService.CreateSatelliteAsync(satelliteDto);
            return CreatedAtAction(nameof(GetSatellite), new { id = satelliteDto.Id }, satelliteDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the satellite.", details = ex.Message });
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSatellite(long id, [FromBody] SatelliteDto satelliteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
    
        try
        {
            await _satelliteService.UpdateSatelliteAsync(id, satelliteDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the satellite.", details = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSatellite(long id)
    {
        try
        {
            await _satelliteService.DeleteSatelliteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the satellite.", details = ex.Message });
        }
    }
    
}