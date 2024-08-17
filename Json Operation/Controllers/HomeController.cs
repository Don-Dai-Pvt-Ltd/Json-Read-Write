using Json_Operation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Json_Operation.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Privacy(string jsonData)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "data.json");

            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var jsonDocument = JsonDocument.Parse(jsonData);

                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var formattedJson = JsonSerializer.Serialize(jsonDocument.RootElement, jsonSerializerOptions);

                await System.IO.File.WriteAllTextAsync(filePath, formattedJson);

                _logger.LogInformation("JSON data has been written to the file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while writing JSON data to the file.");
                return StatusCode(500, "Internal server error");
            }

            // Redirect to Privacy view
            return RedirectToAction("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
