using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace UI.Tests;

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

        services
            .AddScoped<IBasePage, BasePage>()
            .AddScoped<ILoginPage, LoginPage>()
            .AddScoped<IHomePage, HomePage>()
            .AddScoped<IEmployeeListPage, EmployeeListPage>()
            .AddScoped<IEmployeeCreatePage, EmployeeCreatePage>();

        return services;
    }
}
