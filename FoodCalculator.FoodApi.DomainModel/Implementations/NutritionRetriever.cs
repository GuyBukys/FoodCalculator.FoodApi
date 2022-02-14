using FoodCalculator.FoodApi.RepositoryModel;

namespace FoodCalculator.FoodApi.DomainModel.Implementations;
public class NutritionRetriever : INutritionRetriever
{
    private readonly INutritionRepository _nutritionRepository;

    public NutritionRetriever(INutritionRepository nutritionRepository)
    {
        _nutritionRepository = nutritionRepository;
    }

    public async Task<NutritionResult> GetNutrition(NutritionInput input)
    {

    }
}
