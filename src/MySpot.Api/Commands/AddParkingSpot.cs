namespace MySpot.Api.Commands;

public record AddParkingSpot(Guid Id, string Name) : ICommand;