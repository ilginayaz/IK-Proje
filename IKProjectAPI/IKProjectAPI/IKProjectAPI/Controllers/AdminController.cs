using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IKProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPatch("YoneticiyiOnayla")]
        public async Task<IActionResult> YoneticiyiOnayla(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var sirket = _context.sirketler.Where(s => s.SirketYoneticileri.Any(y => y.Id == user.Id) && s.Status == Data.Enums.Status.AwatingApproval).FirstOrDefault();
            
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            if (sirket != null)
            {

            user.Status = Data.Enums.Status.Active;
            sirket.Status = Data.Enums.Status.Active;
            return Ok("Kullanıcı ve Şirket başarıyla onaylandı");
            }
            return BadRequest("Kullanıcıya ait Sirket Bulunamadı");
        }

        [HttpPatch("YoneticiyiReddet")]
        public async Task<IActionResult> YoneticiyiReddet(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var sirket = _context.sirketler.Where(s => s.SirketYoneticileri.Any(y => y.Id == user.Id) && s.Status == Data.Enums.Status.AwatingApproval).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            user.Status = Data.Enums.Status.Passive;
            sirket.Status = Data.Enums.Status.Passive;
            return Ok("Kullanıcı başarıyla reddedildi");
        }
    }
}
