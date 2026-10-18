namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        try
        {
            var product = await _productService.CreateProductAsync(request);
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}