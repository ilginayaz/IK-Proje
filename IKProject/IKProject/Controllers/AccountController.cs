using IKProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace IKProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5240/api/Auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var tokenObj = JsonConvert.DeserializeObject<TokenResponse>(jsonData);

                    HttpContext.Session.SetString("JWTToken", tokenObj.Token);

                    var roleResponse = await _httpClient.GetAsync("http://localhost:5240/api/Auth/getroles?email="+model.Email);
                    if (roleResponse.IsSuccessStatusCode)
                    {
                        var rolesJson = await roleResponse.Content.ReadAsStringAsync();
                        var roles = JsonConvert.DeserializeObject<List<string>>(rolesJson);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else if (roles.Contains("Çalışan"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Employee" });
                        }
                        else if (roles.Contains("Yönetici"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "CompanyManager" });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Geçersiz rol.");
                        }
                    }
                    else
                    {
                        // ModelState.AddModelError(string.Empty, "Rol bilgileri alınamadı.");
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Hata: {response.StatusCode}, Mesaj: {errorMessage}");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz Giriş.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5240/api/Auth/register", content);

                if (response.IsSuccessStatusCode)
                {
                   
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                   
                    ModelState.AddModelError(string.Empty, "Kayıt başarısız. Lütfen tekrar deneyin!");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            // mail entegrasyonu eksik. 

            TempData["Message"] = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";
            return RedirectToAction("ForgotPassword");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWTToken");

            return RedirectToAction("Login", "Account");
        }

    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
