using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using IKProjectAPI.Data.Models;
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
    
    public class CompanyController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public CompanyController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost("SirketOlustur")]
        public async Task<IActionResult> CreateCompany(SirketRegisterModel model)
        {
            //// Yönetici kontrolü
            //var yonetici = await _userManager.FindByIdAsync(model.YoneticiId);
            //if (yonetici == null)
            //{
            //    return BadRequest("Yönetici bulunamadı");
            //}

            // Model doğrulama
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Şirket numarası, vergi numarası veya şirket adı kontrolü
            var existingCompany = await _context.sirketler
                .FirstOrDefaultAsync(c => c.SirketNumarasi == model.SirketNumarasi
                                       || c.VergiNo == model.VergiNo
                                       || c.SirketAdi == model.SirketAdi
                                       || c.SirketEmail == model.SirketEmail);

            // Eğer aynı şirket numarası, vergi numarası veya şirket adından varsa hata döndür
            if (existingCompany != null)
            {
                return BadRequest("Bu bilgilerle kayıtlı bir şirket zaten mevcut.");
            }

            // Şirket oluşturma
            var company = new Sirket
            {
                Status = model.Status,
                SirketAdi = model.SirketAdi,
                SirketNumarasi = model.SirketNumarasi,
                VergiNo = model.VergiNo,
                VergiOfisi = model.VergiOfisi,
                CalisanSayisi = 1,
                SirketEmail = model.SirketEmail,
                Sehir = model.Sehir,
                Address = model.Address,
                PostaKodu = model.PostaKodu,
                Telefon=model.Telefon,
                SirketUnvani= model.SirketUnvani,
                LogoUrl=model.LogoUrl,
                CreatedTime = DateTime.Now
            };

            // Şirketi veritabanına kaydet
            _context.sirketler.Add(company);
            await _context.SaveChangesAsync();

            //// Şirketin yöneticisini ekleme
            //company.SirketYoneticileri.Add(yonetici);            
            //await _context.SaveChangesAsync();

            // Başarılı mesaj döndür
            return Ok(new { Message = "Şirket başarıyla oluşturuldu.", CompanyId = company.Id });
        }


        [HttpGet("SirketYoneticileri/{companyId}")]
        public async Task<IActionResult> GetCompanyManagers(Guid companyId)
        {
            // İlgili şirketi veritabanından buluyoruz.
            var company = await _context.sirketler
                                        .Include(s => s.SirketYoneticileri) // Yoneticileri dahil ediyoruz
                                        .FirstOrDefaultAsync(s => s.Id == companyId);

            // Şirket bulunamazsa hata döneriz.
            if (company == null)
            {
                return NotFound("Şirket bulunamadı.");
            }

            // Şirketin yöneticilerini kontrol ediyoruz.
            var managers = company.SirketYoneticileri;

            // Eğer yöneticiler yoksa ya da liste boşsa bilgilendiriyoruz.
            if (managers == null || !managers.Any())
            {
                return NotFound("Bu şirkete ait yönetici bulunamadı.");
            }

            // Yöneticileri döneriz.
            return Ok(managers);
        }


        //Sirket bilgilerini update 
        [HttpPost("SirketUpdate")]
        public async Task<IActionResult> SirketUpdate([FromBody] Sirket sirket)
        {
            // Veritabanında ilgili şirketi bul
            var existingSirket = await _context.sirketler.FindAsync(sirket.Id);
            if (existingSirket == null)
            {
                return NotFound("Şirket bulunamadı.");
            }
            var claims = User.Claims.ToList();
            var yoneticiId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var yonetici = await _userManager.FindByIdAsync(yoneticiId);
            if (yonetici == null)
            {
                return NotFound("Yönetici bulunamadı.");
            }
            if (sirket.Id == yonetici.Sirket.Id)
            {
                // Şirket bilgilerini güncelle
                existingSirket.SirketAdi = sirket.SirketAdi;
                existingSirket.SirketNumarasi = sirket.SirketNumarasi;
                existingSirket.VergiNo = sirket.VergiNo;
                existingSirket.VergiOfisi = sirket.VergiOfisi;
                existingSirket.CalisanSayisi = sirket.CalisanSayisi;
                existingSirket.SirketEmail = sirket.SirketEmail;
                existingSirket.Sehir = sirket.Sehir;
                existingSirket.Address = sirket.Address;
                existingSirket.PostaKodu = sirket.PostaKodu;

                // Değişiklikleri veritabanına kaydet
                _context.sirketler.Update(existingSirket);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Şirket bilgileri başarıyla güncellendi." });
            }
            return BadRequest("Yönetici ve şirket eşleşmiyor HATA!");
        }

        [HttpGet("sirketListele")]
        public async Task<IActionResult> SirketListele()
        {
            var sirketler = _context.sirketler;
            if (sirketler == null)
            {
                return BadRequest("Herhangi bir şirket bulunamadı.");
            }
            return Ok(sirketler);
        }

        [HttpGet("sirketDetay/{id}")]
        public async Task<IActionResult> GetCompanyDetails(Guid id)
        {
            var sirket = await _context.sirketler
                .Include(s => s.SirketYoneticileri)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sirket == null)
            {
                return NotFound("Şirket bulunamadı.");
            }

            return Ok(sirket);
        }


        [HttpPost("YoneticiAta")]
        public async Task<IActionResult> AssignManager(Guid companyId, string managerId)
        {
            var company = await _context.sirketler.FindAsync(companyId);
            if (company == null)
            {
                return NotFound("Şirket bulunamadı.");
            }

            var manager = await _userManager.FindByIdAsync(managerId);
            if (manager == null)
            {
                return NotFound("Yönetici bulunamadı.");
            }

            company.SirketYoneticileri.Add(manager);
            manager.Sirket = company;

            await _context.SaveChangesAsync();
            return Ok("Yönetici şirkete başarıyla atandı.");
        }
    }
}
