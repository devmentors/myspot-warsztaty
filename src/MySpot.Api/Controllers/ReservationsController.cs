using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots/{parkingSpotId:guid}/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ParkingSpotsService _parkingSpotsService;
    private readonly IClock _clock;

    public ReservationsController(ParkingSpotsService parkingSpotsService, IClock clock)
    {
        _parkingSpotsService = parkingSpotsService;
        _clock = clock;
    }

    [HttpPost]
    public ActionResult Post(Guid parkingSpotId, Reservation reservation)
    {
        var parkingSpot = _parkingSpotsService.GetParkingSpots()
            .SingleOrDefault(x => x.Id == parkingSpotId);
        if (parkingSpot is null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(reservation.LicensePlate))
        {
            return BadRequest("Reservation license plate cannot be empty.");
        }

        if (reservation.Date.Date <= _clock.GetCurrent())
        {
            return BadRequest("Reservation date cannot be from the past.");
        }

        if (parkingSpot.Reservations.Any(x => x.Date.Date == reservation.Date.Date))
        {
            return BadRequest($"Parking spot with ID: '{parkingSpotId}' is already reserved at: {reservation.Date:d}.");
        }

        reservation.Id = Guid.NewGuid();
        parkingSpot.Reservations.Add(reservation);
        return NoContent();
    }

    [HttpDelete("{reservationId:guid}")]
    public ActionResult Delete(Guid parkingSpotId, Guid reservationId)
    {
        var parkingSpot = _parkingSpotsService.GetParkingSpots().SingleOrDefault(x => x.Id == parkingSpotId);
        if (parkingSpot is null)
        {
            return NotFound();
        }

        var reservation = parkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
        if (reservation is null)
        {
            return NotFound();
        }

        parkingSpot.Reservations.Remove(reservation);
        return NoContent();
    }
}