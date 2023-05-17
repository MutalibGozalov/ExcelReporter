using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelReporting.Models.DTOs
{
    public class SalesByCountryDto
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public decimal TotalSales { get; set; }

    }
}