namespace FoodCalculator.FoodApi.EntityModel;
public interface INutrient
{
    public string Label { get; }

    public double Quantity { get; set; }
}
