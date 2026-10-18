namespace Service;

using LinqToDB;
using Infrastructure.Data;
using Infrastructure.Entities;
using Service.DTOs;
using LinqToDB.Async;

public class ProductService(AppDataConnection db)
{
    private readonly AppDataConnection _db = db;

    public async Task<List<Product>> GetAllProductsAsync()
    {
        // .AsAsyncEnumerable() resolves .NET 10 System.Linq compiler collisions
        return await _db.Products.AsAsyncEnumerable().ToListAsync();
    }

    public async Task<Product> CreateProductAsync(CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Product name cannot be empty.");
        }

        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        var id = await _db.InsertWithInt32IdentityAsync(product);
        product.Id = id;

        return product;
    }
}