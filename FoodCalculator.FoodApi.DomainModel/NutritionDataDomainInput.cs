namespace FoodCalculator.FoodApi.DomainModel;

public class NutritionDataDomainInput
{
    public string Ingredient { get; set; } = string.Empty;

    public double AmountInGrams { get; set; }
}