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

namespace IKProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            if (!ModelState.IsValid)
            {
                
                ViewBag.ErrorMessage = "Lütfen tekrar deneyiniz!";
                return View(sirketRegisterModel);
            }

            var jsonContent = JsonConvert.SerializeObject(sirketRegisterModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7149/api/Company/SirketOlustur", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CompanyList");
            }
            else
            {
                ViewBag.ErrorMessage = "Lütfen tekrar deneyiniz!";
                return View(sirketRegisterModel);
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





        [HttpGet]
        // Yönetici onaylama sayfası
        public async Task<IActionResult> YoneticiOnay()
        {
            var response = await _httpClient.GetAsync("https://localhost:7149/api/Admin/OnayBekleyenYoneticiler");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var yoneticiler = JsonConvert.DeserializeObject<List<YoneticiModel>>(content);
                var bekleyenYoneticiler = yoneticiler.Where(y => y.Status == Status.AwatingApproval).ToList();

                return View(bekleyenYoneticiler);
            }

            return View();
        }



        // Yönetici onaylama işlemi
        [HttpPost]
        public async Task<IActionResult> YoneticiOnay(string id)
        {
            var requestUri = $"https://localhost:7149/api/Admin/YoneticiyiOnayla?id={id}";
            var response = await _httpClient.PatchAsync(requestUri, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("YoneticiOnay");
            }

            return View("Hata Oluştu");
        }



        // Yönetici reddetme işlemi
        [HttpPost]
        public async Task<IActionResult> Reddet(string id)
        {
            var requestUri = $"https://localhost:7149/api/Admin/YoneticiyiReddet?id={id}";
            var response = await _httpClient.PatchAsync(requestUri, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("YoneticiOnay");
            }

            return View("Hata Oluştu");
        }



        // Onaylı yöneticiler listesi
        public async Task<IActionResult> YoneticiListe()
        {
            var response = await _httpClient.GetAsync("https://localhost:7149/api/Admin/YoneticileriListele");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var yoneticiler = JsonConvert.DeserializeObject<List<YoneticiModel>>(content);
                var onayliYoneticiler = yoneticiler.Where(y => y.Status == Status.Active).ToList();

                return View(onayliYoneticiler);
            }

            return View("Hata Oluştu");
        }




        // Yönetici silme işlemi
        [HttpPost]
        public async Task<IActionResult> Sil(string id)
        {
            var requestUri = $"https://localhost:7149/api/Admin/YoneticiyiSil?id={id}";
            var response = await _httpClient.PatchAsync(requestUri, null);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> AssignCompanyManager()
        {
            try
            {
                // Yönetici listesini al
                var yoneticilerResponse = await _httpClient.GetAsync("https://localhost:7149/api/admin/BosYoneticileriListele");
                if (!yoneticilerResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Boşta Yönetici bulunamadı.");
                }

                var yoneticilerContent = await yoneticilerResponse.Content.ReadAsStringAsync();
                var yoneticiler = JsonConvert.DeserializeObject<List<YoneticiModel>>(yoneticilerContent) ?? new List<YoneticiModel>();
                var managerList = yoneticiler.Select(manager => new SelectListItem
                {
                    Value = manager.Id.ToString(),
                    Text = $"{manager.Adi} {manager.Soyadi}"
                }).ToList();
                ViewBag.Yoneticiler = managerList;

                // Şirket listesini al
                var sirketlerResponse = await _httpClient.GetAsync("https://localhost:7149/api/Company/sirketListele");
                if (!sirketlerResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Şirket listesi yüklenirken hata oluştu.");
                }

                var sirketlerContent = await sirketlerResponse.Content.ReadAsStringAsync();
                var sirketler = JsonConvert.DeserializeObject<List<Sirket>>(sirketlerContent) ?? new List<Sirket>();
                var sirketList = sirketler.Select(sirket => new SelectListItem
                {
                    Value = sirket.Id.ToString(),
                    Text = $"{sirket.SirketNumarasi} {sirket.SirketAdi}"
                }).ToList();
                ViewBag.Sirketler = sirketList;
            }
            catch (Exception ex)
            {
                // Hata işleme ve loglama
                Console.WriteLine(ex.Message); // veya Debug.WriteLine(ex.Message);
                                               // Kullanıcıya uygun bir hata mesajı göster
                ViewBag.ErrorMessage = ex.Message;
            }

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AssignCompanyManager(AssignCompanyManagerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // API'ye istek gönderme
            var requestUrl = "https://localhost:7149/api/Admin/assignCompanyManager"; 

            var requestData = new
            {
                SirketId = model.SirketId,
                YoneticiId = model.YoneticiId
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7149/api/Admin/assignCompanyManager", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Yönetici başarıyla şirkete atandı.";
                return RedirectToAction("AssignCompanyManager");
            }

            TempData["ErrorMessage"] = "Yönetici ataması başarısız oldu.";
            return View(model);
        }
    

    public IActionResult Success()
        {
            return View();
        }
        public IActionResult Unsuccess()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddManager()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddManager(YoneticiRegisterModel model)
        {
            //ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7149/api/admin/register", content);
                

                if (response.IsSuccessStatusCode)
                {

                    TempData["SuccessMessage"] = "Kayıt başarılı.";
                    return RedirectToAction("YoneticiOnay");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Kayıt başarısız. Hata: {response.StatusCode}, Mesaj: {errorMessage}");
                }
            }


            return View("AddManager", model);
        }

    }
}
