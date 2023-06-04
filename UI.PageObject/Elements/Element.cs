using Core.Support;

namespace UI.PageObject.Elements;

public class Element
{
    public readonly ILocator Locator;

    public Element(ILocator locator) => Locator = locator;

    public virtual string Text => Locator.InnerTextAsync().Result;

    public bool Displayed => Locator.IsVisibleAsync().Result;
    public bool Enabled => Locator.IsEnabledAsync().Result;
    public bool Editable => Locator.IsEditableAsync().Result;

    public string? GetAttribute(string attribute) => Locator.GetAttributeAsync(attribute).Result;

    public void Click()
    {
        Logger.Log.Information($"Clicking on element with locator [{@Locator}].");
        Locator.ClickAsync().Wait();
    }

    public void Fill(string inputText)
    {
        Logger.Log.Information($"Filling with text [{@inputText}] element with locator [{@Locator}].");
        Locator.FillAsync(inputText).Wait();
    }

    public void Hover()
    {
        Logger.Log.Information($"Hovering for element with locator [{@Locator}].");
        Locator.HoverAsync().Wait();
    }

    #region Find

    public T Find<T>(string locator) where T : Element => (T)Activator.CreateInstance(typeof(T), Locator.Locator(locator))!;

    public ElementsCollection<T> FindAll<T>(string locator) where T : Element
    {
        var itemsLocator = Locator.Locator(locator);

        var count = itemsLocator.CountAsync().GetAwaiter().GetResult();
        var collection = new ElementsCollection<T>();
        for (int i = 0; i < count; i++)
        {
            var item = (T)Activator.CreateInstance(typeof(T), itemsLocator.Nth(i))!;
            collection.Add(item);
        }

        return collection;
    }

    public Element Find(string locator) => (Element)Activator.CreateInstance(typeof(Element), Locator.Locator(locator))!;

    public ElementsCollection<Element> FindAll(string locator)
    {
        var itemsLocator = Locator.Locator(locator);

        var count = itemsLocator.CountAsync().GetAwaiter().GetResult();
        var collection = new ElementsCollection<Element>();
        for (int i = 0; i < count; i++) collection.Add((Element)Activator.CreateInstance(typeof(Element), itemsLocator.Nth(i))!);
        return collection;
    }

    #endregion Find
}
