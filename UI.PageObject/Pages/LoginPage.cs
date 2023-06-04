using UI.PageObject.Elements;

namespace UI.PageObject.Pages;

public interface ILoginPage
{
    void LogIn(string userName, string password);
}

public class LoginPage : BasePage, ILoginPage
{
    private readonly Input _userName;
    private readonly Input _password;
    private readonly Button _loginButton;

    public LoginPage(IDriver driver) : base(driver)
    {
        _userName = Find<Input>("#UserName");
        _password = Find<Input>("#Password");
        _loginButton = Find<Button>("text=Log in");
    }

    public void LogIn(string userName, string password)
    {
        _userName.Fill(userName);
        _password.Fill(password);
        _loginButton.Click();
    }
}
