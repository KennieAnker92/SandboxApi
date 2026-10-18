namespace Infrastructure.Entities;

using LinqToDB.Mapping;

[Table(Name = "Products")]
public class Product
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Name = "Name"), NotNull]
    public string Name { get; set; } = string.Empty;

    [Column(Name = "Price"), NotNull]
    public decimal Price { get; set; }
}