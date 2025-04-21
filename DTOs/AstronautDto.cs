using System.ComponentModel.DataAnnotations;

namespace AstronautSatelliteAPI.DTOs;

public class AstronautDto
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

    public ICollection<long> SatelliteIds { get; set; } = new List<long>();
}