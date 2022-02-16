using AutoMapper;
using FoodCalculator.FoodApi.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.FoodApi.ApiModel;

public class ApiModelMappingProfile : Profile
{
    public ApiModelMappingProfile()
    {
        this.CreateMap<NutritionDataResult, NutritionDataViewModel>();
        this.CreateMap<NutritionDataRequestModel, NutritionDataDomainInput>();
    }
}
