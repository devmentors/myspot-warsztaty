using MySpot.Api.Models;

namespace MySpot.Api.Services;

public interface IParkingSpotsService
{
    IEnumerable<ParkingSpot> GetParkingSpots();
    ParkingSpot GetParkingSpot(Guid id);
    bool AddParkingSpot(ParkingSpot parkingSpot);
    bool DeleteParkingSpot(Guid id);
    bool AddReservation(Guid parkingSpotId, Reservation reservation);
    bool DeleteReservation(Guid parkingSpotId, Guid reservationId);
}