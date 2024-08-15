using IKProject.Data.Concrete;
using IKProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace IKProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task <IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userResult = await GetUser(userId);
            if (userResult is OkObjectResult okResult && okResult.Value is ApplicationUser user)
            {
                
               
                return View(user);
            }

            return NotFound("Kullanıcı bulunamadı.");
        }
        [HttpGet]
        public async Task<IActionResult> CompanyRegister()
        {
            var response = await _httpClient.GetAsync("https://localhost:7149/api/admin/YoneticileriListele");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var yoneticiler = JsonConvert.DeserializeObject<List<YoneticiModel>>(content);

                var managerList = yoneticiler.Select(manager => new SelectListItem
                {
                    Value = manager.Id.ToString(),
                    Text = $"{manager.Adi} {manager.Soyadi}"
                }).ToList();
                ViewBag.Yoneticiler = managerList;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CompanyRegister(SirketRegisterModel sirketRegisterModel)
        {
           
            var jsonContent = JsonConvert.SerializeObject(sirketRegisterModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7149/api/Company/SirketOlustur", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("RegisterEmployee");
            }
            else
            {
                return View("Tekrar deneyin!");
            }
        }
        [HttpGet]
        public async  Task<IActionResult> CompanyList() 
        {
            var response = await _httpClient.GetAsync("https://localhost:7149/api/Company/sirketListele");

            if (response.IsSuccessStatusCode)
            {
               var content = await response.Content.ReadAsStringAsync();
                var sirketler = JsonConvert.DeserializeObject<List<Sirket>>(content);
                return View(sirketler);
            }
            return View("Hata Oluştu");
        }
        public async Task<List<SelectListItem>> ManagerList()
        {
            var response = await _httpClient.GetAsync("https://localhost:7149/api/admin/YoneticileriListele");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var yoneticiler = JsonConvert.DeserializeObject<List<YoneticiModel>>(content);
                
                var managerList = yoneticiler.Select(manager => new SelectListItem
                {
                    Value = manager.Id.ToString(),
                    Text = $"{manager.Adi} {manager.Soyadi}"
                }).ToList();

                return managerList;
                
            }
            return null;
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
