using FoodCalculator.FoodApi.EntityModel;

namespace FoodCalculator.FoodApi.DomainModel;

public class NutritionDataResult
{
    public ICollection<INutrient> Nutrients { get; set; } = new List<INutrient>();
}
