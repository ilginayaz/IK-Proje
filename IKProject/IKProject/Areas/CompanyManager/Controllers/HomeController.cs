using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKProject.Areas.CompanyManager.Controllers
{

    [Area("CompanyManager")]
    //[Authorize(Roles = "Yönetici")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
