using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ParkingSpotsService : IParkingSpotsService
{
    private readonly List<ParkingSpot> _parkingSpots = new();

    public IEnumerable<ParkingSpot> GetParkingSpots() => _parkingSpots;

    public ParkingSpot GetParkingSpot(Guid id) => _parkingSpots.SingleOrDefault(x => x.Id == id);

    public bool AddParkingSpot(ParkingSpot parkingSpot)
    {
        if (string.IsNullOrWhiteSpace(parkingSpot.Name))
        {
            // "Parking spot name cannot be empty."
            return false;
        }

        parkingSpot.Id = Guid.NewGuid();
        parkingSpot.Reservations = new List<Reservation>();
        _parkingSpots.Add(parkingSpot);
        return true;
    }

    public bool DeleteParkingSpot(Guid id)
    {
        var parkingSpot = _parkingSpots.SingleOrDefault(x => x.Id == id);
        if (parkingSpot is null)
        {
            return false;
        }

        _parkingSpots.Remove(parkingSpot);
        return true;
    }

    public bool AddReservation(Guid parkingSpotId, Reservation reservation)
    {
        var parkingSpot = _parkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        if (parkingSpot is null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(reservation.LicensePlate))
        {
            // "Reservation license plate cannot be empty."
            return false;
        }

        if (reservation.Date.Date <= DateTime.UtcNow.Date)
        {
            // "Reservation date cannot be from the past."
            return false;
        }

        if (parkingSpot.Reservations.Any(x => x.Date.Date == reservation.Date.Date))
        {
            // $"Parking spot with ID: '{parkingSpotId}' is already reserved at: {reservation.Date:d}."
            return false;
        }

        reservation.Id = Guid.NewGuid();
        parkingSpot.Reservations.Add(reservation);
        return true;
    }

    public bool DeleteReservation(Guid parkingSpotId, Guid reservationId)
    {
        var parkingSpot = _parkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        if (parkingSpot is null)
        {
            return false;
        }

        var reservation = parkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
        if (reservation is null)
        {
            return false;
        }

        parkingSpot.Reservations.Remove(reservation);
        return true;
    }
}