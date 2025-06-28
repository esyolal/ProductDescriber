namespace ProductDescriber.Schema.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Features { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
