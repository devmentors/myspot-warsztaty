using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ParkingSpotsService
{
    private static readonly List<ParkingSpot> ParkingSpots = new();

    public List<ParkingSpot> GetParkingSpots() => ParkingSpots; 
}