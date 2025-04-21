using System;

namespace AstronautSatelliteAPI.Exceptions;

public class AstronautNotFoundException : Exception
{
    public AstronautNotFoundException(string message) : base(message) { }
}
