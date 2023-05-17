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

namespace ExcelReporting.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            this._saleRepository = saleRepository;
        }

        [HttpGet("{type}/{startDate}/{endDate}/{email}")] // :int isn't necessary
        public async Task<ActionResult<SalesByProductDto>> GetItem(int type, DateTime startDate, DateTime endDate, string email, IFormFile file)
        {
            try
            {   /*
                    1 main method will be here which will then call ISaleRepository methods depent
                    on type parameter, and this method will be created at static ReportHandler class
                 */
                var sales = await this._saleRepository.GetSalesByProduct(startDate, endDate);

                if (sales == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDto = sales.ToSalesByProduct();
                    return Ok(productDto);
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SalesByProductDto>> PostXLXS(IFormFile file)
        {
            try
            {   
                var sales = await this._saleRepository.GetSalesByProduct(DateTime.Now, DateTime.Now);
                if (sales == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDto = sales.ToSalesByProduct();
                    return Ok(productDto);
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

    }
}