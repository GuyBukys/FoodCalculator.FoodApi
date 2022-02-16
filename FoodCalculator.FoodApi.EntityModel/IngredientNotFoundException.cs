using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.EntityModel;

public class IngredientNotFoundException : Exception
{
    public IngredientNotFoundException(string ingredientQueryString)
        : base($"could not find nutrition data for ingredient query: '{ingredientQueryString}'")
    {
    }
}
