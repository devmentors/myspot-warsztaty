using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots")]
public class ParkingSpotsController : ControllerBase
{
    private readonly ParkingSpotsService _parkingSpotsService = new();

    [HttpGet]
    public ActionResult<IEnumerable<ParkingSpot>> Get()
        => _parkingSpotsService.GetParkingSpots();


    [HttpGet("{id:guid}")]
    public ActionResult<ParkingSpot> Get(Guid id)
    {
        var parkingSpot = _parkingSpotsService.GetParkingSpots()
            .SingleOrDefault(x => x.Id == id);
        if (parkingSpot is null)
        {
            return NotFound();
        }

        return parkingSpot;
    }

    [HttpPost]
    public ActionResult Post(ParkingSpot parkingSpot)
    {
        if (string.IsNullOrWhiteSpace(parkingSpot.Name))
        {
            return BadRequest("Parking spot name cannot be empty.");
        }

        parkingSpot.Id = Guid.NewGuid();
        parkingSpot.Reservations = new List<Reservation>();
        _parkingSpotsService.GetParkingSpots().Add(parkingSpot);
        return CreatedAtAction(nameof(Get), new {id = parkingSpot.Id}, null);
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        var parkingSpot = _parkingSpotsService.GetParkingSpots()
            .SingleOrDefault(x => x.Id == id);
        if (parkingSpot is null)
        {
            return NotFound();
        }

        _parkingSpotsService.GetParkingSpots().Remove(parkingSpot);
        return NoContent();
    }
}