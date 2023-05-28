using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExcelReporting.Models;
using ExcelReporting.Extensions;
using ExcelReporting.Models.DTOs;

namespace ExcelReporting.Services
{
    public static class ReporterService
    {
        public static string ReportSalesByProduct(IEnumerable<IGrouping<string, SaleModel>> sales, ReportType reportType)
        {
            string? ProductName = String.Empty;
            decimal TotalSales  = 0;
            string GroupBy = String.Empty;
            string TotalOf = "Total Sales";
            StringBuilder emailContent = new("""
            
            """);

            switch (reportType)
            {
                case ReportType.SalesByProduct:
                    GroupBy = "Product:";
                    break;
                case ReportType.SalesByCountry:
                    GroupBy = "Country:";
                    break;
                case ReportType.SalesBySegment:
                    GroupBy = "Segment:";
                    break;
                case ReportType.DiscountsByProduct:
                    GroupBy = "Product:";
                    TotalOf = "Total Discounts:";
                    break;   
                default:
                    break;
            }

            foreach (var itemGroup in sales)
            {
                emailContent.Append($"{GroupBy} {itemGroup.Key}");
                foreach (var item in itemGroup)
                {
                    TotalSales+=item.Sales;
                }
                emailContent.Append($"\n{TotalOf} {TotalSales} $\n");
                TotalSales  = 0;
            }  
            return emailContent.ToString();          
        }

    }
}