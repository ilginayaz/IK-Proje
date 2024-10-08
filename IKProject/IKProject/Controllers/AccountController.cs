﻿using IKProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

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

                var response = await _httpClient.PostAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var tokenObj = JsonConvert.DeserializeObject<TokenResponse>(jsonData);

                    //HttpContext.Session.SetString("JWTToken", tokenObj.Token);

                    // JWT Token'ı bir cookie olarak saklayın
                    Response.Cookies.Append("JWTToken", tokenObj.Token, new CookieOptions
                    {

                        Expires = DateTime.UtcNow.AddHours(1),
                        IsEssential = true
                    });
                    HttpContext.Session.SetString("TokenAuth", tokenObj.Token);

                    _httpClient.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenObj.Token);

                    var roleResponse = await _httpClient.GetAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/getroles?email=" + model.Email);
                    if (roleResponse.IsSuccessStatusCode)
                    {
                        var rolesJson = await roleResponse.Content.ReadAsStringAsync();
                        var roles = JsonConvert.DeserializeObject<List<string>>(rolesJson);

                        // Token'dan claim'leri okuma ve kullanıcıyı oturum açma
                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(tokenObj.Token);

                        var claims = token.Claims.ToList();

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        

                        // Kullanıcıyı oturum açma işlemi ile kimlik doğrulama
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

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

       

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

        //        var response = await _httpClient.PostAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/register", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessMessage"] = "Kayıt başarılı. Lütfen giriş yapın.";
        //            return RedirectToAction("Login", "Account");
        //        }
        //        else
        //        {
        //            var errorMessage = await response.Content.ReadAsStringAsync();
        //            ModelState.AddModelError(string.Empty, $"Kayıt başarısız. Hata: {response.StatusCode}, Mesaj: {errorMessage}");
        //        }
        //    }

        //    return View(model);
        //}


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/register", content);

                if (response.IsSuccessStatusCode)
                {
                    
                    TempData["SuccessMessage"] = "Kayıt başarılı. Lütfen giriş yapın.";
                    return RedirectToAction("Login"); 
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Kayıt başarısız. Hata: {response.StatusCode}, Mesaj: {errorMessage}");
                }
            }

           
            return View("Login", new LoginViewModel());
        }



        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Geçersiz istek.");
            }

            // Modeli doldur
            var model = new ResetPasswordModel
            {
                Token = token,
                Email = email
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task ForgotPasswordPost(string email)
        {

                var requestUri = "https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/ForgotPassword";
                var content = new StringContent(JsonConvert.SerializeObject(new { email }), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/ForgotPassword?email={email}", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Başarı: " + result);
                    // Kullanıcıya bildirim
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Hata: " + error);
                    // Hata işleme
                }
            
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Logout isteğini API'ye gönderir
            var response = await _httpClient.PostAsync("https://ikprojectapi20240825211059.azurewebsites.net/api/Auth/logout", null);

            if (response.IsSuccessStatusCode)
            {
                // Oturumdaki tüm verileri temizler
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                // JWTToken adlı çerezi temizler
                if (HttpContext.Request.Cookies.ContainsKey("JWTToken"))
                {
                    // Çerezi sıfırlar (null), böylece tarayıcıdan silinir
                    Response.Cookies.Delete("JWTToken");
                }

                // Account/Login sayfasına yönlendir
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Çıkış işlemi başarısız.");
                return RedirectToAction("Index", "Home", new { area = "CompanyManager" });
            }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
    
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
