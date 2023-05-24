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

        public async Task<IEnumerable<IGrouping<string, SaleModel>>> GetDiscountsByProduct(DateTime startDate, DateTime endDate)
        {
            var sales = await _context.Sales
                .Where(m => m.Date > startDate && m.Date < endDate)
                .ToListAsync();

            var products = sales
                .GroupBy(m => m.Product)
                .ToList();

           return products;
        }

        public async Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesByProduct(DateTime startDate, DateTime endDate)
        {
            //var products = await this._context.Sales.Where<SaleModel>(m => m.Date > startDate && m.Date < endDate).GroupBy(m => m.Product).ToListAsync();
            // var products = await (from m in _context.Sales
            //                       where m.Date > startDate && m.Date < endDate
            //                       group m by m.Country into newGroup
            //                       orderby newGroup.Key
            //                       select newGroup).ToListAsync();
            var sales = await _context.Sales
                .Where(m => m.Date > startDate && m.Date < endDate)
                .ToListAsync();

            var products = sales
                .GroupBy(m => m.Product)
                .ToList();

            return products;
        }

        public async Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesByCountry(DateTime startDate, DateTime endDate)
        {
            var sales = await _context.Sales
                .Where(m => m.Date > startDate && m.Date < endDate)
                .ToListAsync();

            var products = sales
                .GroupBy(m => m.Country)
                .ToList();
                
            return products;
        }

        public async Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesBySegment(DateTime startDate, DateTime endDate)
        {

            var sales = await _context.Sales
                .Where(m => m.Date > startDate && m.Date < endDate)
                .ToListAsync();

            var products = sales
                .GroupBy(m => m.Segment.ToString())
                .ToList();
                
            return products;
        }

        public async Task<IEnumerable<IGrouping<string, SaleModel>>> Report(ReportType type, DateTime startDate, DateTime endDate, string email)
        {
            
            switch (type)
            {
                case ReportType.SalesByProduct: //GetSalesByProduct
                   return await GetSalesByProduct(startDate, endDate);
                case ReportType.SalesByCountry: //GetSalesByCountry
                    return await GetSalesByCountry(startDate, endDate);
                case ReportType.SalesBySegment: //GetSalesBySegment
                    return await GetSalesBySegment(startDate, endDate);
                case ReportType.DiscountsByProduct: //GetDiscountsByProduct
                    return await GetDiscountsByProduct(startDate, endDate);
                default:
                    throw new NotImplementedException();
            }

        }

        public async Task SaveToDb(List<SaleModel> saleList)
        {
            foreach (var item in saleList)
            {
                _context.Sales.Add(item);
            }
            await _context.SaveChangesAsync();
        }
    }
}
