using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelReporting.Models.DTOs
{
    public class SalesBySegmentDto
    {
        public int Id { get; set; }
        public Segment Segment { get; set; }
        public decimal TotalSales { get; set; }
    }
}