using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots/{parkingSpotId:guid}/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IParkingSpotsService _parkingSpotsService;

    public ReservationsController(IParkingSpotsService parkingSpotsService)
    {
        _parkingSpotsService = parkingSpotsService;
    }

    [HttpPost]
    public ActionResult Post(Guid parkingSpotId, Reservation reservation)
    {
        if (_parkingSpotsService.AddReservation(parkingSpotId, reservation) is false)
        {
            return BadRequest();
        }

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