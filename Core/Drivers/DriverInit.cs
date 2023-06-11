using Core.Config;

namespace Core.Drivers;

public interface IDriverInit
{
    Task<IBrowser> GetDriverAsync(TestSettings testSettings);
}

public class DriverInit : IDriverInit
{
    public async Task<IBrowser> GetDriverAsync(TestSettings testSettings)
    {
        var playwright = await Playwright.CreateAsync();
        var driverName = GetDriverName(testSettings.DriverType);
        var options = GetParameters(testSettings);
        return await playwright[driverName].LaunchAsync(options);
    }

    private static BrowserTypeLaunchOptions GetParameters(TestSettings testSettings)
    {
        var args = testSettings.Args?.ToList() ?? new List<string>();
        if (testSettings.IsMaximized)
        {
            args?.Add(testSettings.DriverType switch
            {
                "firefox" => "--kiosk",
                "safari" => string.Empty,
                _ => "--start-maximized",
            });
        }

        return new()
        {
            Args =  args,
            Timeout = ToMilliseconds(testSettings.TimeoutInSeconds ?? DriverConstants.DEFAULT_TIMEOUT),
            Headless = testSettings.Headless,
            SlowMo = testSettings.SlowMo,
            Devtools = testSettings.DevTools,
        };
    }

    private static float? ToMilliseconds(float? seconds) => seconds * 1000;


    private static string GetDriverName(string? driverType) => driverType?.ToLower() switch
    {
        "chrome" => BrowserType.Chromium,
        "firefox" => BrowserType.Firefox,
        "safari" => BrowserType.Webkit,
        "edge" => BrowserType.Chromium,
        "opera" => BrowserType.Chromium,
        _ => BrowserType.Chromium,
    };
}
