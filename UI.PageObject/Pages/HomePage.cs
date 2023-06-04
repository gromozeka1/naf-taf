using UI.PageObject.Elements;

namespace UI.PageObject.Pages;

public interface IHomePage
{
    void ClickLoginButton();
    Element IsUserLoggedIn(string userName);
}

public class HomePage : BasePage, IHomePage
{
    private readonly Button _loginButton;

    public HomePage(IDriver driver) : base(driver) 
        => _loginButton = Find<Button>("text=Login");

    public void ClickLoginButton() => _loginButton.Click();

    public Element IsUserLoggedIn(string userName)
    {
        var expectedText = $"Hello {userName}!";
        return FindByText<Element>(expectedText, new PageGetByTextOptions { Exact = true });
    }
}
