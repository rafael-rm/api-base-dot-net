using HyzenApi.DTO;
using HyzenApi.Infrastructure;
using HyzenApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HyzenApi.Controllers;

[ApiController]
[Route("api/v1/Base")]
public class BaseController : ControllerBase
{
    [HttpPost, Route("Create")]
    public async Task<IActionResult> Add(BaseDto @base)
    {
        await using var context = DatabaseContext.Get();
        var result = await BaseModel.CreateAsync(@base);
        await context.SaveChangesAsync();

        return Ok(result);
    }
    
    [HttpGet, Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        await using var context = DatabaseContext.Get();
        var result = await BaseModel.GetAsync(id);

        return Ok(result);
    }
    
    [HttpDelete, Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await using var context = DatabaseContext.Get();
        await BaseModel.DeleteAsync(id);

        return Ok();
    }
}