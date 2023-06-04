using Core.Support;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]

namespace UI.Tests.Steps;

[Binding]
public class Hooks
{
    private readonly TestSettings _testSettings;
    private readonly Task<IPage> _page;

    public Hooks(IDriver driver, TestSettings testSettings)
    {
        _testSettings = testSettings;
        _page = driver.Page;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        DirectoryHelper.SetUpTempFolders();
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        ReporterManager.CreateFeature(featureContext);
    }

    [BeforeScenario]
    public async Task OpenApplicationUrl(ScenarioContext scenarioContext)
    {
        ReporterManager.AddScenario(scenarioContext);
        Logger.TestStarted(scenarioContext);
        scenarioContext.Set(_page);

        await (await _page).GotoAsync(_testSettings.BaseUiUrl!);
    }

    [AfterStep]
    public static void AfterStep(ScenarioContext scenarioContext)
    {
        ReporterManager.AddStep(scenarioContext);

        ScreenshotManager.AttachScreenshotIfError(scenarioContext).GetAwaiter();
    }

    [AfterScenario]
    public static void AfterScenario(ScenarioContext scenarioContext)
    {
        Logger.TestFinished(scenarioContext);
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        ReporterManager.Close();

        DirectoryHelper.ClearUpTempFolder();
    }
}
