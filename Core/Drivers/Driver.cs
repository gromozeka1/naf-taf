using Core.Config;

namespace Core.Drivers;

public interface IDriver
{
    Task<IBrowser> Browser { get; }
    Task<IBrowserContext> BrowserContext { get; }
    Task<IAPIRequestContext> ApiRequestContext { get; }
    Task<IPage> Page { get; }
}

public sealed class Driver : IDisposable, IDriver
{
    private readonly AsyncLazy<IBrowser> _browser;
    private readonly AsyncLazy<IBrowserContext> _browserContext;
    private readonly AsyncLazy<IAPIRequestContext> _apiRequestContext;
    private readonly AsyncLazy<IPage> _page;

    private readonly TestSettings _testSettings;

    private readonly IDriverInit _driverInit;

    private bool _isDisposed;

    public Driver(TestSettings testSettings, IDriverInit driverInit)
    {
        _testSettings = testSettings;
        _driverInit = driverInit;

        _browser = new AsyncLazy<IBrowser>(InitializePlaywright);
        _browserContext = new AsyncLazy<IBrowserContext>(CreateBrowserContext);
        _apiRequestContext = new AsyncLazy<IAPIRequestContext>(CreateApiContext);
        _page = new AsyncLazy<IPage>(CreatePageAsync);
    }

    public Task<IPage> Page => _page.Value;

    public Task<IBrowser> Browser => _browser.Value;

    public Task<IBrowserContext> BrowserContext => _browserContext.Value;

    public Task<IAPIRequestContext> ApiRequestContext => _apiRequestContext.Value;

    private async Task<IBrowser> InitializePlaywright() 
        => await _driverInit.GetDriverAsync(_testSettings);

    private async Task<IPage> CreatePageAsync() 
        => await (await _browserContext).NewPageAsync();

    private async Task<IBrowserContext> CreateBrowserContext()
    {
        var browserContextOptions = new BrowserNewContextOptions()
        {
            ViewportSize = ViewportSize.NoViewport,
            ColorScheme = ColorScheme.Dark,
        };

        return (await _browser).NewContextAsync(browserContextOptions).Result;
    }

    private async Task<IAPIRequestContext> CreateApiContext()
    {
        var playwright = await Playwright.CreateAsync();
        
        var apiContextOptions = new APIRequestNewContextOptions
        {
            BaseURL = _testSettings.BaseApiUrl,
            IgnoreHTTPSErrors = _testSettings.IgnoreHTTPSErrors,
        };

        return await playwright.APIRequest.NewContextAsync(apiContextOptions);
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        if (_browser.IsValueCreated)
        {
            Task.Run(async () =>
            {
                await (await Browser).CloseAsync();
                await (await Browser).DisposeAsync();
            });
        }

        _isDisposed = true;
    }
}
