using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    private readonly string _apiName;

    public HomeController(IOptions<ApiOptions> apiOptions)
    {
        _apiName = apiOptions.Value.Name;
    }

    [HttpGet]
    public string Get() => _apiName;
}