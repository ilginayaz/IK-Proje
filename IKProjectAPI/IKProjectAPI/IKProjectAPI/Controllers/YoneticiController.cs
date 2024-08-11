using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data.Enums;
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
    
    public class YoneticiController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly EmailSender _emailSender;

        public YoneticiController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, EmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
        }
        //Çalışanların bilgilerini getiren endpoint
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok(new
            {
                Email = user.Email,
                ProfilePhoto = user.ProfilePhoto,
                Adi = user.Adi,
                IkinciAdi = user.IkinciAdi,
                Soyadi = user.Soyadi,
                IkinciSoyadi = user.IkinciSoyadi,
                TelefonNumarasi = user.PhoneNumber,
                DogumTarihi = user.DogumTarihi,
                DogumYeri = user.DogumYeri,
                TC = user.TC,
                IseGirisTarihi = user.IseGirisTarihi,
                IstenCikisTarihi = user.IstenCikisTarihi,
                Sirket = user.Sirket,
                Meslek = user.Meslek,
                Departman = user.Departman,
                Adres = user.Adres,
                Maas = user.Maas,
                Cinsiyet = user.Cinsiyet,
                Token = user.Token,
                CreatedTime = user.CreatedTime,
                UpdatedTime = user.UpdatedTime,
                DeletedTime = user.DeletedTime,
                Status = user.Status,
                YoneticiId = user.YoneticiId,
                Yonetici = user.Yonetici,
                Calisanlar = user.Calisanlar,
                Izinler = user.Izinler
            });
        }



        // Yöneticinin çalışanlarını getir
        [HttpGet("getEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            // Öncelikle, yöneticinin bilgilerini alalım
            var claims = User.Claims.ToList();
            var yoneticiId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var yonetici = await _userManager.FindByIdAsync(yoneticiId);
            if (yonetici == null)
            {
                return NotFound("Manager not found");
            }

            // Yöneticinin çalışanlarını al
            var calisanlarIds = yonetici.Calisanlar.Select(c => c.Id).ToList();

            // Çalışanların bilgilerini filtrele
            var calisanlar = await _context.Users
                .Where(u => calisanlarIds.Contains(u.Id) && u.Status != Status.Passive)
                .ToListAsync();

            return Ok(calisanlar);
        }

        //Yönetici çalışanlarının İzinler listesi
        [HttpGet("izinListesi")]
        public async Task<IActionResult> IzinListesi()
        {
            var claims = User.Claims.ToList();
            var yoneticiId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var yonetici = await _userManager.FindByIdAsync(yoneticiId);
            if (yonetici == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            // Kullanıcının kendi çalışanlarını al
            var calisanlarIds = yonetici.Calisanlar.Select(c => c.Id).ToList();

            // Çalışanların izin isteklerini filtrele
            var izinIstekleri = await _context.izinIstekleri
                .Where(x => calisanlarIds.Contains(x.AppUserId) && x.OnayDurumu != OnayDurumu.Reddedildi && x.Status != Status.Passive)
                .ToListAsync();
            
            return Ok(izinIstekleri);
        }

        //Çalışan kayıt et
        [HttpPost("register")]
        public async Task<IActionResult> Register( CalisanRegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TC kimlik numarası veya e-posta adresiyle daha önce kayıt yapılmış mı?
            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
            if (existingUser != null)
            {
                return BadRequest("Bu e-posta adresi zaten kullanılıyor.");
            }

            if (_context.Users.Any(u => u.TC == registerModel.TC))
            {
                return BadRequest("Bu TC kimlik numarası ile zaten bir kullanıcı kayıtlı.");
            }

            var user = new ApplicationUser
            {
                Email = registerModel.Email,
                ProfilePhoto = registerModel.ProfilePhoto,
                Adi = registerModel.Adi,
                IkinciAdi = registerModel.IkinciAdi,
                Soyadi = registerModel.Soyadi,
                IkinciSoyadi = registerModel.IkinciSoyadi,
                PhoneNumber = registerModel.TelefonNumarasi,
                DogumTarihi = registerModel.DogumTarihi,
                DogumYeri = registerModel.DogumYeri,
                TC = registerModel.TC,
                Departman = registerModel.Departman,
                Meslek = registerModel.Meslek,
                Adres = registerModel.Adres,
                //PasswordHash = registerModel.Password,
                Cinsiyet = registerModel.Cinsiyet,
                UserName = registerModel.Email,
                Token = string.Empty,
                Status = Data.Enums.Status.AwatingApproval,
            };
            var claims = User.Claims.ToList();
            var yoneticiId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var yonetici = await _userManager.FindByIdAsync(yoneticiId);
            if (yonetici != null)
            {
                yonetici.Sirket.SirketCalisanlari.Add(user);
                user.SirketId = yonetici.Sirket.Id;

            }

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                //Kayıt başarılı, token dönebiliriz veya sadece başarılı yanıtı dönebiliriz
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                _context.SaveChanges();
                var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token = token }, Request.Scheme);

                await _emailSender.SendEmailAsync(user.Email, "FHYI GROUP HOŞGELDİNİZ", $"Sayın {user.Adi} {user.Soyadi} Sistemimize hoşgeldiniz, {yonetici.Adi} {yonetici.Soyadi} tarafından sisteme kaydedildiniz. İyi günler!");

                // Kullanıcının bilgilerini içeren e-posta içeriği
                string emailBody = $"Sayın {user.Adi} {user.Soyadi},<br/><br/>" +
                                   $"Aşağıdaki bilgilerinizle sisteme kaydedildiniz:<br/>" +
                                   $"Ad: {user.Adi} {user.IkinciAdi} {user.Soyadi} {user.IkinciSoyadi}<br/>" +
                                   $"Telefon Numarası: {user.PhoneNumber}<br/>" +
                                   $"Doğum Tarihi: {user.DogumTarihi.ToShortDateString()}<br/>" +
                                   $"Doğum Yeri: {user.DogumYeri}<br/>" +
                                   $"TC: {user.TC}<br/>" +
                                   $"Departman: {user.Departman}<br/>" +
                                   $"Meslek: {user.Meslek}<br/><br/>" +
                                   $"Daha sonra değiştirebileceğiniz bilgileriniz doğru ise, lütfen aşağıdaki linke tıklayarak onaylayınız:<br/>" +
                                   $"<a href='{Url.Action("ConfirmCalisanDetails", "Auth", new { userId = user.Id, token = token }, Request.Scheme)}'>Bilgilerimi Onayla</a>";

                await _emailSender.SendEmailAsync(user.Email, "FHYI Group - Bilgilerinizi Onaylayın", emailBody);


                await _emailSender.SendEmailAsync(user.Email, "FHYI Group E-posta Doğrulama", $"Lütfen e-postanızı doğrulamak için <a href='{confirmationLink}'>buraya tıklayın</a>.");
                // Rol oluştur ve kullanıcıyı bu role ekle
                if (!await _roleManager.RoleExistsAsync("CALISAN"))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Çalışan", NormalizedName = "CALISAN" });

                }
                await _userManager.AddToRoleAsync(user, role: "CALISAN");
                user.Token = token;
                return Ok(new { Message = "Kullanıcı Başarıyla Oluşturuldu." });
            }
            // Hata mesajlarını döndür
            return BadRequest(result.Errors);


        }

        //Yöneticinin kendine çalışan eklemesi
        [HttpPost("assignManager")]
        public async Task<IActionResult> AssignManager(string calisanId)
        {
            var calisan = await _userManager.FindByIdAsync(calisanId);
            var claims = User.Claims.ToList();
            var yoneticiId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
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
                _emailSender.SendEmailAsync(calisan.Email,"FHYI Group - Yöneticiye Atandınız",$"{yonetici.Sirket.SirketAdi} yöneticisi Sayın {yonetici.Adi} {yonetici.Soyadi} tarafından Çalışan olarak atandınız. İyi günler!");
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
                _emailSender.SendEmailAsync(izin.ApplicationUser.Email, "FHYI Group - İzin Durumu Değişikliği", $"Sevgili çalışanımız {izin.ApplicationUser.Adi} {izin.ApplicationUser.Soyadi} {izin.IzinGunSayisi} günlük izniniz onaylanmıştır. İyi günler!");
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
                _emailSender.SendEmailAsync(izin.ApplicationUser.Email, "FHYI Group - İzin Durumu Değişikliği", $"Sevgili çalışanımız {izin.ApplicationUser.Adi} {izin.ApplicationUser.Soyadi} {izin.IzinGunSayisi} günlük izniniz reddedilmiştir. İyi günler!");
                return Ok("İzin isteği başarıyla reddedildi");
            }
            return BadRequest("Böyle bir izin bulunamadı");
        }

    }
}
