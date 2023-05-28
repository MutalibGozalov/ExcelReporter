using System.Text;
using System.Net;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExcelReporting.Data;
using ExcelReporting.Models;
using ExcelReporting.Repositories;
using ExcelReporting.Repositories.Contracts;
using ExcelReporting.Models.DTOs;
using ExcelReporting.Extensions;
using ExcelReporting.Services;
using System.Data;
using System.Data.OleDb;

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
                string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }


                OleDbConnection connection = new(connectionString: $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 8.0; HDR = YES\";");
                connection.Open();
                string query = "SELECT * FROM [SHEET1$]";
                OleDbDataAdapter da = new OleDbDataAdapter(query, connection);


                DataTable dt = new DataTable();
                da.Fill(dt);
                connection.Dispose();


                List<SaleModel> saleList = new List<SaleModel>();

                List<string> rows = new List<string>();
                int i = 1;
                foreach (DataRow item in dt.Rows)
                {
                    rows.Add($"{i}");
                    i++;
                    foreach (var cell in item.ItemArray)
                    {
                        rows.Add(cell.ToString().Trim());
                    }
                    saleList.Add(
                        new SaleModel() 
                        {
                            Id = int.Parse(rows[0]),
                            Segment = (Segment) Enum.Parse(typeof(Segment), rows[1].Replace(" ", ""), true),
                            Country = rows[2],
                            Product = rows[3],
                            DiscountBand = (Discount) Enum.Parse(typeof(Discount), rows[4], true),
                            UnitsSold = decimal.Parse(rows[5]),
                            ManufacturingPrice = decimal.Parse(rows[6]),
                            SalePrice = decimal.Parse(rows[7]),
                            GrossSales = decimal.Parse(rows[8]),
                            Discount = decimal.Parse(rows[9]),
                            Sales = decimal.Parse(rows[10]),
                            COGS = decimal.Parse(rows[11]),
                            Profit = decimal.Parse(rows[12]),
                            Date = DateTime.Parse(rows[13])
                        });
                    rows.Clear();
                }

                await this._saleRepository.SaveToDb(saleList);
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
