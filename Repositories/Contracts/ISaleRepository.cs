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
        Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesBySegment(DateTime startDate, DateTime endDate);
        Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesByCountry(DateTime startDate, DateTime endDate);
        Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesByProduct(DateTime startDate, DateTime endDate);
        Task<IEnumerable<IGrouping<string, SaleModel>>> GetDiscountsByProduct(DateTime startDate, DateTime endDate);

        Task<IEnumerable<IGrouping<string, SaleModel>>> Report(ReportType type, DateTime startDate, DateTime endDate, string email);
        
        Task SaveToDb(List<SaleModel> saleList);
       
        /*  
         *  One method will be added to report to email and will be called inside of action where it'll
         *  accept email parameter from get request
         */


    }
}