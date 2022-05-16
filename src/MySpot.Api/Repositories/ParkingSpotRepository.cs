using MySpot.Api.Models;

namespace MySpot.Api.Repositories;

public class ParkingSpotRepository : IParkingSpotRepository
{
    private static readonly List<ParkingSpot> ParkingSpots = new();

    public IEnumerable<ParkingSpot> GetAll() => ParkingSpots;

    public ParkingSpot Get(Guid id) => ParkingSpots.SingleOrDefault(x => x.Id == id);

    public void Add(ParkingSpot parkingSpot) => ParkingSpots.Add(parkingSpot);

    public void Delete(ParkingSpot parkingSpot) => ParkingSpots.Remove(parkingSpot);

    public void Update(ParkingSpot parkingSpot)
    {
    }
}