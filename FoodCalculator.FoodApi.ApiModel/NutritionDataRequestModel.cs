using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.ApiModel;

public class NutritionDataRequestModel
{
    public string Ingredient { get; set; } = string.Empty;

    public double AmountInGrams { get; set; }
}
