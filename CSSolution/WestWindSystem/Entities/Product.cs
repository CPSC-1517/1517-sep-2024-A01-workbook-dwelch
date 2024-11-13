﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WestWindSystem.Entities;

[Index("CategoryID", Name = "CategoriesProducts")]
[Index("CategoryID", Name = "CategoryID")]
[Index("ProductName", Name = "ProductName")]
[Index("SupplierID", Name = "SupplierID")]
[Index("SupplierID", Name = "SuppliersProducts")]
public partial class Product
{
    //if the pkey is not an IDENTITY pkey you will need to add additional
    //  annotation parameter(s) to your key annotation
    // DatabaseGenerated()
    //  values: DatabaseGeneratedOption.None (not a IDENTITY field, user must supply the pkey)
    //          DatabaseGeneratedOption.IDENTITY (pkey is an IDENTITY field, default, can be added by is optional)
    //          DatabaseGeneratedOption.Computed (this is used on attributes that are computed from
    //                                              other record attributes, not seen on Keys
    //                                               Assume you have attributes price and quantity
    //                                                      you could compute totalcost = price and quantity
    //                                             this field does not actually contain data and the entity
    //                                                 will not expected data to be supplied)
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductID { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(40, ErrorMessage = "Product name is limited to 40 characters.")]
    public string ProductName { get; set; }

    public int SupplierID { get; set; }

    public int CategoryID { get; set; }

    [Required(ErrorMessage = "Product quantity per unit is required")]
    [StringLength(20, ErrorMessage = "Quantity per unit is limited to 20 characters.")]
    public string QuantityPerUnit { get; set; }

    public short? MinimumOrderQuantity { get; set; }

    [Column(TypeName = "money")]
    [Range(0.00,double.MaxValue, ErrorMessage = "Unit price cannot be a negative number.")]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Unit on order cannot be a negative number.")]
    public int UnitsOnOrder { get; set; }

    public bool Discontinued { get; set; }

    [ForeignKey("CategoryID")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<ManifestItem> ManifestItems { get; set; } = new List<ManifestItem>();

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("SupplierID")]
    [InverseProperty("Products")]
    public virtual Supplier Supplier { get; set; }
}