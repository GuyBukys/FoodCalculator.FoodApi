
using FoodCalculator.FoodApi.RepositoryModel.Models;

namespace FoodCalculator.FoodApi.RepositoryModel;

public interface INutritionRepository
{
    Task<NutritionDataOutputModel> GetNutritionData(string food);
}