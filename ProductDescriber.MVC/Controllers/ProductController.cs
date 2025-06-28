using Microsoft.AspNetCore.Mvc;
using ProductDescriber.Base;
using ProductDescriber.Schema.Responses;
using System.Text;
using System.Text.Json;

namespace ProductDescriber.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            var request = new
            {
                title = model.Title,
                features = model.Features
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5050/api/Product", content);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "API isteği başarısız.");
                return View(model);
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<ProductResponse>>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            model.Description = result?.Data?.Description;
            return View("Result", model);
        }

        [HttpPost]
        [Route("Product/CreateJson")]
        public async Task<IActionResult> CreateJson([FromBody] ProductCreateViewModel model)
        {
            var request = new
            {
                title = model.Title,
                features = model.Features
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5050/api/Product", content);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("API isteği başarısız.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<ProductResponse>>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // TempData ile description saklanıyor
            TempData["Description"] = result?.Data?.Description;

            // Redirect URL gönderiliyor
            return Json(new
            {
                redirectUrl = Url.Action("ResultView", "Product")
            });
        }

        public IActionResult ResultView()
        {
            var description = TempData["Description"] as string;

            var model = new ProductCreateViewModel
            {
                Description = description
            };

            return View("Result", model);
        }


    }
}
