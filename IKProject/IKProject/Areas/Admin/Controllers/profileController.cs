﻿using IKProject.Areas.Admin.Models;
using IKProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace IKProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {

        private HttpClient _httpClient;

        public ProfileController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit()
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
                ModelState.AddModelError(string.Empty, "Profil bilgileri alınamadı.");
                return View(new UpdateProfileViewModel());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileViewModel model)
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
                    TempData["SuccessMessage"] = "Profiliniz başarıyla güncellendi.";
                    return RedirectToAction("Details", "Profile");
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


        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://ikprojectapi20240825211059.azurewebsites.net/api/Admin/GetAdminDetails?id={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var adminDetails = JsonConvert.DeserializeObject<AdminDetailsViewModel>(content);
                return View(adminDetails);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Admin bilgileri alınamadı.");
                return View();
            }
        }
       

    }

}
