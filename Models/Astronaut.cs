using System.ComponentModel.DataAnnotations;

namespace AstronautSatelliteAPI.Models;

public class Astronaut
{
    public long Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string LastName { get; set; }

    [Range(0, 50)]
    public int ExperienceYears { get; set; }

    public ICollection<Satellite> Satellites { get; set; } = new List<Satellite>();
}