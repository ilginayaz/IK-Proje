using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CompanyRegister()
        {
            return View();
        }
        public IActionResult CompanyList() 
        {
            return View();
        }
    }
}
