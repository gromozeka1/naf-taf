using TechTalk.SpecFlow.Assist;

namespace UI.Tests.Steps;

[Binding]
[Scope(Feature ="Login")]
public sealed class LoginSteps
{
    private readonly ScenarioContext _scenarioContext;

    private readonly ILoginPage _loginPage;
    private readonly IHomePage _homePage;

    public LoginSteps(ScenarioContext scenarioContext, ILoginPage loginPage, IHomePage homePage)
    {
        _scenarioContext = scenarioContext;

        _loginPage = loginPage;
        _homePage = homePage;
    }

    [When(@"I click login link")]
    public void WhenIClickLoginLink()
    {
        _homePage.ClickLoginButton();
    }

    [When(@"I enter login details")]
    public void WhenIEnterLoginDetails(Table table)
    {
        var user = table.CreateInstance<User>();
        _loginPage.LogIn(user.UserName!, user.Password!);
        _scenarioContext.Set(user);
    }

    [Then(@"I successfully login")]
    public async Task ThenISuccessfullyLogin()
    {
        var expectedUserName = _scenarioContext
            .Get<User>()
            .UserName!;
        
        var element = _homePage.IsUserLoggedIn(expectedUserName);
        await Assertions.Expect(element.Locator).ToBeVisibleAsync();
    }
}
