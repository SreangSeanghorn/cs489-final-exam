namespace AstronautSatelliteAPI.Models;

public class Astronaut
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ExperienceYears { get; set; }
    public ICollection<Satellite> Satellites { get; set; } = new List<Satellite>();
}