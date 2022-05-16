namespace MySpot.Api.Commands;

public record AddReservation(Guid ParkingSpotId, Guid ReservationId, string LicensePlate, DateTime Date) : ICommand;