using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace API.Tests;

public class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        services
            .AddSingleton(ConfigReader.ReadConfig())
            .AddScoped<IDriver, Driver>()
            .AddScoped<IDriverInit, DriverInit>();

        return services;
    }
}
