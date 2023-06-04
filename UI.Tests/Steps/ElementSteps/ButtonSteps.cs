using UI.PageObject.Elements;

namespace UI.Tests.Steps.ElementSteps;

[Binding]
public class ButtonSteps
{
    private readonly IBasePage _page;

    public ButtonSteps(IBasePage page)
    {
        _page = page;
    }

    [When("I click '(.*)' button")]
    public void WhenIClickButton(string buttonName)
    {
        _page.FindByRole<Button>(AriaRole.Button, buttonName).Click();
    }
}
