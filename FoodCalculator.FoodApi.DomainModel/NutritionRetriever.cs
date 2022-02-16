using AutoMapper;
using FoodCalculator.FoodApi.EntityModel;
using FoodCalculator.FoodApi.EntityModel.Poco;
using FoodCalculator.FoodApi.RepositoryModel;
using Microsoft.Extensions.Logging;

namespace FoodCalculator.FoodApi.DomainModel;
public class NutritionRetriever : INutritionRetriever
{
    private readonly ILogger<NutritionRetriever> _logger;
    private readonly IMapper _mapper;
    private readonly INutritionRepository _nutritionRepository;

    public NutritionRetriever(
        ILogger<NutritionRetriever> logger,
        IMapper mapper,
        INutritionRepository nutritionRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _nutritionRepository = nutritionRepository;
    }

    public async Task<NutritionDataResult> RetrieveNutritionData(NutritionDataDomainInput input)
    {
        NutritionDataResult result = new();
        try
        {
            _logger.LogInformation($"Started getting nutrition data for ingredient: '{input.Ingredient}' Domain model");

            string nutritionDataQueryString = string.Concat(
                input.Ingredient,
                " ",
                input.AmountInGrams,
                "grams"
                );

            NutritionDataResponse nutritionDataResponse = await _nutritionRepository.GetNutritionData(nutritionDataQueryString);
            TotalNutrients totalNutrients = nutritionDataResponse.totalNutrients;

            if (totalNutrients is null ||
                totalNutrients.ENERC_KCAL is null ||
                totalNutrients.ENERC_KCAL.quantity == 0)
            {
                throw new IngredientNotFoundException(nutritionDataQueryString);
            }

            ICollection<INutrient> nutrients = this.ConvertTotalNutrientsToList(totalNutrients);

            _logger.LogInformation($"Finished getting nutrition data for ingredient: '{input.Ingredient}' Domain model");

            return new NutritionDataResult
            {
                Nutrients = nutrients,
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured while trying to get nutrition data for ingredient: '{input.Ingredient}' Domain model");
            throw;
        }
    }

    private ICollection<INutrient> ConvertTotalNutrientsToList(TotalNutrients totalNutrients)
    {
        return new List<INutrient>
        {
            new Calorie
            {
                Quantity = totalNutrients.ENERC_KCAL.quantity,
            },
            new Carb
            {
                Quantity = totalNutrients.ENERC_KCAL.quantity,
            },
            new Fat
            {
                Quantity = totalNutrients.FAT.quantity,
            },
            new Protein
            {
                Quantity = totalNutrients.PROCNT.quantity,
            },
            new Sugar
            {
                Quantity = totalNutrients.SUGAR.quantity,
            }
        };
    }
}
