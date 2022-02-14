using FoodCalculator.FoodApi.Options;
using FoodCalculator.FoodApi.RepositoryModel.Models;
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

    public async Task<NutritionDataOutputModel> GetNutritionData(string food)
    {
        try
        {
            _logger.LogInformation($"Started getting nutrition data for food: '{food}'");

            string requestUri = $"nutrition-data" +
                $"?app_id={_edamamOptions.CurrentValue.ApiId}" +
                $"&app_key={_edamamOptions.CurrentValue.ApiSecret}" +
                $"&ingr={food}";

            HttpResponseMessage response = await _httpClient.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            NutritionDataOutputModel output = JsonConvert.DeserializeObject<NutritionDataOutputModel>(responseContent)!;

            _logger.LogInformation($"Finished getting nutrition data for food: '{food}'");

            return output;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Finished getting nutrition data for food: '{food}'");
            throw;
        }
    }
}
