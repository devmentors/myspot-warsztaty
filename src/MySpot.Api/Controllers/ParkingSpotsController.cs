using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
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
    public ActionResult<IEnumerable<ParkingSpotDto>> Get() => Ok(_parkingSpotsService.GetParkingSpots());


    [HttpGet("{id:guid}")]
    public ActionResult<ParkingSpotDetailsDto> Get(Guid id)
    {
        var parkingSpot = _parkingSpotsService.GetParkingSpot(id);
        if (parkingSpot is null)
        {
            return NotFound();
        }

        return parkingSpot;
    }

    [HttpPost]
    public ActionResult Post(AddParkingSpot command)
    {
        if (_parkingSpotsService.AddParkingSpot(command) is false)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
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