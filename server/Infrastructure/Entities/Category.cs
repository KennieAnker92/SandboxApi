using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Infrastructure.Entities;

[Table("Categories")]
public class Category
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public string Name { get; set; } = null!;

    // Navigation back to the join table
    [Association(ThisKey = nameof(Id), OtherKey = nameof(ProductCategory.CategoryId), CanBeNull = true)]
    public List<ProductCategory> ProductCategories { get; set; } = [];
}