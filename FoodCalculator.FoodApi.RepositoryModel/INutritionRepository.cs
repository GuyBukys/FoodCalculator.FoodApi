
namespace FoodCalculator.FoodApi.RepositoryModel;

public interface INutritionRepository
{
    Task<NutritionDataResponse> GetNutritionData(string ingredientQueryString);
}