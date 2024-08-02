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

        public AuthController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                Sirket = registerModel.Sirket,
                Departman = registerModel.Departman,
                Meslek = registerModel.Meslek,
                Adres = registerModel.Adres,
                PasswordHash = registerModel.Password,
                Cinsiyet = registerModel.Cinsiyet,
                UserName = registerModel.Email,
                Token = string.Empty

            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                //Kayıt başarılı, token dönebiliriz veya sadece başarılı yanıtı dönebiliriz
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Rol oluştur ve kullanıcıyı bu role ekle
                if (!await _roleManager.RoleExistsAsync("CALISAN"))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Calisan", NormalizedName = "CALISAN" });

                }
                    await _userManager.AddToRoleAsync(user, role:"Calisan");
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

    }
}
