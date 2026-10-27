namespace Service;

using LinqToDB;
using Infrastructure.Data;
using Infrastructure.Entities;
using LinqToDB.Async;

public class CategoryService(AppDataConnection db)
{
    private readonly AppDataConnection _db = db;

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        // Fetch all categories for navigation and filtering
        return await _db.Categories.AsAsyncEnumerable().ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        // Fetch a category and eagerly load its associated products through the join table
        return await _db.Categories
            .LoadWith(c => c.ProductCategories)
            .ThenLoad(pc => pc.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateCategoryAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name cannot be empty.");
        }

        var category = new Category
        {
            Name = name
        };

        var id = await _db.InsertWithInt32IdentityAsync(category);
        category.Id = id;

        return category;
    }
}