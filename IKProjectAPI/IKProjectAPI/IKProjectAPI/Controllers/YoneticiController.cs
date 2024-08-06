using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Yonetici")]
    public class YoneticiController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public YoneticiController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        //Çalışanların bilgilerini getiren endpoint
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return Ok(new
            {
                user.ProfilePhoto,
                user.Adi,
                user.IkinciAdi,
                user.Soyadi,
                user.IkinciSoyadi,
                user.Email,
                user.PhoneNumber,
                user.DogumTarihi,
                user.DogumYeri,
                user.TC,
                user.Sirket,
                user.Departman,
                user.Meslek,
                user.Adres,
                user.Cinsiyet
            });
        }

        // Yöneticinin çalışanlarını getir
        [HttpGet("getEmployees")]
        public async Task<IActionResult> GetEmployees(string yoneticiId)
        {
            // Öncelikle, yöneticinin bilgilerini alalım
            var yonetici = await _userManager.FindByIdAsync(yoneticiId);
            if (yonetici == null)
            {
                return NotFound("Manager not found");
            }

            // Yöneticinin çalışanlarını al
            var calisanlarIds = yonetici.Calisanlar.Select(c => c.Id).ToList();

            // Çalışanların bilgilerini filtrele
            var calisanlar = await _context.Users
                .Where(u => calisanlarIds.Contains(u.Id))
                .ToListAsync();

            return Ok(calisanlar);
        }

        //Yönetici çalışanlarının İzinler listesi
        [HttpGet("izinListesi")]
        public async Task<IActionResult> izinListesi(string yoneticiId)
        {
            var yonetici = await _userManager.FindByIdAsync(yoneticiId);
            if (yonetici == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            // Kullanıcının kendi çalışanlarını al
            var calisanlarIds = yonetici.Calisanlar.Select(c => c.Id).ToList();

            // Çalışanların izin isteklerini filtrele
            var izinIstekleri = await _context.izinIstekleri
                .Where(x => calisanlarIds.Contains(x.AppUserId))
                .ToListAsync();

            return Ok(izinIstekleri);
        }

        //Çalışan kayıt et eklenecek

        
        //Yöneticinin kendine çalışan eklemesi
        [HttpPost("assignManager")]
        public async Task<IActionResult> AssignManager(string calisanId, string yoneticiId)
        {
            var calisan = await _userManager.FindByIdAsync(calisanId);
            var yonetici = await _userManager.FindByIdAsync(yoneticiId);

            if (calisan == null || yonetici == null)
            {
                return NotFound("Çalışan veya yönetici bulunamadı.");
            }

            calisan.Yonetici = yonetici;
            yonetici.Calisanlar.Add(calisan);

            var result = await _userManager.UpdateAsync(calisan);
            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(yonetici);
                return Ok("Çalışan başarılı bir şekilde yöneticiye atandı.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPatch("izinOnayla")]
        public async Task<IActionResult> IzinOnayla(string id)
        {
            var izin = await _context.izinIstekleri.FindAsync(id);
            if (izin != null)
            {
                izin.OnayDurumu = Data.Enums.OnayDurumu.Onaylandı;
                izin.Status = Data.Enums.Status.Active;
                _context.Update(izin);
                _context.SaveChanges();
                return Ok("İzin isteği başarıyla onaylandı");
            }
            return BadRequest("Böyle bir izin bulunamadı");
        }

        //İzin Reddet 
        [HttpPatch("izinReddet")]
        public async Task<IActionResult> IzinReddet(string id)
        {
            var izin = await _context.izinIstekleri.FindAsync(id);
            if (izin != null)
            {
                izin.OnayDurumu = Data.Enums.OnayDurumu.Reddedildi;
                izin.Status = Data.Enums.Status.Passive;
                _context.Update(izin);
                await _context.SaveChangesAsync();
                return Ok("İzin isteği başarıyla reddedildi");
            }
            return BadRequest("Böyle bir izin bulunamadı");
        }

    }
}
