using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Models;

namespace MySpot.Api.Services;

public interface IParkingSpotsService
{
    IEnumerable<ParkingSpotDto> GetParkingSpots();
    ParkingSpotDetailsDto GetParkingSpot(Guid id);
    bool AddParkingSpot(AddParkingSpot command);
    bool DeleteParkingSpot(Guid id);
    bool AddReservation(Guid parkingSpotId, Reservation reservation);
    bool DeleteReservation(Guid parkingSpotId, Guid reservationId);
}