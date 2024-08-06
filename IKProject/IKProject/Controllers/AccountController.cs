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

                    return RedirectToAction("Index", "Home");
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
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
