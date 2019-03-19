using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("upload")]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(string data)
        {
            return Ok(await Upload(data));
        }

        private async Task<IActionResult> Upload(string data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");
            var response = await client.PostAsJsonAsync("api/values", data);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
