using LinqToDB.Mapping;

namespace Infrastructure.Entities;

[Table("ProductCategories")]
public class ProductCategory
{
    [PrimaryKey(1), Column]
    public int ProductId { get; set; }

    [PrimaryKey(2), Column]
    public int CategoryId { get; set; }

    [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id), CanBeNull = false)]
    public Product? Product { get; set; }

    [Association(ThisKey = nameof(CategoryId), OtherKey = nameof(Category.Id), CanBeNull = false)]
    public Category? Category { get; set; }
}