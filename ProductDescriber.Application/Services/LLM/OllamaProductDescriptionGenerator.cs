using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProductDescriber.Application.Services.LLM;

public class OllamaProductDescriptionGenerator : IProductDescriptionGenerator
{
    private readonly HttpClient _httpClient;

    public OllamaProductDescriptionGenerator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GenerateDescriptionAsync(string title, string features)
    {
        var prompt = $"""
Sen bir ürün açıklama asistanısın. Kullanıcı sana bir ürünün başlığını ve temel özelliklerini verir.
Amacın bu bilgilere dayanarak sadece sade, kısa ve kullanıcı dostu bir ürün açıklaması oluşturmaktır.

Kurallar:
- Sadece açıklama yaz. 
- Başlık, yorum, düşünce, açıklama veya kapanış cümlesi ekleme.
- "<think>" veya benzeri herhangi bir analiz ya da sistem çıktısı yazma.
- Cümleler kısa, anlaşılır ve doğal Türkçe ile yazılmış olsun.
- Teknik terimler veya aşırı satış dili kullanma.
- Sadece Türkçe karakterler ve düzgün dilbilgisi kullan.

Örnek çıktılar:
✅ "Bluetooth 5.0 ile hızlı bağlantı sunar. 20 saatlik pil ömrüyle uzun süreli kullanım sağlar. Gürültü engelleme özelliği sayesinde dış sesleri azaltır."
⛔ "İşte sizin için mükemmel bir ürün!" (yapma)
⛔ "Bu ürün şu şu özelliklere sahiptir:" (yapma)
⛔ "<think>Bu özelliklere göre..." (yapma)

Giriş:
Başlık: {title}
Özellikler: {features}

Yalnızca açıklamayı üret:
""";

        var requestData = new
        {
            model = "deepseek-r1:8b",
            prompt = prompt,
            stream = false
        };

        var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:11434/api/generate", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ollama API hatası: {error}");
        }

        var resultJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(resultJson);
        var rawText = doc.RootElement.GetProperty("response").GetString()?.Trim() ?? "";

        // <think>...</think> bloklarını temizle (her ihtimale karşı)
        var cleanText = Regex.Replace(rawText, "<think>.*?</think>", "", RegexOptions.Singleline).Trim();

        return cleanText;
    }
}
