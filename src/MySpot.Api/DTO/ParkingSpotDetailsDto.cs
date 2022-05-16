namespace MySpot.Api.DTO;

public class ParkingSpotDetailsDto : ParkingSpotDto
{
    public List<ReservationDto> Reservations { get; set; }
}