namespace MySpot.Api.Services;

public class Clock : IClock
{
    public Guid Id { get; } = Guid.NewGuid();
    
    public DateTime GetCurrent() => DateTime.UtcNow;
}