using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data.Models;
using IKProjectAPI.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IKProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly EmailSender _emailSender;

        public AuthController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, EmailSender emailSender)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _emailSender = emailSender;
        }

        //Kullanıcı Kayıt endpoit
       
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
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

            var sirket = new Sirket
            {
                SirketAdi = registerModel.SirketAdi,
                SirketEmail = registerModel.SirketEmail,
                SirketNumarasi = registerModel.SirketNumarasi,
                VergiNo = registerModel.VergiNo,
                VergiOfisi = registerModel.VergiOfisi,
                Address = registerModel.Address,
                PostaKodu = registerModel.PostaKodu,
                Sehir = registerModel.Sehir,
                Status = Data.Enums.Status.AwatingApproval,
                SirketYoneticileri = new List<ApplicationUser>()
            };

            _context.sirketler.Add(sirket);
            user.SirketId = sirket.Id;
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                _context.SaveChanges();
                //Kayıt başarılı, token dönebiliriz veya sadece başarılı yanıtı dönebiliriz
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                sirket.SirketYoneticileri.Add(user);
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
                return Ok(new { Message = "Kullanıcı Başarıyla Oluşturuldu." });
            }
            // Hata mesajlarını döndür
            return BadRequest(result.Errors);
        }
        //Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {

            var result = await _signInManager.PasswordSignInAsync(
                loginModel.Email,
                loginModel.Password,
                isPersistent: false,
                lockoutOnFailure: false
                );
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                // Kullanıcının rollerini al
                var roles = await _userManager.GetRolesAsync(user);


                // Claim'leri oluştur
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.Adi),
                        new Claim(ClaimTypes.Surname, user.Soyadi),
                        new Claim(ClaimTypes.Uri, user.ProfilePhoto)
                    };

                // Rolleri claim'lere ekle
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }
        [HttpPost("logout")]
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    user.Status = Data.Enums.Status.Active;
                    user.EmailConfirmed = true;
                    _emailSender.SendEmailAsync(email, "FHYI Group HOŞGELDİNİZ", "<h1>Hoş Geldiniz!</h1><p>FHYI Group'a katıldığınız için teşekkür ederiz.</p>");
                    if (await _userManager.IsInRoleAsync(user, "Calisan"))
                    {
                        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var confirmationLink = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token = resetToken }, Request.Scheme);

                        await _emailSender.SendEmailAsync(user.Email, "FHYI Group Şifre Sıfırlama", $"Lütfen şifrenizi sıfırlamak için <a href='{confirmationLink}'>buraya tıklayın</a>.");
                        return Ok("Email başarıyla onaylandı ve Çalışana Şifre sıfırlama isteği gönderildi");
                    }
                    return Ok("Email başarıyla onaylandı");
                }
            }
            return BadRequest("Kullanıcı Bulunamadı.");
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }
            if (await _userManager.IsInRoleAsync(user, "Calisan")) 
            {
                var a = "log";
            }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account", new { token=token, email = user.Email }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "FHYI Group - Şifre Sıfırlama Talebi",
                $"<p>Lütfen şifrenizi sıfırlamak için aşağıdaki bağlantıya tıklayın:</p><a href='{resetLink}'>Şifre Sıfırla</a>");

            return Ok("Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {

                _emailSender.SendEmailAsync(model.Email, "FHYI Group - Şifre Değiştirme Başarılı", "<h1>Şifreniz Başarıyla Değiştirildi!</h1><p>Hesabınızın şifresi değiştirildi.</p>");
                return Ok("Şifreniz başarıyla sıfırlandı.");
            }

            return BadRequest("Şifre sıfırlama başarısız oldu.");
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await _userManager.GetUserAsync(User); // Giriş yapan kullanıcıyı al

            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                _emailSender.SendEmailAsync(user.Email, "FHYI Group - Şifre Değiştirme Başarılı", "<h1>Şifreniz Başarıyla Değiştirildi!</h1><p>Hesabınızın şifresi değiştirildi.</p>");
                return Ok("Şifreniz başarıyla değiştirildi.");
            }

            return BadRequest("Şifre değişikliği başarısız oldu.");
        }

        [HttpGet("getroles")]
        public async Task<IActionResult> GetRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpGet("ConfirmCalisanDetails")]
        public async Task<IActionResult> ConfirmCalisanDetails(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                user.Status = Data.Enums.Status.Active;
                _context.SaveChanges(); // Kullanıcının durumu kaydedildi.

                await _emailSender.SendEmailAsync(user.Email, "FHYI Group - Onay Başarılı",
                    $"Sayın {user.Adi},<br/><br/>Bilgileriniz başarıyla onaylandı ve hesabınız aktif hale getirildi.");

                return Ok("Bilgileriniz onaylandı ve hesabınız aktif hale getirildi.");
            }

            return BadRequest("Bilgileriniz onaylanamadı. Lütfen tekrar deneyin.");
        }

    }
}
