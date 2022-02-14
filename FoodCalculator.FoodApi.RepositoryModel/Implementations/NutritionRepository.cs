using FoodCalculator.FoodApi.Options;
using Microsoft.Extensions.Options;

namespace FoodCalculator.FoodApi.RepositoryModel.Implementations;
public class NutritionRepository : INutritionRepository
{
    private readonly HttpClient _httpClient;
    private readonly IOptionsMonitor<EdamamOptions> _edamamOptions;

    public NutritionRepository(
        HttpClient httpClient,
        IOptionsMonitor<EdamamOptions> edamamOptions)
    {
        _httpClient = httpClient;
        _edamamOptions = edamamOptions;
    }

    public async Task GetCaloriesForFood(string food)
    {
        string requestUri = $"nutrition-data" +
            $"?app_id={_edamamOptions.CurrentValue.ApiId}" +
            $"&app_key={_edamamOptions.CurrentValue.ApiSecret}" +
            $"&ingr={food}";

        HttpResponseMessage response = await _httpClient.GetAsync(
            requestUri,
            HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        using Stream responseStream = await response.Content
            .ReadAsStreamAsync()
            .ConfigureAwait(false);

        var output
    }
}
