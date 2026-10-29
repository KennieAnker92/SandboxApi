using LinqToDB;
using LinqToDB.Data;
using Infrastructure.Entities;

namespace Infrastructure.Data;

public class AppDataConnection : DataConnection
{
    public AppDataConnection(DataOptions<AppDataConnection> options) : base(options.Options) { }

    public ITable<Product> Products => this.GetTable<Product>();
    public ITable<Category> Categories => this.GetTable<Category>();
    public ITable<ProductCategory> ProductCategories => this.GetTable<ProductCategory>();
}