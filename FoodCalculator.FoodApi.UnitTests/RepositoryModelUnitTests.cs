using System;
using System.Threading.Tasks;
using FoodCalculator.FoodApi.RepositoryModel;
using FoodCalculator.FoodApi.WebApiApplication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FoodCalculator.FoodApi.UnitTests;
public class RepositoryModelUnitTests
{
    private readonly INutritionRepository _nutritionRepository;

    public RepositoryModelUnitTests()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Testing.json")
            .Build();

        IServiceProvider serviceProvider = new ServiceCollection()
            .ConfigureServices(configuration)
            .BuildServiceProvider();

        _nutritionRepository = serviceProvider.GetRequiredService<INutritionRepository>();
    }

    [Fact]
    public async Task CanGetNutritionDataFromApi()
    {
        NutritionDataResponse response = await _nutritionRepository.GetNutritionData("chicken 100 gram");

        Assert.NotNull(response);
        Assert.NotNull(response.totalNutrients);
    }
}