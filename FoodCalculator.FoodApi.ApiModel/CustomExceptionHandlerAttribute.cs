using FluentValidation;
using FoodCalculator.FoodApi.EntityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FoodCalculator.FoodApi.ApiModel;

internal class CustomExceptionHandlerAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            ValidationException => new BadRequestResult(),
            IngredientNotFoundException => new NotFoundResult(),
            _ => new StatusCodeResult(500)
        };
    }
}