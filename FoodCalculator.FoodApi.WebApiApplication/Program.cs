using FoodCalculator.FoodApi.WebApiApplication;

var builder = WebApplication.CreateBuilder(args);

// custom builder configure 
builder.Services.ConfigureServices(builder.Configuration);
builder.Host.ConfigureHost();

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