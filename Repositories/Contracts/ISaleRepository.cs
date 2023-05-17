using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExcelReporting.Models;
using ExcelReporting.Models.DTOs;

namespace ExcelReporting.Repositories.Contracts
{
    public interface ISaleRepository
    {
        Task<IEnumerable<SaleModel>> GetSalesBySegment(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SaleModel>> GetSalesByCountry(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SaleModel>> GetSalesByProduct(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SaleModel>> GetDiscountsByProduct(DateTime startDate, DateTime endDate);

        Task<IEnumerable<SaleModel>> Report(int type, DateTime startDate, DateTime endDate, string email);
        

       
        /*  
         *  One method will be added to report to email and will be called inside of action where it'll
         *  accept email parameter from get request
         */


    }
}