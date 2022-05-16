using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IParkingSpotsService, ParkingSpotsService>()
    .AddSingleton<IClock, Clock>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
