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
        var request = new CreateProductRequest("Mechanical Keyboard", 799.00m);

        // Act: POST
        var postResponse = await _client.PostAsJsonAsync("/api/products", request);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        // Act: GET
        var getResponse = await _client.GetAsync("/api/products");
        var products = await getResponse.Content.ReadFromJsonAsync<List<Product>>();

        // Assert
        Assert.NotNull(products);
        Assert.Single(products);
        Assert.Equal("Mechanical Keyboard", products[0].Name);
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