using MySpot.Api;
using MySpot.Api.Commands;
using MySpot.Api.Middlewares;
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
    .Scan(scan => scan.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime())
    // .AddTransient<ICommandHandler<AddParkingSpot>, AddParkingSpotHandler>()
    // .AddTransient<ICommandHandler<AddReservation>, AddReservationHandler>()
    .AddTransient<IParkingSpotRepository, ParkingSpotRepository>()
    .AddSingleton<IParkingSpotsService, ParkingSpotsService>()
    .AddSingleton<IClock, Clock>()
    .AddSingleton<ErrorHandlerMiddleware>()
    .AddSingleton<LoggingMiddleware>()
    .Configure<ApiOptions>(builder.Configuration.GetSection("api"))
    .AddControllers();

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
