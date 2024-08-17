using Microsoft.AspNetCore.Mvc;
using Read_Json.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Read_Json.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var filePath = Path.Combine("D:\\DotNet\\Json Operation\\Json Operation\\bin\\Release\\net8.0\\publish", "data", "data.json");

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    var jsonData = await System.IO.File.ReadAllTextAsync(filePath);

                    var jsonObject = JsonSerializer.Deserialize<object>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    ViewData["JsonData"] = jsonObject;
                }
                else
                {
                    _logger.LogWarning("JSON file not found.");
                    ViewData["JsonData"] = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading JSON data from the file.");
                ViewData["JsonData"] = null;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
