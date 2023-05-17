using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelReporting.Models.DTOs
{
    public class SalesByProductDto
    {
        public int Id { get; set; }
        public string? Product { get; set; }
        public decimal TotalSales { get; set; }
    }
}