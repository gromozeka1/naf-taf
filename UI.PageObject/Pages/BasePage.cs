using UI.PageObject.Elements;

namespace UI.PageObject.Pages;

public interface IBasePage
{
    Element Find(string locator);
    T Find<T>(string locator) where T : Element;
    ElementsCollection<Element> FindAll(string locator);
    ElementsCollection<T> FindAll<T>(string locator) where T : Element;
    T FindByLabel<T>(string label) where T : Element;
    T FindByRole<T>(AriaRole role, string name) where T : Element;
    T FindByText<T>(string text, PageGetByTextOptions? options = null) where T : Element;
    bool HasValidationError();
    Element ValidationError(string expectedError);
    void GoToPage(string pageName);
}

public class BasePage : IBasePage
{
    protected readonly IPage _page;

    public BasePage(IDriver driver)
    {
        _page = driver.Page.Result;
    }

    public T FindByLabel<T>(string label) where T : Element
        => (T)Activator.CreateInstance(typeof(T), _page.GetByLabel(label))!;

    public T FindByRole<T>(AriaRole role, string name) where T : Element
        => (T)Activator.CreateInstance(typeof(T), _page.GetByRole(role, new() { Name = name, Exact = true }))!;

    public T FindByText<T>(string text, PageGetByTextOptions? options = null) where T : Element
        => (T)Activator.CreateInstance(typeof(T), _page.GetByText(text, options))!;

    public T Find<T>(string locator) where T : Element
        => (T)Activator.CreateInstance(typeof(T), _page.Locator(locator))!;

    public Element Find(string locator) => Find<Element>(locator);

    public ElementsCollection<T> FindAll<T>(string locator) where T : Element
    {
        var itemsLocator = _page.Locator(locator);
        var count = itemsLocator.CountAsync().GetAwaiter().GetResult();
        var collection = new ElementsCollection<T>();
        for (int i = 0; i < count; i++)
        {
            var element = (T)Activator.CreateInstance(typeof(T), itemsLocator.Nth(i))!;
            collection.Add(element);
        }
        return collection;
    }

    public ElementsCollection<Element> FindAll(string locator) => FindAll<Element>(locator);

    public bool HasValidationError() => FindAll("//*[contains(@class,'error')]").Any();

    public Element ValidationError(string expectedError) => FindByText<Element>(expectedError);

    public void GoToPage(string pageName)
    {
        var pageLink = FindByRole<Link>(AriaRole.Link, pageName);
        pageLink.Click();
    }
}
