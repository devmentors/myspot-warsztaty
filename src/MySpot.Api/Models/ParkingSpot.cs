namespace MySpot.Api.Models;

public class ParkingSpot
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Reservation> Reservations { get; set; }
}