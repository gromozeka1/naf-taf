using System.Text.Json;

namespace API.Tests.Steps;

public abstract class BaseSteps
{
    protected IAPIResponse? _response;
    protected readonly IDriver _driver;
    protected JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    protected BaseSteps(IDriver driver)
    {
        _driver = driver;
    }

    protected async Task Get(string endpoint, APIRequestContextOptions? options = null)
    {
        _response = await (await _driver.ApiRequestContext).GetAsync(endpoint, options);
    }
}
