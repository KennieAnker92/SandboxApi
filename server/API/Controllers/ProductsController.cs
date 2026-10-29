using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers;

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
    public async Task<ActionResult<List<Product>>> GetAll([FromQuery] int? categoryId)
    {
        if (categoryId.HasValue)
        {
            var filtered = await _productService.GetProductsByCategoryAsync(categoryId.Value);
            return Ok(filtered);
        }
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        
        return Ok(product);
    }

   [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Service.DTOs.CreateProductRequest request)
    {
        var created = await _productService.CreateProductAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}