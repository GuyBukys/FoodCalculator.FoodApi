using FoodCalculator.FoodApi.DomainModel;
using FoodCalculator.FoodApi.DomainModel.Implementations;
using FoodCalculator.FoodApi.Options;
using FoodCalculator.FoodApi.RepositoryModel;
using FoodCalculator.FoodApi.RepositoryModel.Implementations;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string EdamamOptionsKey = "EdamamOptions";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
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

builder.Services.Configure<EdamamOptions>(
    builder.Configuration.GetSection(EdamamOptionsKey));

builder.Services.AddHttpClient<INutritionRepository, NutritionRepository>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection(EdamamOptionsKey).GetValue<string>("EdemamApi"));
});

builder.Services
    .AddScoped<INutritionRepository, NutritionRepository>()
    .AddScoped<INutritionRetriever, NutritionRetriever>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller}/{action}/{id?}",
        defaults: new { action = "Get" });
});

app.MapControllers();

app.Run();
