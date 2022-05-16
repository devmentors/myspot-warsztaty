using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots/{parkingSpotId:guid}/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ICommandHandler<AddReservation> _addReservationHandler;
    private readonly IParkingSpotsService _parkingSpotsService;

    public ReservationsController(ICommandHandler<AddReservation> addReservationHandler,
        IParkingSpotsService parkingSpotsService)
    {
        _addReservationHandler = addReservationHandler;
        _parkingSpotsService = parkingSpotsService;
    }

    [HttpPost]
    public ActionResult Post(Guid parkingSpotId, AddReservation command)
    {
        command = command with {ReservationId = Guid.NewGuid(), ParkingSpotId = parkingSpotId};
        _addReservationHandler.Handle(command);
        return NoContent();
    }

    [HttpDelete("{reservationId:guid}")]
    public ActionResult Delete(Guid parkingSpotId, Guid reservationId)
    {
        if (_parkingSpotsService.DeleteReservation(parkingSpotId, reservationId) is false)
        {
            return NotFound();
        }

        return NoContent();
    }
}