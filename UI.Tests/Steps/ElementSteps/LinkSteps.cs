using UI.PageObject.Elements;

namespace UI.Tests.Steps.ElementSteps;

[Binding]
public class LinkSteps
{
    private readonly IBasePage _page;

    public LinkSteps(IBasePage page)
    {
        _page = page;
    }

    [When("I click '(.*)' link")]
    public void WhenIClickButton(string linkName)
    {
        _page.FindByRole<Link>(AriaRole.Link, linkName).Click();
    }
}
