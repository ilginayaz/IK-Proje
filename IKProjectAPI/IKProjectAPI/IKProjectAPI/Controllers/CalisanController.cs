using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using IKProjectAPI.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IKProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Yonetici,Calisan")]
    public class CalisanController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly EmailSender _emailSender;

        public CalisanController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, EmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
        }
        //kullanıcıyı güncelle
        [HttpPatch("UpdateUser")]
        public async Task<IActionResult> UpdateUser(ApplicationUser userModel)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user != null)
            {
                user.ProfilePhoto = userModel.ProfilePhoto;
                user.Adres = userModel.Adres;
                user.PhoneNumber = userModel.PhoneNumber;
                user.Status = Data.Enums.Status.Active;
                //user.UpdatedTime = DateTime.Now;
                //user.Adi = userModel.Adi;
                //user.IkinciAdi = userModel.IkinciAdi;
                //user.Soyadi = userModel.Soyadi;
                //user.IkinciSoyadi = userModel.IkinciSoyadi;
                //user.DogumTarihi = userModel.DogumTarihi;
                //user.DogumYeri = userModel.DogumYeri;
                //user.TC = userModel.TC;
                //user.Sirket = userModel.Sirket;
                //user.Departman = userModel.Departman;
                //user.Meslek = userModel.Meslek;
                //user.Cinsiyet = userModel.Cinsiyet;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok("Kullanıcı başarıyla güncellendi");
                }
                else { return BadRequest("Başarısız"); }
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
        //kullanıcı sil --Databaseden silmez, status'ü pasife çeker
        [HttpPatch("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.DeletedTime = DateTime.Now;
                user.Status = Data.Enums.Status.Passive;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok("Kullanıcı başarıyla silindi");
                }
                else { return BadRequest("Başarısız"); }
            }
            return BadRequest("Kullanıcı bulunamadı");
        }

        
        // Kullanıcı bilgilerini getiren endpoint
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindAll(ClaimTypes.NameIdentifier).Last().Value;
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

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
        [HttpPost("izinOlustur")]
        public async Task<IActionResult> IzinCreate(string userId, IzinIstegi izinIstegi, int tipId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("İstek bilgileri hatalı");
            }
            izinIstegi.IzinTipiId = tipId;
            izinIstegi.AppUserId = user.Id;
            izinIstegi.Status = Data.Enums.Status.AwatingApproval;
            izinIstegi.OnayDurumu = Data.Enums.OnayDurumu.Beklemede;
            _context.izinIstekleri.Add(izinIstegi);
            _context.SaveChanges();
            _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi} {izinIstegi.IzinGunSayisi} günlük izniniz oluşturulmuştur. İyi günler!");
            return Ok("İzin isteği başarıyla oluşturuldu");
        }
        [HttpPatch("izinGuncelle")]
        public async Task<IActionResult> IzinUpdate(string userId, IzinIstegi izinIstegi)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var izin = _context.izinIstekleri.Find(izinIstegi.Id);
            if (user != null)
            {
                if (izin != null)
                {
                    izin.UpdatedTime = DateTime.Now;
                    izin.BitisTarihi = izinIstegi.BitisTarihi;
                    izin.BaslangicTarihi = izinIstegi.BaslangicTarihi;
                    izin.IzinTipiId = izinIstegi.IzinTipiId;
                    _context.Update(izin);
                    _context.SaveChanges();
                    _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi} {izinIstegi.IzinGunSayisi} günlük izniniz güncellenmiştir. İyi günler!");
                    return Ok("İzin isteği başarıyla güncellenmiştir.");
                }
                return BadRequest("Böyle bir izin bulunamadı");
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
        [HttpPatch("izinSil")]
        public async Task<IActionResult> IzinDelete(string userId, IzinIstegi izinIstegi)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var izin = _context.izinIstekleri.Find(izinIstegi.Id);
            if (user != null)
            {
                if (izin != null)
                {
                    izin.UpdatedTime = DateTime.Now;
                    izin.DeletedTime = DateTime.Now;
                    izin.Status = Data.Enums.Status.Passive;
                    _context.Update(izin);
                    _context.SaveChanges();
                    _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi} {izinIstegi.IzinGunSayisi} günlük izniniz silinmiştir. İyi günler!");
                    return Ok("İzin isteği başarıyla silinmiştir..");
                }
                return BadRequest("Böyle bir izin bulunamadı");
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
        [HttpGet("izinGetById")]
        public async Task<IActionResult> IzinGet(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var list = _context.izinIstekleri.Where(x => x.AppUserId == userId).ToList();
                return Ok(list);
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
    }
}
