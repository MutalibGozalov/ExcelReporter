using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelReporting.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public Segment Segment { get; set; }
        public string? Country { get; set; }
        public string? Product { get; set; }
        public Discount DiscountBand  { get; set; }
        public decimal UnitsSold { get; set; }
        public decimal ManufacturingPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal GrossSales { get; set; }
        public decimal Discount { get; set; }
        public decimal Sales { get; set; }
        public decimal COGS { get; set; }
        public decimal Profit { get; set; }
        public DateTime Date { get; set; }
    }
}