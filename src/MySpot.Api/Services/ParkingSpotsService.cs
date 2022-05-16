using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ParkingSpotsService
{
    private readonly IClock _clock;
    private static readonly List<ParkingSpot> ParkingSpots = new();

    public ParkingSpotsService(IClock clock)
    {
        _clock = clock;
    }

    public List<ParkingSpot> GetParkingSpots() => ParkingSpots; 
}