﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WestWindSystem.Entities;

namespace WestWindSystem.DAL;

public partial class WestWindContext : DbContext
{
    public WestWindContext(DbContextOptions<WestWindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BuildVersion> BuildVersions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ManifestItem> ManifestItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BuildVer__3214EC07A34F4456");

            entity.Property(e => e.ReleaseDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerID).IsFixedLength();

            entity.HasOne(d => d.Address).WithOne(p => p.Customer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_Addresses");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.Address).WithOne(p => p.Employee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Addresses");

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation).HasConstraintName("FK_Employees_Employees");

            entity.HasMany(d => d.Territories).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeTerritory",
                    r => r.HasOne<Territory>().WithMany()
                        .HasForeignKey("TerritoryID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeTerritories_Territories"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeTerritories_Employees"),
                    j =>
                    {
                        j.HasKey("EmployeeID", "TerritoryID").IsClustered(false);
                        j.ToTable("EmployeeTerritories");
                        j.IndexerProperty<string>("TerritoryID").HasMaxLength(20);
                    });
        });

        modelBuilder.Entity<ManifestItem>(entity =>
        {
            entity.HasKey(e => e.ManifestItemID).HasName("PK__Manifest__9000192C20310370");

            entity.HasOne(d => d.Product).WithMany(p => p.ManifestItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ManifestItems_ToProducts");

            entity.HasOne(d => d.Shipment).WithMany(p => p.ManifestItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ManifestItems_ToShipments");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.CustomerID).IsFixedLength();
            entity.Property(e => e.Freight).HasDefaultValue(0m);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.SalesRep).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.ShipAddress).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Addresses");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.Quantity).HasDefaultValue((short)1);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentID).HasName("PK_PAY_PayID");

            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_ToTable");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAY_PTYP_PayTypeID");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeID).HasName("PK_PTYP_PayTypeID");

            entity.Property(e => e.PaymentTypeID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionID)
                .HasName("PK_Region")
                .IsClustered(false);

            entity.Property(e => e.RegionDescription).IsFixedLength();
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.ShipmentID).HasName("PK__Shipment__5CAD378DC350990B");

            entity.HasOne(d => d.Order).WithMany(p => p.Shipments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shipments_ToOrders");

            entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.Shipments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shipments_ToTShippers");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasOne(d => d.Address).WithOne(p => p.Supplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Suppliers_Addresses");
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasKey(e => e.TerritoryID).IsClustered(false);

            entity.Property(e => e.TerritoryDescription).IsFixedLength();

            entity.HasOne(d => d.Region).WithMany(p => p.Territories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Territories_Region");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}