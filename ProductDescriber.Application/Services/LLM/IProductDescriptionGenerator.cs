namespace ProductDescriber.Application.Services.LLM;

public interface IProductDescriptionGenerator
{
    Task<string> GenerateDescriptionAsync(string title, string features);
}
