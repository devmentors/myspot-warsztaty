using Microsoft.Extensions.DependencyInjection;
using MySpot.Api.Services;
using Xunit;

namespace MySpot.Tests;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IClock, Clock>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var clock1 = scope.ServiceProvider.GetService<IClock>();
            var clock2 = scope.ServiceProvider.GetService<IClock>();
        }

        using (var scope = serviceProvider.CreateScope())
        {
            var clock1 = scope.ServiceProvider.GetService<IClock>();
            var clock2 = scope.ServiceProvider.GetService<IClock>();
        }
    }
}