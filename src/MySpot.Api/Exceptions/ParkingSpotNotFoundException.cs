namespace MySpot.Api.Exceptions;

public class ParkingSpotNotFoundException : CustomException
{
    public Guid Id { get; }

    public ParkingSpotNotFoundException(Guid id) : base($"Parking spot with ID: '{id}' was not found.")
    {
        Id = id;
    }
}