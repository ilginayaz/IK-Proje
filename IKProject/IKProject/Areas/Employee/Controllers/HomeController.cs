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
        public async Task<ApplicationUser> GetApplicationUser(string userId)
        {


            var response = await _httpClient.GetAsync($"http://localhost:5240/api/Auth/getUser?userId={userId}");
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(content); //
                return user;
            }
            return null;
        }
        [HttpGet]
        public async Task<IActionResult> Izinler()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var izinler = await _httpClient.GetFromJsonAsync<List<IzinIstegi>>($"https://localhost:7149/api/Calisan/izinGetById?userId={userId}");

            if (izinler == null)
            {
                TempData["ErrorMessage"] = "İzinler alınamadı.";
                return RedirectToAction("Index"); // Hata durumunda ana sayfaya yönlendir
            }

            return View(izinler);
        }
        [HttpGet]
        public async Task<IActionResult> Harcamalar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var harcamalar = await _httpClient.GetFromJsonAsync<List<HarcamaTalepViewModel>>($"https://localhost:7149/api/Calisan/HarcamaGetById?userId={userId}");

            if (harcamalar == null)
            {
                TempData["ErrorMessage"] = "Harcamalar alınamadı.";
                return RedirectToAction("Index"); // Hata durumunda ana sayfaya yönlendir
            }

            return View(harcamalar);
        }
        [HttpGet]
        public async Task<IActionResult> Avanslar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var avanslar = await _httpClient.GetFromJsonAsync<List<AvansTalepViewModel>>($"https://localhost:7149/api/Calisan/AvansGetById?userId={userId}");

            if (avanslar == null)
            {
                TempData["ErrorMessage"] = "Avanslar alınamadı.";
                return RedirectToAction("Index"); // Hata durumunda ana sayfaya yönlendir
            }

            return View(avanslar);
        }



        [HttpGet]
        public IActionResult IzinOlustur()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IzinOlustur(IzinIstegiViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                TempData["Message"] = "Kullanıcı bulunamadı";
                return View(model);
            }
            model.ApplicationUserId = userId;
            ModelState.Remove("ApplicationUserId");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"https://localhost:7149/api/Calisan/IzinOlustur?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "İzin başarıyla oluşturulmuştur.";
                return RedirectToAction("Izinler");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult IzinGuncelle(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IzinGuncelle(IzinIstegiViewModel model)
        {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync($"https://localhost:7149/api/Calisan/izinGuncelle?userId={userId}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "İzin isteği başarıyla güncellenmiştir.";
                    return RedirectToAction("Izinler");  // İzinler listesi sayfasına yönlendir
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                    return View(model);
                }
        }
        [HttpPost]
        public async Task<IActionResult> IzinSil(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var content = new StringContent(JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"https://localhost:7149/api/Calisan/izinSil?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "İzin başarıyla silinmiştir.";
                return RedirectToAction("Izinler");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return RedirectToAction("Izinler");
            }
        }

        [HttpGet]
        public IActionResult HarcamaOlustur()
        {
            var model = new HarcamaTalepViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.ApplicationUserId = userId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> HarcamaOlustur(HarcamaTalepViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"https://localhost:7149/api/Calisan/HarcamaOlustur?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Harcama başarıyla oluşturulmuştur.";
                return RedirectToAction("Harcamalar");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult HarcamaGuncelle(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HarcamaGuncelle(HarcamaTalepViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"https://localhost:7149/api/Calisan/HarcamaGuncelle?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Harcama isteği başarıyla güncellenmiştir.";
                return RedirectToAction("Harcamalar");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult HarcamaSil(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HarcamaSil(HarcamaTalepViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"https://localhost:7149/api/Calisan/HarcamaSil?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Harcama isteği başarıyla silinmiştir.";
                return RedirectToAction("Harcamalar");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AvansOlustur()
        {
            var model = new AvansTalepViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.ApplicationUserId = userId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AvansOlustur(AvansTalepViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await GetApplicationUser(userId);
            model.ApplicationUserId = userId;
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bilgileri alınamadı.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            // Maaşın avans talebinin 3 katından fazla olup olmadığını kontrol et
            if (user.Maas > model.Tutar * 3)
            {
                ModelState.AddModelError(string.Empty, "Avans talebi maaşın 3 katından fazla olamaz.");
                return View(model);
            }

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"https://localhost:7149/api/Calisan/AvansOlustur?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Avans talebi başarıyla oluşturulmuştur.";
                return RedirectToAction("Avanslar");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult AvansGuncelle(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AvansGuncelle(AvansTalepViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"https://localhost:7149/api/Calisan/AvansGuncelle?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Avans isteği başarıyla güncellenmiştir.";
                return RedirectToAction("Avanslar");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AvansSil(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var content = new StringContent(JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"https://localhost:7149/api/Calisan/AvansSil?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Avans başarıyla silinmiştir.";
                return RedirectToAction("Avanslar");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return RedirectToAction("Avanslar");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BilgiGuncelle()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7149/api/Auth/GetUser?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<UpdateProfileViewModel>(content);
                return View(model);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Profil bilgileri alınamadı.");
                return View(new UpdateProfileViewModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> BilgiGuncelle(UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PatchAsync("https://localhost:7149/api/Calisan/UpdateUser", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Profiliniz başarıyla güncellendi.";

                   
                    return View(model);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Profil güncellenemedi: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Bir hata oluştu: {ex.Message}");
            }

            return View(model);
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var changePasswordModel = new ChangePasswordViewModel
            {
                OldPassword = model.OldPassword,
                NewPassword = model.NewPassword
            };

            var content = new StringContent(JsonConvert.SerializeObject(changePasswordModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7149/api/Auth/ChangePassword", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Şifre değişikliği başarısız oldu: {errorMessage}");
                return View(model);
            }
        }

    }
}
