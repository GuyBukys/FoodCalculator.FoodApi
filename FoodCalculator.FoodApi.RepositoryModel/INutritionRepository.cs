
namespace FoodCalculator.FoodApi.RepositoryModel;

public interface INutritionRepository
{
    Task GetCaloriesForFood(string food);
}