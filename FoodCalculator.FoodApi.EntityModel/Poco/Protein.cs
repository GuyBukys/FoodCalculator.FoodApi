using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.EntityModel.Poco;

public class Protein : INutrient
{
    public string Label { get; } = "Protein";
    public double Quantity { get; set; }
}
