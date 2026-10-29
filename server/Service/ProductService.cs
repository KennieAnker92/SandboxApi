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
        return await _db.Products
            .LoadWith(p => p.ProductCategories)
            .ThenLoad(pc => pc.Category)
            .AsAsyncEnumerable()
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _db.Products
            .LoadWith(p => p.ProductCategories)
            .ThenLoad(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _db.Products
            .LoadWith(p => p.ProductCategories)
            .ThenLoad(pc => pc.Category)
            .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
            .AsAsyncEnumerable()
            .ToListAsync();
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
            Price = request.Price,
            Description = request.Description
        };

        // 1. Insert product and retrieve its generated ID
        var id = await _db.InsertWithInt32IdentityAsync(product);
        product.Id = id;

        // 2. Handle many-to-many category associations
        if (request.CategoryIds is { Count: > 0 })
        {
            foreach (var categoryId in request.CategoryIds)
            {
                await _db.InsertAsync(new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = categoryId
                });
            }
        }

        return product;
    }
}