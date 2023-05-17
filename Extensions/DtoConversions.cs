using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExcelReporting.Models.DTOs;
using ExcelReporting.Models;

namespace ExcelReporting.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<SalesByCountryDto> ToSalesByCountry(this IEnumerable<SaleModel> sales)
        {
            return (from sale in sales
                    select new SalesByCountryDto
                    {
                        Id = sale.Id,
                        Country = sale.Country,
                        TotalSales = sale.Sales
                    }).ToList();
        }

        public static IEnumerable<SalesByProductDto> ToSalesByProduct(this IEnumerable<SaleModel> sales)
        {
            return (from sale in sales
                    select new SalesByProductDto
                    {
                        Id = sale.Id,
                        Product = sale.Product,
                        TotalSales = sale.Sales
                    }).ToList();
        }

        public static IEnumerable<SalesBySegmentDto> ToSalesBySegment(this IEnumerable<SaleModel> sales)
        {
            return (from sale in sales
                    select new SalesBySegmentDto
                    {
                        Id = sale.Id,
                        Segment = sale.Segment,
                        TotalSales = sale.Sales
                    }).ToList();
        }

        public static IEnumerable<DiscountsByProductDto> ToDiscountsByProduct(this IEnumerable<SaleModel> sales)
        {
            return (from sale in sales
                    select new DiscountsByProductDto
                    {
                        Id = sale.Id,
                        Product = sale.Product,
                        TotalDiscount = sale.Discount
                    }).ToList();
        }
    }
}