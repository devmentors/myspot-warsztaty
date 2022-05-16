using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots")]
public class ParkingSpotsController : ControllerBase
{
    private readonly ICommandHandler<AddParkingSpot> _addParkingSpotHandler;
    private readonly IParkingSpotsService _parkingSpotsService;

    public ParkingSpotsController(ICommandHandler<AddParkingSpot> addParkingSpotHandler,
        IParkingSpotsService parkingSpotsService)
    {
        _addParkingSpotHandler = addParkingSpotHandler;
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
        command = command with {Id = Guid.NewGuid()};
        _addParkingSpotHandler.Handle(command);
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