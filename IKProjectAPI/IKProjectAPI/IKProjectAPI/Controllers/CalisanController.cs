using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data.Models;
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
        public async Task<IActionResult> UpdateUser(UpdateProfileModel userModel)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user != null)
            {
                user.ProfilePhoto = userModel.ProfilePhoto;
                user.Adres = userModel.Adres;
                user.PhoneNumber = userModel.TelefonNumarasi;
                user.Status = Data.Enums.Status.Active;
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
        public async Task<IActionResult> izinOlustur(string userId, IzinIstegiViewModel model)
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
            var izinIstegi = new IzinIstegi();
            izinIstegi.ApplicationUserId = user.Id;
            izinIstegi.Status = Data.Enums.Status.AwatingApproval;
            izinIstegi.OnayDurumu = Data.Enums.OnayDurumu.Beklemede;
            izinIstegi.IzinTuru = model.IzinTuru;
            izinIstegi.IstekYorumu = model.IstekYorumu;
            izinIstegi.BaslangicTarihi = model.BaslangicTarihi;
            izinIstegi.BitisTarihi =model.BitisTarihi;
            izinIstegi.IzinGunSayisi = model.IzinGunSayisi;
            _context.izinIstekleri.Add(izinIstegi);
            _context.SaveChanges();
            _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi} {izinIstegi.IzinGunSayisi} günlük izniniz oluşturulmuştur. İyi günler!");
            return Ok("İzin isteği başarıyla oluşturuldu");
        }
        [HttpPatch("izinGuncelle")]
        public async Task<IActionResult> izinGuncelle(string userId, IzinIstegiViewModel izinIstegi)
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
        public async Task<IActionResult> izinSil(string userId, IzinIstegiViewModel izinIstegi)
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
                var list = _context.izinIstekleri.Where(x=> x.ApplicationUserId == userId).ToList();
                
                return Ok(list);
            }
            return BadRequest("Kullanıcı bulunamadı");
        }


        [HttpPost("HarcamaOlustur")]
        public async Task<IActionResult> HarcamaOlustur(string userId, HarcamaTalepViewModel model)
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
            var izinIstegi = new HarcamaTalep();
            izinIstegi.ApplicationUserId = user.Id;
            izinIstegi.Status = Data.Enums.Status.AwatingApproval;
            izinIstegi.OnayDurumu = Data.Enums.OnayDurumu.Beklemede;
            izinIstegi.Aciklama = model.Aciklama;
            izinIstegi.GiderTutari = model.GiderTutari;
            izinIstegi.ParaBirimi = model.ParaBirimi;
            izinIstegi.MasrafTarihi = model.MasrafTarihi;
            _context.HarcamaTalepleri.Add(izinIstegi);
            _context.SaveChanges();
            _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi}  harcamanız oluşturulmuştur. İyi günler!");
            return Ok("İzin isteği başarıyla oluşturuldu");
        }
        [HttpPatch("HarcamaGuncelle")]
        public async Task<IActionResult> HarcamaGuncelle(string userId, HarcamaTalepViewModel izinIstegi)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var izin = _context.HarcamaTalepleri.Find(izinIstegi.Id);
            if (user != null)
            {
                if (izin != null)
                {
                    izin.UpdatedTime = DateTime.Now;
                    izin.MasrafTarihi = izinIstegi.MasrafTarihi;
                    izin.ParaBirimi = izinIstegi.ParaBirimi;
                    izin.Aciklama = izinIstegi.Aciklama;
                    izin.GiderTutari = izinIstegi.GiderTutari;
                    _context.Update(izin);
                    _context.SaveChanges();
                    _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi}  harcamanız güncellenmiştir. İyi günler!");
                    return Ok("İzin isteği başarıyla güncellenmiştir.");
                }
                return BadRequest("Böyle bir izin bulunamadı");
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
        [HttpPatch("HarcamaSil")]
        public async Task<IActionResult> HarcamaSil(string userId, HarcamaTalepViewModel izinIstegi)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var izin = _context.HarcamaTalepleri.Find(izinIstegi.Id);
            if (user != null)
            {
                if (izin != null)
                {
                    izin.UpdatedTime = DateTime.Now;
                    izin.DeletedTime = DateTime.Now;
                    izin.Status = Data.Enums.Status.Passive;
                    _context.Update(izin);
                    _context.SaveChanges();
                    _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi} harcamanız izniniz silinmiştir. İyi günler!");
                    return Ok("İzin isteği başarıyla silinmiştir..");
                }
                return BadRequest("Böyle bir izin bulunamadı");
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
        [HttpGet("HarcamaGetById")]
        public async Task<IActionResult> HarcamaGet(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var list = _context.HarcamaTalepleri.Where(x => x.ApplicationUserId == userId).ToList();
                return Ok(list);
            }
            return BadRequest("Kullanıcı bulunamadı");
        }

        [HttpPost("AvansOlustur")]
        public async Task<IActionResult> AvansOlustur(string userId, AvansTalpeViewModel model)
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
            var izinIstegi = new AvansTalep();
            izinIstegi.ApplicationUserId = user.Id;
            izinIstegi.Status = Data.Enums.Status.AwatingApproval;
            izinIstegi.OnayDurumu = Data.Enums.OnayDurumu.Beklemede;
            izinIstegi.TalepTarihi = model.TalepTarihi;
            izinIstegi.Tutar = model.Tutar;
            izinIstegi.ParaBirimi = model.ParaBirimi;
            izinIstegi.Aciklama =  model.Aciklama;

            _context.AvansTalepleri.Add(izinIstegi);
            _context.SaveChanges();
            _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi}  avans talebiniz  oluşturulmuştur. İyi günler!");
            return Ok("İzin isteği başarıyla oluşturuldu");
        }
        [HttpPatch("AvansGuncelle")]
        public async Task<IActionResult> AvansGuncelle(string userId, AvansTalpeViewModel izinIstegi)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var izin = _context.AvansTalepleri.Find(izinIstegi.Id);
            if (user != null)
            {
                if (izin != null)
                {
                    izin.UpdatedTime = DateTime.Now;
                    izin.TalepTarihi = izinIstegi.TalepTarihi;
                    izin.ParaBirimi = izinIstegi.ParaBirimi;
                    izin.Aciklama = izinIstegi.Aciklama;
                    izin.Tutar = izinIstegi.Tutar;
                    _context.Update(izin);
                    _context.SaveChanges();
                    _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi}  avans talebiniz güncellenmiştir. İyi günler!");
                    return Ok("İzin isteği başarıyla güncellenmiştir.");
                }
                return BadRequest("Böyle bir izin bulunamadı");
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
        [HttpPatch("AvansSil")]
        public async Task<IActionResult> AvansSil(string userId, AvansTalpeViewModel izinIstegi)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var izin = _context.AvansTalepleri.Find(izinIstegi.Id);
            if (user != null)
            {
                if (izin != null)
                {
                    izin.UpdatedTime = DateTime.Now;
                    izin.DeletedTime = DateTime.Now;
                    izin.Status = Data.Enums.Status.Passive;
                    _context.Update(izin);
                    _context.SaveChanges();
                    _emailSender.SendEmailAsync(user.Email, "FHYI Group - İzin İsteği", $"Sevgili çalışanımız {user.Adi} {user.Soyadi} avans talebiniz  silinmiştir. İyi günler!");
                    return Ok("İzin isteği başarıyla silinmiştir..");
                }
                return BadRequest("Böyle bir izin bulunamadı");
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
        [HttpGet("AvansGetById")]
        public async Task<IActionResult> AvansGet(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var list = _context.AvansTalepleri.Where(x => x.ApplicationUserId == userId).ToList();
                return Ok(list);
            }
            return BadRequest("Kullanıcı bulunamadı");
        }
    }
}
