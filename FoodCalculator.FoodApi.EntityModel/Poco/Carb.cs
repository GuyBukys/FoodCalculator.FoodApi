using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.EntityModel.Poco;

public class Carb : INutrient
{
    public string Label { get; } = "Carbohydrates";
    public double Quantity { get; set; }
}
