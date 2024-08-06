using IKProjectAPI.Data;
using IKProjectAPI.Data.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> CreateCompany([FromBody] Sirket company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            company.Id = Guid.NewGuid();
            _context.sirketler.Add(company);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Şirket başarıyla oluşturuldu.", CompanyId = company.Id });
        }

        //Sirket bilgilerini update yapılacak

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
