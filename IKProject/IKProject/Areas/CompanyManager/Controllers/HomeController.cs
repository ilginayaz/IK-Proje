using IKProject.Data.Concrete;
using IKProject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace IKProjectMVC.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles ="Yönetici")]
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

            ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CalisanSil(string email)
        {
            var response = await _httpClient.PatchAsync($"https://localhost:7149/api/Yonetici/CalisanSil?Email={email}", null);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Personel başarıyla silindi." });
            }

            return Json(new { success = false, message = "Silme işlemi başarısız oldu." });
        }

        public async Task<IActionResult> ProfilDetay()
        {
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameIdentifier")?.Value;
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/getEmployees?yoneticiId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<ApplicationUser>>(content);
                return View(employees);
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                return View();
            }
        }


        //çalışanların bilgilerini getiren istek
        public async Task<IActionResult> GetUser(string userId)
        {


            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/getUser?userId={userId}");
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
            
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameIdentifier")?.Value;
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/getEmployees?yoneticiId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<ApplicationUser>>(content);
                return View(employees);
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                return View();
            }
        }
        // izinler listesi 
        public async Task<IActionResult> Izinler()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/izinListesi?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<IzinIstegi>>(content);

                foreach (var item in izinListesi)
                {
                    item.IzinGunSayisi = (item.BitisTarihi - item.BaslangicTarihi).Days;

                }

                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }
        public async Task<IActionResult> OnaylananIzinler()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7149/api/Yonetici/OnaylananIzinler?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<IzinIstegi>>(content);

                foreach (var item in izinListesi)
                {
                    item.IzinGunSayisi = (item.BitisTarihi - item.BaslangicTarihi).Days;

                }

                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }
        public async Task<IActionResult> ReddedilenIzinler()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7149/api/Yonetici/ReddedilenIzinler?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<IzinIstegi>>(content);

                foreach (var item in izinListesi)
                {
                    item.IzinGunSayisi = (item.BitisTarihi - item.BaslangicTarihi).Days;

                }

                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }
        public async Task<IActionResult> OnaylananAvanslar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7149/api/Yonetici/OnaylananIzinler?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<AvansTalep>>(content);

               
                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }
        public async Task<IActionResult> ReddedilenAvanslar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7149/api/Yonetici/ReddedilenIzinler?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<AvansTalep>>(content);

               
                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }

        // avanslar listesi 
        public async Task<IActionResult> Avanslar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/avansListesi?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<AvansTalep>>(content);

                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }






        // Harcama listesi 
        public async Task<IActionResult> Harcamalar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/harcamaListesi?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<HarcamaTalep>>(content);

                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }
        public async Task<IActionResult> OnaylananHarcamalar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7149/api/Yonetici/OnaylananHarcamalar?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<HarcamaTalep>>(content);

                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }
        public async Task<IActionResult> ReddedilenHarcamalar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7149/api/Yonetici/ReddedilenHarcamalar?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<HarcamaTalep>>(content);

                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
            }
        }

        // izinler listesi 
        public async Task<IActionResult> IzinListesi()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/izinListesi?managerId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var izinListesi = JsonConvert.DeserializeObject<List<IzinIstegi>>(content);

              
                return View(izinListesi);
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View();
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

            var response = await _httpClient.PostAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/register", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Personel başarıyla kaydedildi.";
                return RedirectToAction("RegisterEmployee");
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return View(registerModel);
            }
        }

        // izin onaylama
        [HttpGet]
        public async Task<IActionResult> OnaylaIzin(int Id)
        {
            var response = await _httpClient.PatchAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/izinOnayla?id={Id}", null);
            if (Id == 0)
            {
                ViewBag.ErrorMessage = "Geçersiz izin ID'si.";
                return RedirectToAction("Izinler");
            }

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "İzin başarıyla onaylandı.";
                return RedirectToAction("Izinler");
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return RedirectToAction("Izinler");
            }
        }

        // izin reddetme
        [HttpGet]
        public async Task<IActionResult> ReddetIzin(int Id)
        {
            var response = await _httpClient.PatchAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/izinReddet?id={Id}", null);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "İzin başarıyla reddedildi.";
                return RedirectToAction("Izinler");
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return RedirectToAction("Izinler");
            }
        }

        // avans onaylama
        [HttpGet("OnaylaAvans/{id}")]
        public async Task<IActionResult> OnaylaAvans(int Id)
        {
            var response = await _httpClient.PatchAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/avansOnayla?id={Id}", null);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Avans başarıyla onaylandı.";
                return RedirectToAction("Avanslar");
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return RedirectToAction("Avanslar");
            }
        }

        // avans reddetme
        [HttpGet]
        public async Task<IActionResult> ReddetAvans(int Id)
        {
            var response = await _httpClient.PatchAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/avansReddet?id={Id}", null);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Avans başarıyla reddedildi.";
                return RedirectToAction("Avanslar");
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return RedirectToAction("Avanslar");
            }
        }

        // harcama onaylama
        [HttpGet]
        public async Task<IActionResult> OnaylaHarcama(int Id)
        {
            var response = await _httpClient.PatchAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/harcamaOnayla?id={Id}", null);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Harcama başarıyla onaylandı.";
                return RedirectToAction("Harcamalar");
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return RedirectToAction("Harcamalar");
            }
        }

        // harcama reddetme
        [HttpGet]
        public async Task<IActionResult> ReddetHarcama(int Id)
        {
            var response = await _httpClient.PatchAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Yonetici/harcamaReddet?id={Id}", null);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Harcama başarıyla reddedildi.";
                return RedirectToAction("Harcamalar");
            }
            else
            {
                ViewBag.ErrorMessage = "Tekrar deneyin!";
                return RedirectToAction("Harcamalar");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/GetUser?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<UpdateProfileViewModel>(content);
                return View(model);
            }
            else
            {
                
                ViewBag.ErrorMessage = "Profil bilgileri alınamadı.";
                return View(new UpdateProfileViewModel());
               
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PatchAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Calisan/UpdateUser", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMessage = "Profiliniz başarıyla güncellendi.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Profil güncellenemedi: {errorMessage}";
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
            var response = await _httpClient.PostAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/ChangePassword", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Şifreniz başarıyla değiştirildi.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = $"Şifre değişikliği başarısız oldu: {errorMessage}";
                return View(model);
            }
        }

    }
}
