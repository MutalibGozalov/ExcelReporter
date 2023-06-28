using ExcelReporting.Models;

namespace ExcelReporting.Repositories.Contracts
{
    public interface ISaleRepository
    {
        // Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesBySegment(DateTime startDate, DateTime endDate);
        // Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesByCountry(DateTime startDate, DateTime endDate);
        // Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesByProduct(DateTime startDate, DateTime endDate);
        // Task<IEnumerable<IGrouping<string, SaleModel>>> GetDiscountsByProduct(DateTime startDate, DateTime endDate);

        Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesBy(ReportType type, DateTime startDate, DateTime endDate, string email);
        
        Task SaveToDb(IFormFile file, string webRootPath);
    }
}