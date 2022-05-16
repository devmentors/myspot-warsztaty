using MySpot.Api.Exceptions;
using MySpot.Api.Models;
using MySpot.Api.Repositories;

namespace MySpot.Api.Commands.Handlers;

public class AddReservationHandler : ICommandHandler<AddReservation>
{
    private readonly IParkingSpotRepository _parkingSpotRepository;
    private readonly ILogger<AddReservation> _logger;

    public AddReservationHandler(IParkingSpotRepository parkingSpotRepository, ILogger<AddReservation> logger)
    {
        _parkingSpotRepository = parkingSpotRepository;
        _logger = logger;
    }

    public void Handle(AddReservation command)
    {
        var parkingSpot = _parkingSpotRepository.Get(command.ParkingSpotId);
        if (parkingSpot is null)
        {
            throw new ParkingSpotNotFoundException(command.ParkingSpotId);
        }

        if (string.IsNullOrWhiteSpace(command.LicensePlate))
        {
            throw new InvalidLicensePlateException();
        }

        if (command.Date.Date <= DateTime.UtcNow.Date)
        {
            throw new InvalidReservationDateException(command.Date);
        }

        if (parkingSpot.Reservations.Any(x => x.Date.Date == command.Date.Date))
        {
            throw new ParkingSpotAlreadyReservedException(command.ParkingSpotId, command.Date);
        }

        parkingSpot.Reservations.Add(new Reservation
        {
            Id = command.ReservationId,
            LicensePlate = command.LicensePlate,
            Date = command.Date
        });
        _parkingSpotRepository.Update(parkingSpot);
        _logger.LogInformation($"Reserved parking spot: '{parkingSpot.Name}' for date: {command.Date:d}.");
    }
}