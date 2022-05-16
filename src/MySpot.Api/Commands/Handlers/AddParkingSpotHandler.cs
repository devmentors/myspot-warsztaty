using MySpot.Api.Exceptions;
using MySpot.Api.Models;
using MySpot.Api.Repositories;

namespace MySpot.Api.Commands.Handlers;

public class AddParkingSpotHandler : ICommandHandler<AddParkingSpot>
{
    private readonly IParkingSpotRepository _parkingSpotRepository;
    private readonly ILogger<AddParkingSpotHandler> _logger;

    public AddParkingSpotHandler(IParkingSpotRepository parkingSpotRepository,
        ILogger<AddParkingSpotHandler> logger)
    {
        _parkingSpotRepository = parkingSpotRepository;
        _logger = logger;
    }
    
    public void Handle(AddParkingSpot command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new InvalidParkingSpotNameException(command.Name);
        }

        var parkingSpot = new ParkingSpot
        {
            Id = command.Id,
            Name = command.Name,
            Reservations = new List<Reservation>()
        };
        _parkingSpotRepository.Add(parkingSpot);
        _logger.LogInformation("Added a parking with spot ID: {ParkingSpotId}", parkingSpot.Id);
    }
}