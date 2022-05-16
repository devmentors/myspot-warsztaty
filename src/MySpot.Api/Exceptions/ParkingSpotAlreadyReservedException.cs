namespace MySpot.Api.Exceptions;

public class ParkingSpotAlreadyReservedException : CustomException
{
    public Guid Id { get; }
    public DateTime Date { get; }

    public ParkingSpotAlreadyReservedException(Guid id, DateTime date) 
        : base($"Parking spot with ID: {id} is already reserved at: {date:d}.")
    {
        Id = id;
        Date = date;
    }
}