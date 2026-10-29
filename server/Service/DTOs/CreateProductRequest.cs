namespace Service.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public List<int> CategoryIds { get; set; } = [];
}