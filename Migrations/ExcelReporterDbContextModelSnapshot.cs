﻿// <auto-generated />
using System;
using ExcelReporting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExcelReporting.Migrations
{
    [DbContext(typeof(ExcelReporterDbContext))]
    partial class ExcelReporterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("ExcelReporting.Models.SaleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("COGS")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discount")
                        .HasColumnType("TEXT");

                    b.Property<int>("DiscountBand")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("GrossSales")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ManufacturingPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Product")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Profit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Sales")
                        .HasColumnType("TEXT");

                    b.Property<int>("Segment")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UnitsSold")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
