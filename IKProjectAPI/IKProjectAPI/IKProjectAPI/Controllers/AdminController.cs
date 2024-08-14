using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IKProjectAPI.NewFolder;

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
        private readonly EmailSender _emailSender;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, EmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
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
                _emailSender.SendEmailAsync(user.Email, "FHYI GROUP - Hesabınız Onaylandı", "Hesabınız onaylanmıştır");
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
            _emailSender.SendEmailAsync(user.Email, "FHYI GROUP - Hesabınız Reddedildi", "Hesabınız reddedildi üzgünüz :(");
            return Ok("Kullanıcı başarıyla reddedildi");
        }


        [HttpGet("GetAdminDetails")]
        public async Task<IActionResult> GetAdminDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("Admin bulunamadı.");
            }

            var adminDetails = new
            {
                user.ProfilePhoto,
                user.Adi,
                user.IkinciAdi,
                user.Soyadi,
                user.IkinciSoyadi,
                user.DogumTarihi,
                user.DogumYeri,
                user.TC,
                user.Adres,
                user.Cinsiyet,
                user.Email,
                user.PhoneNumber
            };

            return Ok(adminDetails);
        }
    }
}
