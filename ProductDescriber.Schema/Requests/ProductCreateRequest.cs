namespace ProductDescriber.Schema.Requests;

public class ProductCreateRequest
{
    public string Title { get; set; } = null!;
    public string Features { get; set; } = null!;
}
