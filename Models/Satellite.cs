using System.ComponentModel.DataAnnotations;
using AstronautSatelliteAPI.Validation;

namespace AstronautSatelliteAPI.Models;

public class Satellite
{
    public long Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [PastDate] 
    public DateTime LaunchDate { get; set; }

    [Required]
    [RegularExpression("^(LEO|MEO|GEO)$", ErrorMessage = "OrbitType must be one of: LEO, MEO, GEO.")]
    public string OrbitType { get; set; }

    public bool Decommissioned { get; set; }

    public ICollection<Astronaut> Astronauts { get; set; } = new List<Astronaut>();
}