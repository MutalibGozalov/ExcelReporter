using Microsoft.AspNetCore.Mvc;
using ExcelReporting.Models;
using ExcelReporting.Repositories.Contracts;
using ExcelReporting.Services;

namespace ExcelReporting.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleRepository _saleRepository;
    private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

    public SaleController(ISaleRepository saleRepository, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
    {
        this._saleRepository = saleRepository;
        this._environment = environment;
        _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    }

    [HttpGet("{type}/{startDate}/{endDate}/{email}")]
    public async Task<ActionResult<string>> GetItem(ReportType type, DateTime startDate, DateTime endDate, string email)
    {
        try
        {   
            var sales = await this._saleRepository.GetSalesBy(type, startDate, endDate, email);

            if (sales == null)
            {
                return NotFound("not found");
            }
            else
            {
                string content = ReporterService.ReportSalesByProduct(sales, type);
                return Ok(content);
            }
        }
        catch (System.Exception m)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
            m.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<string>> PostXLXS(IFormFile file)
    { 
        try
        {
            if (file != null)
            {
                await this._saleRepository.SaveToDb(file, this._environment.WebRootPath);
                return Ok("data saved");

            }
            else
            {
                return Ok("File is null :/");    
            }

        }
        catch (System.Exception m)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
            m.Message);
        }
    }
}
