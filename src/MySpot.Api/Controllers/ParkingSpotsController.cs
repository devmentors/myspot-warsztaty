using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots")]
public class ParkingSpotsController : ControllerBase
{
    private readonly IParkingSpotsService _parkingSpotsService;

    public ParkingSpotsController(IParkingSpotsService parkingSpotsService)
    {
        _parkingSpotsService = parkingSpotsService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ParkingSpot>> Get() => Ok(_parkingSpotsService.GetParkingSpots());


    [HttpGet("{id:guid}")]
    public ActionResult<ParkingSpot> Get(Guid id)
    {
        var parkingSpot = _parkingSpotsService.GetParkingSpot(id);
        if (parkingSpot is null)
        {
            return NotFound();
        }

        return parkingSpot;
    }

    [HttpPost]
    public ActionResult Post(ParkingSpot parkingSpot)
    {
        if (_parkingSpotsService.AddParkingSpot(parkingSpot) is false)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(Get), new {id = parkingSpot.Id}, null);
    }
    
    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        if (_parkingSpotsService.DeleteParkingSpot(id) is false)
        {
            return NotFound();
        }

        return NoContent();
    }
}