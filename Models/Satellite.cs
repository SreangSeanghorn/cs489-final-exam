namespace AstronautSatelliteAPI.Models;

public class Satellite
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Astronaut> Astronauts { get; set; } = new List<Astronaut>();
}