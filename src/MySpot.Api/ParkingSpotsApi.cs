using MySpot.Api.Commands;
using MySpot.Api.Services;

namespace MySpot.Api;

public static class ParkingSpotsApi
{
    public static WebApplication MapParkingSpotsApi(this WebApplication app)
    {
        app.MapGet("/api/parking-spots/{id:guid}", (Guid id, IParkingSpotsService parkingSpotsService) =>
        {
            var parkingSpot = parkingSpotsService.GetParkingSpot(id);
            return parkingSpot is null ? Results.NotFound() : Results.Ok(parkingSpot);
        }).WithName("GetParkingSpot").WithDisplayName("Get parking spot");

        app.MapPost("/api/parking-spots", (AddParkingSpot command,
            ICommandHandler<AddParkingSpot> addParkingSpotHandler) =>
        {
            command = command with {Id = Guid.NewGuid()};
            addParkingSpotHandler.Handle(command);
            return Results.CreatedAtRoute("GetParkingSpot", new {id = command.Id});
        });
        
        return app;
    }
}