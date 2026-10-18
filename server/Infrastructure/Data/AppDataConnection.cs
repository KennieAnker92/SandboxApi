namespace Infrastructure.Data;

using LinqToDB;
using LinqToDB.Data;
using Infrastructure.Entities;

public class AppDataConnection : DataConnection
{
    public AppDataConnection(DataOptions options) : base(options) { }

    public ITable<Product> Products => this.GetTable<Product>();
}