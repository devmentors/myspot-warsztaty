using MySpot.Api.Models;

namespace MySpot.Api.Repositories;

public interface IParkingSpotRepository
{
    IEnumerable<ParkingSpot> GetAll();
    ParkingSpot Get(Guid id);
    void Add(ParkingSpot parkingSpot);
    void Delete(ParkingSpot parkingSpot);
    void Update(ParkingSpot parkingSpot);
}