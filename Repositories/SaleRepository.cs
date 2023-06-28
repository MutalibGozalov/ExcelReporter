using ExcelReporting.Models;
using ExcelReporting.Data;
using ExcelReporting.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

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

        public async Task<IEnumerable<IGrouping<string, SaleModel>>> GetSalesBy(ReportType type, DateTime startDate, DateTime endDate, string email)
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


        public async Task SaveToDb(IFormFile file, string webRootPath)
        {
            string path = Path.Combine(webRootPath, "Uploads");
        
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

            foreach (var item in saleList)
            {
                _context.Sales.Add(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}
