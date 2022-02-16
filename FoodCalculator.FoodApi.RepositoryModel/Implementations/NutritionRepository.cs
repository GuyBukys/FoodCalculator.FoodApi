using FoodCalculator.FoodApi.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FoodCalculator.FoodApi.RepositoryModel.Implementations;
public class NutritionRepository : INutritionRepository
{
    private readonly ILogger<NutritionRepository> _logger;
    private readonly HttpClient _httpClient;
    private readonly IOptionsMonitor<EdamamOptions> _edamamOptions;

    public NutritionRepository(
        ILogger<NutritionRepository> logger,
        HttpClient httpClient,
        IOptionsMonitor<EdamamOptions> edamamOptions)
    {
        _logger = logger;
        _httpClient = httpClient;
        _edamamOptions = edamamOptions;
    }

    public async Task<NutritionDataResponse> GetNutritionData(string ingredientQueryString)
    {
        try
        {
            _logger.LogInformation($"Started getting nutrition data for ingredient query: '{ingredientQueryString}'");

            string requestUri = $"nutrition-data" +
                $"?app_id={_edamamOptions.CurrentValue.ApiId}" +
                $"&app_key={_edamamOptions.CurrentValue.ApiSecret}" +
                $"&ingr={ingredientQueryString}";

            HttpResponseMessage response = await _httpClient.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            NutritionDataResponse output = JsonConvert.DeserializeObject<NutritionDataResponse>(responseContent)!;

            _logger.LogInformation($"Finished getting nutrition data for query: '{ingredientQueryString}'");

            return output;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Finished getting nutrition data for query: '{ingredientQueryString}'");
            throw;
        }
    }
}
