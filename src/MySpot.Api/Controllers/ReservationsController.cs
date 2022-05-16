using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

public class ReservationsController : ControllerBase
{
    private readonly ParkingSpotsService _parkingSpotsService = new();
}