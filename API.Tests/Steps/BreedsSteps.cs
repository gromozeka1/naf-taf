using API.Tests.Models;
using Core.Constants;
using FluentAssertions;
using System.Text.Json;

namespace API.Tests.Steps;

[Binding]
[Scope(Feature ="Breeds")]
public class BreedsSteps : BaseSteps
{
    public BreedsSteps(IDriver driver) : base(driver)
    {
    }

    [When(@"I request all cat breeds")]
    public async Task WhenIRequestAllCatBreeds()
    {
        await Get(Endpoint.Breeds);
    }

    [Then(@"I should receive a list of cat breeds")]
    public async Task ThenIShouldReceiveAListOfCatBreeds()
    {
        var responseData = await _response!.JsonAsync();
        _response.Status.Should().Be(200);
        responseData.Should().NotBeNull();

        var catBreeds = responseData?.Deserialize<ResponseBreedsModel>(_jsonSerializerOptions);
        catBreeds?.Data.Should().NotBeNullOrEmpty();
    }

    [When(@"I request a cat breed with limited amount of results '(\d+)'")]
    public async Task WhenIRequestACatBreedWithLimitedAmountOfResults(int limit)
    {
        var options = new APIRequestContextOptions
        {
            Params = new Dictionary<string, object>
            {
                { "limit", limit }
            }
        };

        await Get(Endpoint.Breeds, options);
    }

    [Then(@"I should receive '([^']*)' cat breeds")]
    public async Task ThenIShouldReceiveCatBreeds(int limit)
    {
        var responseData = await _response!.JsonAsync();
        _response.Status.Should().Be(200);
        responseData.Should().NotBeNull();

        var catBreeds = responseData?.Deserialize<ResponseBreedsModel>(_jsonSerializerOptions);
        catBreeds?.Data?.Should().NotBeNullOrEmpty();
        catBreeds?.Data?.Length.Should().Be(limit);
    }
}
