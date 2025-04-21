using System.ComponentModel.DataAnnotations;

namespace AstronautSatelliteAPI.DTOs;

public class SatelliteDto
{
    public long Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime LaunchDate { get; set; }

    [Required]
    [RegularExpression("^(LEO|MEO|GEO)$", ErrorMessage = "OrbitType must be one of: LEO, MEO, GEO.")]
    public string OrbitType { get; set; }

    public bool Decommissioned { get; set; }
}