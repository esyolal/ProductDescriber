using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ProductDescriber.Application.Services.LLM;

public class GeminiApiProductDescriptionGenerator : IProductDescriptionGenerator
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiApiProductDescriptionGenerator(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException("Gemini API Key yok");
    }

    public async Task<string> GenerateDescriptionAsync(string title, string features)
    {
        var prompt = $"Başlık: {title}\nÖzellikler: {features}\n\nYalnızca bu ürün için sade, net ve açıklayıcı bir ürün açıklaması üret. Giriş, başlık, vurgulu ifadeler, pazarlama cümleleri, markdown, emoji ya da açıklama dışı herhangi bir şey kullanma. Sadece açıklamayı döndür.";

        var requestData = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(
      $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={_apiKey}",
      content
  );




        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Gemini API hatası: {error}");
        }

        var resultJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(resultJson);

        var description = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return description ?? "Açıklama üretilemedi.";
    }
}
