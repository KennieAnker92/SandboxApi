namespace IntegrationTests.Controllers;

using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Fixtures;
using Infrastructure.Entities;
using Service.DTOs;
using Xunit;

public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_And_Get_Products_ReturnsCreatedAndSuccess()
    {
        // Arrange
        var categoriesResponse = await _client.GetAsync("/api/categories");
        categoriesResponse.EnsureSuccessStatusCode();
        var categories = await categoriesResponse.Content.ReadFromJsonAsync<List<Category>>();
        Assert.NotNull(categories);
        var electronics = Assert.Single(categories, category => category.Name == "Electronics");

        var request = new CreateProductRequest
        {
            Name = "Mechanical Keyboard",
            Price = 799.00m,
            Description = "A mechanical keyboard with tactile switches.",
            CategoryIds = [electronics.Id]
        };

        // Act: POST
        var postResponse = await _client.PostAsJsonAsync("/api/products", request);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        // Act: GET
        var getResponse = await _client.GetAsync("/api/products");
        getResponse.EnsureSuccessStatusCode();
        var products = await getResponse.Content.ReadFromJsonAsync<List<Product>>();

        // Assert
        Assert.NotNull(products);
        var product = Assert.Single(products, product => product.Name == request.Name);
        Assert.Equal(request.Description, product.Description);

        var productCategory = Assert.Single(product.ProductCategories);
        Assert.Equal(electronics.Id, productCategory.CategoryId);
        Assert.Equal(electronics.Name, productCategory.Category?.Name);
    }

    [Fact]
    public async Task Get_Categories_ReturnsSuccessAndCategories()
    {
        // Act
        var response = await _client.GetAsync("/api/categories");
        var categories = await response.Content.ReadFromJsonAsync<List<Category>>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(categories);
        Assert.Contains(categories, category => category.Name == "Electronics");
    }

    [Fact]
    public async Task OpenApiDocument_ReturnsSuccessAndValidJson()
    {
        // Act
        var response = await _client.GetAsync("/openapi/v1.json");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }
}