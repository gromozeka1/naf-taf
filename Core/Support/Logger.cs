using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Core.Support;

public static class Logger
{
    public static ILogger Log { get; } = InitLogger();

    private static ILogger InitLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .WriteTo.File("LogFile.txt")
            .CreateLogger();
    }

    public static void LogError(string? message)
    {
        Log.Error("ERROR => !**!**!**!**!**!**!**!**!**!**!**!**!", message);
        Log.Error($"         {message}");
        Log.Error("         !**!**!**!**!**!**!**!**!**!**!**!**!");
    }

    public static void TestStarted(ScenarioContext scenarioContext)
    {
        var scenarioName = scenarioContext.ScenarioInfo.Title;
        Log.Information($"--- Start test: {scenarioName} ---");
    }


    public static void TestFinished(ScenarioContext scenarioContext)
    {
        var scenarioName = scenarioContext.ScenarioInfo.Title;
        var testStatus = scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK ? "Successful" : "Failed";
        Log.Information($"--- Test {testStatus}: {scenarioName} ---");
    }

    public static async Task AttachScreenshotIfError(ScenarioContext scenarioContext)
    {
        if (scenarioContext.StepContext.Status != ScenarioExecutionStatus.OK)
        {
            var page = await scenarioContext.Get<Task<IPage>>();
            var bytes = await page.ScreenshotAsync(new PageScreenshotOptions()
            {
                FullPage = true,
                Path = Paths.Screenshots,
            });

            var screenshot = Convert.ToBase64String(bytes);
            TestContext.AddTestAttachment(screenshot);
        }
    }
}
