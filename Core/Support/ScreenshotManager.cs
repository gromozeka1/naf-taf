namespace Core.Support;

public static class ScreenshotManager
{
    private static int _counter = 0;
    public static async Task AttachScreenshotIfError(ScenarioContext scenarioContext)
    {
        if (scenarioContext.StepContext.Status == ScenarioExecutionStatus.OK)
        {
            return;
        }

        var fileName = $"{DateTime.Now:yyyyMMdd____hhmmss_}{++_counter}.png";

        var fullPath = Path.GetFullPath(fileName, Paths.Screenshots);
        var page = await scenarioContext.Get<Task<IPage>>();
        _ = await page.ScreenshotAsync(new PageScreenshotOptions()
        {
            FullPage = true,
            Path = fullPath,
        });

        TestContext.AddTestAttachment(fullPath);
    }
}
