using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKProject.Areas.CompanyManager.Controllers
{

    [Area("CompanyManager")]
    [Authorize(Roles = "YONETICI")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
