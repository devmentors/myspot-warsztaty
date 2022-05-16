using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Models;
using MySpot.Api.Repositories;

namespace MySpot.Api.Services;

public class ParkingSpotsService : IParkingSpotsService
{
    private readonly IParkingSpotRepository _parkingSpotRepository;
    private readonly ILogger<ParkingSpotsService> _logger;

    public ParkingSpotsService(IParkingSpotRepository parkingSpotRepository, ILogger<ParkingSpotsService> logger)
    {
        _parkingSpotRepository = parkingSpotRepository;
        _logger = logger;
    }

    public IEnumerable<ParkingSpotDto> GetParkingSpots()
        => _parkingSpotRepository.GetAll().Select(x => new ParkingSpotDto
        {
            Id = x.Id,
            Name = x.Name
        });

    public ParkingSpotDetailsDto GetParkingSpot(Guid id)
    {
        var parkingSpot = _parkingSpotRepository.Get(id);

        return parkingSpot is null
            ? null
            : new ParkingSpotDetailsDto
            {
                Id = parkingSpot.Id,
                Name = parkingSpot.Name,
                Reservations = parkingSpot.Reservations.Select(x =>
                    new ReservationDto
                    {
                        Id = x.Id,
                        Date = x.Date
                    }).ToList()
            };
    }

    public bool AddParkingSpot(AddParkingSpot command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            // "Parking spot name cannot be empty."
            return false;
        }

        var parkingSpot = new ParkingSpot
        {
            Id = command.Id,
            Name = command.Name,
            Reservations = new List<Reservation>()
        };
        _parkingSpotRepository.Add(parkingSpot);
        _logger.LogInformation("Added a parking with spot ID: {ParkingSpotId}", parkingSpot.Id);
        
        return true;
    }

    public bool DeleteParkingSpot(Guid id)
    {
        var parkingSpot = _parkingSpotRepository.Get(id);
        if (parkingSpot is null)
        {
            return false;
        }

        _parkingSpotRepository.Delete(parkingSpot);
        return true;
    }

    public bool AddReservation(Guid parkingSpotId, Reservation reservation)
    {
        var parkingSpot = _parkingSpotRepository.Get(parkingSpotId);
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
        var parkingSpot = _parkingSpotRepository.Get(parkingSpotId);
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
        _parkingSpotRepository.Update(parkingSpot);
        return true;
    }
}