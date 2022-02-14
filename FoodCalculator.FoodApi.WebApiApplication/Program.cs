using FoodCalculator.FoodApi.Options;
using FoodCalculator.FoodApi.RepositoryModel;
using FoodCalculator.FoodApi.RepositoryModel.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string EdamamOptionsKey = "EdamamOptions";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<EdamamOptions>(
    builder.Configuration.GetSection(EdamamOptionsKey));

builder.Services.AddHttpClient<INutritionRepository, NutritionRepository>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection(EdamamOptionsKey).GetValue<string>("EdemamApi"));
});

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
