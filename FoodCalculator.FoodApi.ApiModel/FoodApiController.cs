using AutoMapper;
using FoodCalculator.FoodApi.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FoodCalculator.FoodApi.ApiModel;

[ApiController]
[CustomExceptionHandler]
[Route("api/[controller]/[action]")]
public class FoodApiController : ControllerBase
{
    private readonly ILogger<FoodApiController> _logger;
    private readonly IMapper _mapper;
    private readonly INutritionRetriever _nutritionRetriever;

    public FoodApiController(
        ILogger<FoodApiController> logger,
        IMapper mapper,
        INutritionRetriever nutritionRetriever)
    {
        _logger = logger;
        _mapper = mapper;
        _nutritionRetriever = nutritionRetriever;
    }

    [HttpPost]
    public async Task<IActionResult> GetNutritionData([FromBody] NutritionDataRequestModel request)
    {
        try
        {
            _logger.LogInformation($"Started getting nutrition data for ingredient: '{request.Ingredient}' Api model");

            NutritionDataDomainInput input = _mapper.Map<NutritionDataRequestModel, NutritionDataDomainInput>(request);

            NutritionDataResult result = await _nutritionRetriever.RetrieveNutritionData(input);

            NutritionDataViewModel viewModel = _mapper.Map<NutritionDataResult, NutritionDataViewModel>(result);

            _logger.LogInformation($"Finished getting nutrition data for ingredient: '{request.Ingredient}' Api model");

            return Ok(viewModel);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured while trying to get nutrition data for ingredient: '{request.Ingredient}' Api model");
            throw;
        }
    }
}
