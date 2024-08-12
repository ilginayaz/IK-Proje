using IKProject.Data.Concrete;
using IKProject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace IKProjectMVC.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]

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
                // İzin listesi almak için async metot çağrısı yapın
                var izinListesi = await IzinListesi(); // IzinleriGetir aslında veriyi döndürmelidir
                ViewBag.izinler = izinListesi;
                return View(user);
            }

            return NotFound("Kullanıcı bulunamadı.");
        }
        public IActionResult ProfilDetay()
        {
            return View();
        }


        //çalışanların bilgilerini getiren istek
        public async Task<IActionResult> GetUser(string userId)
        {


            var response = await _httpClient.GetAsync($"http://localhost:5240/api/Yonetici/getUser?userId={userId}");
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(content); //
                return Ok(user);
            }
            return NotFound("Personel bulunamadı.");
        }

        // yöneticinin çalışanları
        public async Task<IActionResult> GetEmployees()
        {
            var response = await _httpClient.GetAsync("http://localhost:5240/api/Yonetici/getEmployees");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<ApplicationUser>>(content);
                return View(employees);
            }
            else
            {
                return View("Personel Bulunamadı!");
            }
        }

        // izinler listesi 
        public async Task<IActionResult> IzinListesi()
        {
            var response = await _httpClient.GetAsync("http://localhost:5240/api/Yonetici/izinListesi");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<IzinIstegi>>(content);
                return View(izinListesi);
            }
            else
            {
                return View("Tekrar deneyin!");
            }
        }
        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        // yeni personel kayıt
        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(CalisanRegisterModel registerModel)
        {
            
            registerModel.YoneticiId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var jsonContent = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5240/api/Yonetici/register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetEmployees");
            }
            else
            {
                return View("Tekrar deneyin!");
            }
        }

        // izin onaylama
        [HttpPatch]
        public async Task<IActionResult> OnaylaIzin(string izinId)
        {
            var response = await _httpClient.PatchAsync($"http://localhost:5240/api/Yonetici/izinOnayla?id={izinId}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("IzinListesi");
            }
            else
            {
                return View("Tekrar deneyin!");
            }
        }

        // izin reddetme
        [HttpPatch]
        public async Task<IActionResult> ReddetIzin(string izinId)
        {
            var response = await _httpClient.PatchAsync($"http://localhost:5240/api/Yonetici/izinReddet?id={izinId}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("IzinListesi");
            }
            else
            {
                return View("Tekrar deneyin!");
            }
        }
       
    }
}
