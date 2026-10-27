using LinqToDB;
using LinqToDB.AspNet;
using Infrastructure.Data;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Register Controllers and OpenAPI/Swagger
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Extract the array from appsettings
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register linq2db Context
builder.Services.AddLinqToDBContext<AppDataConnection>((provider, options) =>
    options.UseSQLite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db"));

// Register Service Layer
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDataConnection>();
    await DbInitializer.SeedAsync(db);
}
    app.MapOpenApi();
}

// For future Kennie. This must be placed before MapControllers and UseAuthorization
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run();