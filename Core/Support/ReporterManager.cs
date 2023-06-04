using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Core.Config;

namespace Core.Support;

public class ReporterManager
{
    private static readonly Lazy<ExtentReports> _lazy = new(() => new ExtentReports());
    private static ExtentReports Reporter => _lazy.Value;

    private static readonly ThreadLocal<ExtentTest> _feature = new();
    private static readonly ThreadLocal<ExtentTest> _scenario = new();

    private static readonly object _synclock = new();

    static ReporterManager()
    {
        var reporter = new ExtentHtmlReporter(Paths.Reports + "/");
        reporter.Config.Theme = Theme.Standard;
        reporter.Config.DocumentTitle = "Auto-tests report";
        reporter.Config.ReportName = $"Run on {ConfigReader.ReadConfig().DriverType}";
        Reporter.AttachReporter(reporter);
    }

    private ReporterManager()
    {
    }

    public static void CreateFeature(FeatureContext featureContext)
    {
        var featureName = featureContext.FeatureInfo.Title;
        lock (_synclock)
        {
            _feature.Value = Reporter.CreateTest<Feature>(featureName);
        }
    }

    public static void AddScenario(ScenarioContext scenarioContext)
    {
        var scenarioName = scenarioContext.ScenarioInfo.Title;
        lock (_synclock)
        {
            _scenario.Value = _feature!.Value!.CreateNode<Scenario>(scenarioName);
        }
    }

    public static void AddStep(ScenarioContext scenarioContext)
    {
        lock (_synclock)
        {
            var stepType = scenarioContext.CurrentScenarioBlock.ToString();
            var gherkinStepType = new GherkinKeyword(stepType);

            var stepName = scenarioContext.StepContext.StepInfo.Text;

            var stepResult = _scenario.Value!.CreateNode(gherkinStepType, stepName);

            _ = scenarioContext.StepContext.Status switch
            {
                ScenarioExecutionStatus.OK => stepResult,
                _ => FailedStep().Result,
            };

            async Task<ExtentTest> FailedStep()
            {
                var screenshotName = scenarioContext.ScenarioInfo.Title.Trim();
                var page = await scenarioContext.Get<Task<IPage>>();
                var bytes = await page.ScreenshotAsync();
                var screenshotAsBase64EncodedString = Convert.ToBase64String(bytes);
                var screenshotCapture = MediaEntityBuilder
                        .CreateScreenCaptureFromBase64String(screenshotAsBase64EncodedString, screenshotName)
                        .Build();

                var message = scenarioContext.TestError.Message;
                
                return stepResult.Fail(message, screenshotCapture);
            }
        }
    }

    public static void Close()
    {
        Reporter.Flush();
    }
}
