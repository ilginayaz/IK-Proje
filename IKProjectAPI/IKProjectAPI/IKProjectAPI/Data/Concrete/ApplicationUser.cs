
using IKProjectAPI.Data.Abstract;
using IKProjectAPI.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace IKProjectAPI.Data.Concrete
{
    public class ApplicationUser : IdentityUser, IBaseEntity
    {
        public ApplicationUser()
        {
            Izinler = new HashSet<IzinIstegi>();
            Calisanlar = new HashSet<ApplicationUser>();
            Masraflar = new HashSet<HarcamaTalep>();
        }

        [Display(Name = "Profil Fotoğrafınız")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string ProfilePhoto { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [MaxLength(25), MinLength(2)]
        [Display(Name = "Adınız")]
        public string Adi { get; set; }
        [MaxLength(25), MinLength(2)]
        [Display(Name = "İkinci Adınız")]
        public string? IkinciAdi { get; set; }
        [MaxLength(25), MinLength(2)]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Soyadınız")]
        public string Soyadi { get; set; }
        [MaxLength(25), MinLength(2)]
        [Display(Name = "İkinci Soyadınız")]
        public string? IkinciSoyadi { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Tarihiniz")]
        public DateOnly DogumTarihi { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Yeriniz")]
        public Sehirler DogumYeri { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [MaxLength(11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [MinLength(11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC Kimlik Numarası sadece 11 haneli bir sayı olmalıdır.")]
        public string TC { get; set; }
        [DataType(DataType.Date)]
        public DateOnly IseGirisTarihi { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [DataType(DataType.Date)]
        public DateOnly IstenCikisTarihi { get; set; }
        public Guid SirketId { get; set; }
        public Sirket Sirket { get; set; }
        public Meslek Meslek { get; set; }
        public Departman Departman { get; set; }
        public string Adres { get; set; }
        public decimal Maas { get; set; } = 20000;
        public Cinsiyet Cinsiyet { get; set; }
        public string? Token { get; set; }


        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;

        public string? YoneticiId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser? Yonetici { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Calisanlar { get; set; }
        public virtual ICollection<IzinIstegi> Izinler { get; set; }
        public virtual ICollection<HarcamaTalep> Masraflar { get; set; }

    }
}
