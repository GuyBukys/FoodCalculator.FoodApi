using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.EntityModel.Poco;

public class Sugar : INutrient
{
    public string Label { get; } = "Sugars";
    public double Quantity { get; set; }
}
