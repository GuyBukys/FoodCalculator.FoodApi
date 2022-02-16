using FoodCalculator.FoodApi.ApiModel;
using FoodCalculator.FoodApi.DomainModel;
using FoodCalculator.FoodApi.Options;
using FoodCalculator.FoodApi.RepositoryModel;
using FoodCalculator.FoodApi.RepositoryModel.Implementations;
using Serilog;
using Serilog.Formatting.Compact;

namespace FoodCalculator.FoodApi.WebApiApplication;

public static class ProgramExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        const string EdamamOptionsKey = "EdamamOptions";

        // register domain and repository instances
        services
            .AddScoped<INutritionRepository, NutritionRepository>()
            .AddScoped<INutritionRetriever, NutritionRetriever>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // register options
        services.Configure<EdamamOptions>(
            configuration.GetSection(EdamamOptionsKey));

        // register Http clients
        services.AddHttpClient<INutritionRepository, NutritionRepository>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("EdamamOptions:EdamamApiUri"));
        });

        // register mappings
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ApiModelMappingProfile>();
        });

        return services;
    }

    public static ConfigureHostBuilder ConfigureHost(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.WithThreadId();
            configuration.Enrich.WithProcessId();
            configuration.Enrich.WithEnvironmentName();
            configuration.Enrich.WithAssemblyName();
            configuration.WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Information)
            .WriteTo.File(
                path: "Logs/foodCalculatorDebug.json",
                formatter: new CompactJsonFormatter(),
                rollingInterval: RollingInterval.Day));


            configuration.WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Error)
            .WriteTo.File(
                path: "Logs/foodCalculatorError.json",
                formatter: new CompactJsonFormatter(),
                rollingInterval: RollingInterval.Day));
        });

        return host;
    }
}
