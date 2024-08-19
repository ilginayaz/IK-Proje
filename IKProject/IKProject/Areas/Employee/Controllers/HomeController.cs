using IKProject.Data.Concrete;
using IKProject.Data.Enums;
using IKProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace IKProject.Areas.Employee.Controllers
{

    [Area("Employee")]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userResult = await GetUser(userId);
            if (userResult is OkObjectResult okResult && okResult.Value is ApplicationUser user)
            {


                return View(user);
            }

            return NotFound("Kullanıcı bulunamadı.");
        }
        public async Task<IActionResult> GetUser(string userId)
        {


            var response = await _httpClient.GetAsync($"http://localhost:5240/api/Auth/getUser?userId={userId}");
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(content); //
                return Ok(user);
            }
            return NotFound("Personel bulunamadı.");
        }
    }
}
