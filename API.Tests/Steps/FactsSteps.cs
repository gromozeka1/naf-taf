using System.Text.Json;
using FluentAssertions;
using Core.Constants;
using API.Tests.Models;

namespace API.Tests.Steps;

[Binding]
[Scope(Feature = "Facts")]
public class FactsSteps : BaseSteps
{
    public FactsSteps(IDriver driver) : base(driver)
    {
    }

    [When(@"I request a random cat fact")]
    public async Task WhenIRequestARandomCatFact()
    {
        await Get(Endpoint.Fact);
    }

    [Then(@"I should receive a cat fact")]
    public async Task ThenIShouldReceiveACatFact()
    {
        var responseData = await _response!.JsonAsync();
        _response.Status.Should().Be(200);
        responseData.Should().NotBeNull();

        var catFact = responseData?.Deserialize<CatFactModel>(_jsonSerializerOptions);
        catFact?.Fact.Should().NotBeNullOrEmpty();
    }

    [When(@"I request a random cat fact with length less than (\d+)")]
    public async Task WhenIRequestARandomCatFactWithLengthLessThan(int maxLength)
    {
        var options = new APIRequestContextOptions
        {
            Params = new Dictionary<string, object>
            {
                { "max_length", maxLength }
            }
        };

        await Get(Endpoint.Fact, options);
    }

    [Then(@"I should receive a cat fact with length less than (\d+)")]
    public async Task ThenIShouldReceiveACatFactWithLengthLessThan(int maxLength)
    {
        var responseData = await _response!.JsonAsync();
        _response.Status.Should().Be(200);
        responseData.Should().NotBeNull();

        var catFact = responseData?.Deserialize<CatFactModel>(_jsonSerializerOptions);
        catFact?.Fact.Should().NotBeNullOrEmpty();
        catFact?.Length.Should().BeLessThanOrEqualTo(maxLength);
    }
}
