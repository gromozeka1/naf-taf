# NAF TAF

## **Description**

Test Automation Framework skeleton provides the abitility to automate UI and API tests. The framework is based on BDD (SpecFlow) approach in connection with Playwright.

---

## **Technologies**
- [] [.NET 7.0](https://learn.microsoft.com/en-gb/dotnet/core/whats-new/dotnet-7?WT.mc_id=dotnet-35129-website)
- [] [Playwright](https://playwright.dev/dotnet/docs/intro)
- [] [SpecFlow](https://docs.specflow.org/projects/getting-started/en/latest/GettingStarted/Step1.html)
- [] [NUnit3](https://docs.nunit.org/articles/nunit/intro.html)
- [] [Extent report](https://www.extentreports.com/docs/versions/4/net/index.html)
- [] [Serilog](https://github.com/serilog/serilog/wiki/Getting-Started)
- [] [Bogus](https://github.com/bchavez/Bogus)

---

## **Browser support**
- [] **Chrome** (by default)
- [] **Firefox**
- [] **Safari**
- [] **Edge**

Note: headless mode is available for UI testing.

---

## **Modules**

**NAF TAF** main modules are:

- [] **API.Tests** – project where API Tests (including Features, Steps implementations and Models) are located.
- [] **Core** – project with main classes to work with Browsers, Web Elements, Logs, Configurations, etc.
- [] **UI.PageObject** – project implemented Page Object pattern for testing UI part of web-services.
- [] **UiSteps** - project where UI Test Steps implementations are located.
- [] **UiTests** - project where UI Tests (including Features and Steps implementations) are located.

---

## **How to run tests**

### **Run from the command line**
1. Open command line
2. Navigate to the directory containing the *.sln
3. Run the following command

```powershell
dotnet build --configuration {QA/Local/etc}
dotnet test --filter "TestCategory={Tag}"
```
Where `{Tag}` can be *Smoke*, *API* or any other that you may specify in Features files.

---

## **Parallel tests execution**

The framework set up a parallel execution using assembly-level attributes ```[Parallelizable]``` and ```[LevelOfParallelism]```.
```
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
```
NUnit framework supports only Fixtures as ParallelScope. That means parallelization will be on Features level.

```
[Parallelizable(ParallelScope.Fixtures)]
```

To set up level of parallelism change the number for ```[LevelOfParallelism]``` attribute.

## **Test Result Report**

For test result reporting in NAF TAF **Extent Report** is used.

Reports placed under `Report` folder in root of the UI.Tests and API.Tests projects.

### **Screenshots**

Screenshots are captured for failed UI tests and stored in `Screenshots` folder in root of the UI.Tests project.

---

## **Configurations**

### **appsettings.json files**

Parameters for UI and API tests are stored in appsettings.json files in UI.Tests and API.Tests projects accordingly.

---

## **Logging**

For logging in NAF TAF **Serilog** is used.

**LogFile.txt** is generated after each test execution and placed under `/bin/{build}/net7.0` directory.
