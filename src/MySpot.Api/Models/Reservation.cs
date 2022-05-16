namespace MySpot.Api.Models;

public class Reservation
{
    public Guid Id { get; set; }
    public string LicensePlate { get; set; }
    public DateTime Date { get; set; }
}