using FoodCalculator.FoodApi.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.ApiModel;

public class NutritionDataViewModel
{
    public ICollection<INutrient> Nutrients { get; set; } = new List<INutrient>();
}
