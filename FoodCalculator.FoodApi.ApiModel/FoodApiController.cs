using Microsoft.AspNetCore.Mvc;

namespace FoodCalculator.FoodApi.ApiModel;

[ApiController]
public class FoodApiController : ControllerBase
{
    [HttpGet]
    [Route("HelloWorld")]
    public async Task<string> HelloWorld()
    {
        return await Task.Run(() => "hello world");
    }
}
