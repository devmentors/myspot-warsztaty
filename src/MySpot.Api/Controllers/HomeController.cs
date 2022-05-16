using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public string Get() => "Hello world";
}