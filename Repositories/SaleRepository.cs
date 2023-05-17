using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using ExcelReporting.Models;
using ExcelReporting.Data;
using ExcelReporting.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;


namespace ExcelReporting.Repositories
{
    public class SaleRepository : ISaleRepository
    {

        private readonly ExcelReporterDbContext _context;
        public SaleRepository(ExcelReporterDbContext context)
        {
            _context = context;    
        }

        public async Task<IEnumerable<SaleModel>> GetDiscountsByProduct(DateTime startDate, DateTime endDate)
        {
           var products = await this._context.Sales.Where<SaleModel>(m => m.Date > startDate && m.Date < endDate).ToListAsync();
           return products;
        }

        public async Task<IEnumerable<SaleModel>> GetSalesByCountry(DateTime startDate, DateTime endDate)
        {
            var products = await this._context.Sales.Where<SaleModel>(m => m.Date > startDate && m.Date < endDate).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<SaleModel>> GetSalesByProduct(DateTime startDate, DateTime endDate)
        {
            var products = await this._context.Sales.Where<SaleModel>(m => m.Date > startDate && m.Date < endDate).GroupBy().ToListAsync();
            return products;
        }

        public async Task<IEnumerable<SaleModel>> GetSalesBySegment(DateTime startDate, DateTime endDate)
        {
            var products = await this._context.Sales.Where<SaleModel>(m => m.Date > startDate && m.Date < endDate).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<SaleModel>> Report(int type, DateTime startDate, DateTime endDate, string email)
        {
            
            switch (type)
            {
                case 1: //GetSalesByProduct
                   return await GetSalesByCountry(startDate, endDate);
                    break;
                case 2: //GetSalesByCountry
                    return await GetSalesByCountry(startDate, endDate);
                    break;
                case 3: //GetSalesBySegment
                    return await GetSalesBySegment(startDate, endDate);
                    break;
                case 4: //GetDiscountsByProduct
                    return await GetDiscountsByProduct(startDate, endDate);
                    break;
                default:

                    break;
            }
              throw new NotImplementedException();
        }
    }
}