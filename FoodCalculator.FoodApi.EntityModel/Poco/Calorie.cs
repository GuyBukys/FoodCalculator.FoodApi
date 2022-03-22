using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.EntityModel.Poco;

public class Calorie : INutrient
{
    public string Label { get;} = "Energy - Calories";
    public double Quantity { get; set; }
}
