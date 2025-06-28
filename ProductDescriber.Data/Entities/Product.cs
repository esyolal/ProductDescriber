namespace ProductDescriber.Data.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Features { get; set; } = null!;
    public string? Description { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
