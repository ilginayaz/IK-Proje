using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data.Models;
using Microsoft.AspNetCore.Http;
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

        public AuthController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        //Kullanıcı Kayıt endpoit

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
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
                PasswordHash = registerModel.Password,
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
                Status = Data.Enums.Status.AwatingApproval
            };
            
                _context.sirketler.Add(sirket);
            user.SirketId = sirket.Id;
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                //Kayıt başarılı, token dönebiliriz veya sadece başarılı yanıtı dönebiliriz
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                sirket.SirketYoneticileri.Add(user);
                _context.SaveChanges();
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
                    isPersistent:false,
                    lockoutOnFailure: false
                    );
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(loginModel.Email);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
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

        [HttpPost("ConfirmEmail")]
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
                    return Ok("Email başarıyla onaylandı");
                }
            }
            return BadRequest("Kullanıcı Bulunamadı.");
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
        //şifremi unuttum 
        // şifre değiştir eklenecek
    }
}
