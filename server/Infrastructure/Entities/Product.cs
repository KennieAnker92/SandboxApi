using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Infrastructure.Entities;

[Table("Products")]
public class Product
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public string Name { get; set; } = null!;

    [Column]
    public decimal Price { get; set; }

    [Column]
    public string Description { get; set; } = null!;

    // Links to the join table
    [Association(ThisKey = nameof(Id), OtherKey = nameof(ProductCategory.ProductId), CanBeNull = true)]
    public List<ProductCategory> ProductCategories { get; set; } = [];
}