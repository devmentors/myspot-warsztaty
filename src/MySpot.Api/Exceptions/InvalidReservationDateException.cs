namespace MySpot.Api.Exceptions;

public class InvalidReservationDateException : CustomException
{
    public DateTime Date { get; }

    public InvalidReservationDateException(DateTime date) : base($"Reservation date: {date:d} is invalid.")
    {
        Date = date;
    }
}