namespace UI.Tests.Steps;

[Binding]
public class ReusableSteps
{
    private readonly IBasePage _basePage;
    private readonly IHomePage _homePage;
    private readonly ILoginPage _loginPage;

    public ReusableSteps(IHomePage homePage, ILoginPage loginPage, IBasePage basePage)
    {
        _homePage = homePage;
        _loginPage = loginPage;
        _basePage = basePage;
    }

    [Given(@"I logged in")]
    public async Task GivenILoggedIn()
    {
        _homePage.ClickLoginButton();
        var user = new User
        {
            UserName = "admin",
            Password = "password"
        };

        _loginPage.LogIn(user.UserName, user.Password);

        var element = _homePage.IsUserLoggedIn(user.UserName);
        await Assertions.Expect(element.Locator).ToBeVisibleAsync();
    }

    [When(@"I go to '([^']*)' page")]
    public void WhenIGoToPage(string page)
    {
        _basePage.GoToPage(page);
    }

    [Then(@"I see '([^']*)' validation error")]
    public async Task ThenISeeValidationErrorAsync(string expectedError)
    {
        if (expectedError == "no")
        {
            Assert.That(_basePage.HasValidationError(), Is.False);
        }
        else
        {
            Assert.That(_basePage.HasValidationError(), Is.True);

            var element = _basePage.ValidationError(expectedError);
            await Assertions.Expect(element.Locator).ToBeVisibleAsync();
        }
    }
}
