using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKProject.Areas.Employee.Controllers
{

    [Area("Employee")]
    [Authorize(Roles = "Çalışan")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
