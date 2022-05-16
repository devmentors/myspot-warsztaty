using MySpot.Api;
using MySpot.Api.Commands;
using MySpot.Api.Commands.Handlers;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logger) =>
{
    logger.WriteTo.Console();
    // logger.WriteTo.Seq("http://localhost:5341");
});

builder.Services
    .AddTransient<ICommandHandler<AddParkingSpot>, AddParkingSpotHandler>()
    .AddTransient<IParkingSpotRepository, ParkingSpotRepository>()
    .AddSingleton<IParkingSpotsService, ParkingSpotsService>()
    .AddSingleton<IClock, Clock>()
    .Configure<ApiOptions>(builder.Configuration.GetSection("api"))
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
