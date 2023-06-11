using Core.Support;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]

namespace API.Tests.Steps
{
    [Binding]
    public class Hooks
    {
        public Hooks()
        {
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
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            ReporterManager.AddScenario(scenarioContext);

            Logger.TestStarted(scenarioContext);
        }

        [AfterStep]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            ReporterManager.AddStep(scenarioContext);
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
}
