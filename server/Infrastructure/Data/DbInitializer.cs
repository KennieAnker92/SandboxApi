namespace Infrastructure.Data;

using Infrastructure.Entities;
using LinqToDB;
using LinqToDB.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDataConnection db)
    {
        // 1. Ensure tables exist (Safe for SQLite if tables aren't pre-created)
        await db.CreateTableAsync<Category>(tableOptions: TableOptions.CreateIfNotExists);
        await db.CreateTableAsync<Product>(tableOptions: TableOptions.CreateIfNotExists);
        await db.CreateTableAsync<ProductCategory>(tableOptions: TableOptions.CreateIfNotExists);

        // 2. Check if data already exists to avoid duplicate seeding
        if (db.Categories.Count() > 0)
        {
            return;
        }

        // 3. Seed Categories
        var electronics = new Category { Name = "Electronics" };
        var apparel = new Category { Name = "Apparel" };
        var books = new Category { Name = "Books" };

        electronics.Id = await db.InsertWithInt32IdentityAsync(electronics);
        apparel.Id = await db.InsertWithInt32IdentityAsync(apparel);
        books.Id = await db.InsertWithInt32IdentityAsync(books);

        // 4. Seed Products
        var p1 = new Product { Name = "Wireless Mouse", Price = 29.99m, Description = "Ergonomic wireless mouse with smooth tracking." };
        var p2 = new Product { Name = "Running Shoes", Price = 89.99m, Description = "Lightweight and durable shoes for long-distance running." };
        var p3 = new Product { Name = "TypeScript Handbook", Price = 39.99m, Description = "Comprehensive guide to mastering TypeScript in modern apps." };        
        var p4 = new Product { Name = "Bluetooth Headphones", Price = 59.99m, Description = "Noise-cancelling over-ear headphones with long battery life." };
        var p5 = new Product { Name = "Smartwatch", Price = 199.99m, Description = "Feature-rich smartwatch with fitness tracking and notifications." };
        var p6 = new Product { Name = "Graphic T-Shirt", Price = 19.99m, Description = "Comfortable cotton t-shirt with a stylish graphic print." };
        var p7 = new Product { Name = "Fantasy Novel", Price = 14.99m, Description = "A captivating fantasy novel filled with adventure and magic." };
        var p8 = new Product { Name = "Laptop Stand", Price = 49.99m, Description = "Adjustable laptop stand for better ergonomics and cooling." };
        var p9 = new Product { Name = "Wireless Charger", Price = 24.99m, Description = "Fast wireless charger compatible with Qi-enabled devices." };
        var p10 = new Product { Name = "Yoga Mat", Price = 29.99m, Description = "Non-slip yoga mat with extra cushioning for comfort." };
        var p11 = new Product { Name = "Digital Camera", Price = 499.99m, Description = "High-resolution digital camera with advanced features." };
        var p12 = new Product { Name = "Leather Wallet", Price = 39.99m, Description = "Premium leather wallet with multiple card slots." };

        p1.Id = await db.InsertWithInt32IdentityAsync(p1);
        p2.Id = await db.InsertWithInt32IdentityAsync(p2);
        p3.Id = await db.InsertWithInt32IdentityAsync(p3);
        p4.Id = await db.InsertWithInt32IdentityAsync(p4);
        p5.Id = await db.InsertWithInt32IdentityAsync(p5);
        p6.Id = await db.InsertWithInt32IdentityAsync(p6);
        p7.Id = await db.InsertWithInt32IdentityAsync(p7);
        p8.Id = await db.InsertWithInt32IdentityAsync(p8);
        p9.Id = await db.InsertWithInt32IdentityAsync(p9);
        p10.Id = await db.InsertWithInt32IdentityAsync(p10);
        p11.Id = await db.InsertWithInt32IdentityAsync(p11);
        p12.Id = await db.InsertWithInt32IdentityAsync(p12);

        // 5. Seed Many-to-Many Relationships (ProductCategories)
        // (Demonstrating mapping products to categories)
        await db.InsertAsync(new ProductCategory { ProductId = p1.Id, CategoryId = electronics.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p2.Id, CategoryId = apparel.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p3.Id, CategoryId = books.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p4.Id, CategoryId = electronics.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p5.Id, CategoryId = electronics.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p6.Id, CategoryId = apparel.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p7.Id, CategoryId = books.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p8.Id, CategoryId = electronics.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p9.Id, CategoryId = electronics.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p10.Id, CategoryId = apparel.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p11.Id, CategoryId = electronics.Id });
        await db.InsertAsync(new ProductCategory { ProductId = p12.Id, CategoryId = apparel.Id });
    }
}