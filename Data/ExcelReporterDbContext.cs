using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExcelReporting.Models;

namespace ExcelReporting.Data
{
    public class ExcelReporterDbContext : DbContext
    {
        public ExcelReporterDbContext(DbContextOptions<ExcelReporterDbContext> options) : base(options)
        {
           
        }

         public DbSet<SaleModel>? Sales { get; set; }
    }
}