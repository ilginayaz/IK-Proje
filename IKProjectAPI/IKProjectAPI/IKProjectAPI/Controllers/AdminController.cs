using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IKProjectAPI.NewFolder;
using IKProjectAPI.Data.Models;

namespace IKProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
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
        //Kullanıcı Kayıt endpoit

        [HttpPost("register")]
        public async Task<IActionResult> Register(YoneticiRegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
                Cinsiyet = registerModel.Cinsiyet,
                UserName = registerModel.Email,
                Token = string.Empty,
                Status = Data.Enums.Status.AwatingApproval,
            };

            registerModel.Password = "Yönetici1.";

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                _context.SaveChanges();
                //Kayıt başarılı, token dönebiliriz veya sadece başarılı yanıtı dönebiliriz
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                _context.SaveChanges();
                var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { email = user.Email, token = token }, Request.Scheme);


                await _emailSender.SendEmailAsync(user.Email, "FHYI Group E-posta Doğrulama", $"Lütfen e-postanızı doğrulamak için <a href='{confirmationLink}'>buraya tıklayın</a>.");
                // Rol oluştur ve kullanıcıyı bu role ekle
                if (!await _roleManager.RoleExistsAsync("YONETICI"))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Yonetici", NormalizedName = "YONETICI" });

                }
                await _userManager.AddToRoleAsync(user, role: "Yonetici");
                user.Token = token;
                await _emailSender.SendEmailAsync(user.Email, "FHYI Group Anlık Şifreniz","Şifreniz ' Yönetici1. '.");
                return Ok(new { Message = "Kullanıcı Başarıyla Oluşturuldu." });
            }
            // Hata mesajlarını döndür
            return BadRequest(result.Errors);
        }
        [HttpGet("YoneticileriListele")]
        public async Task<IActionResult> YoneticileriListele()
        {
            var yoneticiler = await _userManager.GetUsersInRoleAsync("Yonetici");
            if (yoneticiler != null && yoneticiler.Any())
            {
            return Ok(yoneticiler);
            }
            return BadRequest("Herhangi bir yönetici bulunamadı");
        }
        [HttpGet("OnayBekleyenYoneticiler")]
        public async Task<IActionResult> OnayBekleyenYoneticiler()
        {
            var yoneticiler = await _userManager.GetUsersInRoleAsync("Yonetici");
           var onayBekleyenler = yoneticiler.Where(x => x.Status == Data.Enums.Status.AwatingApproval);
            if (onayBekleyenler != null && onayBekleyenler.Any())
            {
                return Ok(onayBekleyenler);
            }
            return BadRequest("Herhangi bir yönetici bulunamadı");
        }

        [HttpGet("BosYoneticileriListele")]
        public async Task<IActionResult> BosYoneticileriListele()
        {
            // "Yonetici" rolündeki kullanıcıları alıyoruz
            var yoneticiler = await _userManager.GetUsersInRoleAsync("Yonetici");

            // sirketId'si null olan yöneticileri filtreliyoruz
            var bosYoneticiler = yoneticiler.Where(y => y.SirketId == null).ToList();

            if (bosYoneticiler.Any())
            {
                return Ok(bosYoneticiler);
            }

            return BadRequest("Herhangi bir yönetici bulunamadı");
        }


        [HttpPatch("YoneticiyiOnayla")]
        public async Task<IActionResult> YoneticiyiOnayla(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var sirket = _context.sirketler.Where(s => s.Id == user.SirketId && s.Status == Data.Enums.Status.AwatingApproval).FirstOrDefault();
            
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            if (sirket != null)
            {

            user.Status = Data.Enums.Status.Active;
            sirket.Status = Data.Enums.Status.Active;
                await _context.SaveChangesAsync();
               await _emailSender.SendEmailAsync(user.Email, "FHYI GROUP - Hesabınız Onaylandı", "Hesabınız onaylanmıştır");
            return Ok("Kullanıcı ve Şirket başarıyla onaylandı");
            }
            return BadRequest("Kullanıcıya ait Sirket Bulunamadı");
        }

        [HttpPatch("YoneticiyiReddet")]
        public async Task<IActionResult> YoneticiyiReddet(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var sirket = _context.sirketler.Where(s => s.Id == user.SirketId && s.Status == Data.Enums.Status.AwatingApproval).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            user.Status = Data.Enums.Status.Passive;
            sirket.Status = Data.Enums.Status.Passive;
            await _context.SaveChangesAsync();
           await _emailSender.SendEmailAsync(user.Email, "FHYI GROUP - Hesabınız Reddedildi", "Hesabınız reddedildi üzgünüz :(");
            return Ok("Kullanıcı başarıyla reddedildi");
        }

        [HttpPatch("YoneticiyiSil")]
        public async Task<IActionResult> YoneticiyiSil(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var sirket = _context.sirketler.Where(s => s.SirketYoneticileri.Any(y => y.Id == user.Id) && s.Status == Data.Enums.Status.Active).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            user.Status = Data.Enums.Status.Passive;
            await _context.SaveChangesAsync();
            _emailSender.SendEmailAsync(user.Email, "FHYI GROUP - Hesabınız ve Şirketiniz Hakkında Bilgilendirme", "Hesabınız ve Şirketiniz silindi üzgünüz :(");
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

        [HttpPost("assignCompanyManager")]
        public async Task<IActionResult> AssignCompanyManager([FromBody] AssignCompanyManagerViewModel request)
        {
            // Şirketi veritabanından bul
            var sirket = await _context.sirketler.FindAsync(request.SirketId);
            if (sirket == null)
            {
                return NotFound("Şirket bulunamadı.");
            }

            // Yönetici kullanıcıyı veritabanından bul
            var yonetici = await _userManager.FindByIdAsync(request.YoneticiId);
            if (yonetici == null)
            {
                return NotFound("Yönetici bulunamadı.");
            }

            // Yönetici şirkete atanır
            yonetici.SirketId = sirket.Id;
            sirket.SirketYoneticileri.Add(yonetici);

            // Veritabanı güncellemeleri
            var result = await _userManager.UpdateAsync(yonetici);
            if (result.Succeeded)
            {
                _context.sirketler.Update(sirket);
                await _context.SaveChangesAsync();

                // Yöneticiye e-posta bildirimi gönder
                await _emailSender.SendEmailAsync(yonetici.Email, "FHYI Group - Şirkete Yönetici Olarak Atandınız",
                    $"{sirket.SirketAdi} adlı şirkete yönetici olarak atandınız. İyi günler!");

                return Ok("Yönetici başarılı bir şekilde şirkete atandı.");
            }

            return BadRequest(result.Errors);
        }

    }
}
