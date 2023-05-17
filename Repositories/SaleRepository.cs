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
        Task<IEnumerable<SaleModel>> ISaleRepository.GetDiscountsByProduct(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SaleModel>> ISaleRepository.GetSalesByCountry(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<SaleModel>> ISaleRepository.GetSalesByProduct(DateTime startDate, DateTime endDate)
        {
            var products = await this._context.Sales.ToListAsync();
            return products;
        }

        Task<IEnumerable<SaleModel>> ISaleRepository.GetSalesBySegment(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SaleModel>> Report(int type, DateTime startDate, DateTime endDate, string email)
        {
            // ISaleRepository handler = new SaleRepository();
            switch (type)
            {
                case 1: //GetSalesByProduct
                    
                    break;
                case 2: //GetSalesByCountry

                    break;
                case 3: //GetSalesBySegment

                    break;
                case 4: //GetDiscountsByProduct

                    break;
                default:

                    break;
            }
              throw new NotImplementedException();
        }

        Task<IEnumerable<SaleModel>> ISaleRepository.Report(int type, DateTime startDate, DateTime endDate, string email)
        {
            throw new NotImplementedException();
        }
    }
}