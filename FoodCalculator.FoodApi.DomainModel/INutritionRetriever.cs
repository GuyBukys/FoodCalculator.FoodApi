namespace FoodCalculator.FoodApi.DomainModel;
public interface INutritionRetriever
{
    Task<NutritionDataResult> RetrieveNutritionData(NutritionDataDomainInput input);
}
