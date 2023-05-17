using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelReporting.Models.DTOs
{
    public class DiscountsByProductDto
    {
        public int Id { get; set; }
        public string? Product { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}