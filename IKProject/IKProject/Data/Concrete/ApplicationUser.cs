using IKProject.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKProject.Data.Concrete
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            Izinler = new HashSet<IzinTipi>();
            Calisanlar = new HashSet<ApplicationUser>();
            //Yoneticiler = new HashSet<Masraf>();
        }

        [Display(Name = "Profil Fotoğrafınız")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string ProfilePhoto { get; set; }

        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [MaxLength(25, ErrorMessage = "Adınız en fazla 25 karakter olabilir.")]
        [MinLength(2, ErrorMessage = "Adınız en az 2 karakter olmalıdır.")]
        [Display(Name = "Adınız")]
        public string Adi { get; set; }

        [MaxLength(25, ErrorMessage = "İkinci adınız en fazla 25 karakter olabilir.")]
        [MinLength(2, ErrorMessage = "İkinci adınız en az 2 karakter olmalıdır.")]
        [Display(Name = "İkinci Adınız")]
        public string? IkinciAdi { get; set; }

        [MaxLength(25, ErrorMessage = "Soyadınız en fazla 25 karakter olabilir.")]
        [MinLength(2, ErrorMessage = "Soyadınız en az 2 karakter olmalıdır.")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Soyadınız")]
        public string Soyadi { get; set; }

        [MaxLength(25, ErrorMessage = "İkinci soyadınız en fazla 25 karakter olabilir.")]
        [Display(Name = "İkinci Soyadınız")]
        public string? IkinciSoyadi { get; set; }

        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Tarihiniz")]
        public DateTime DogumTarihi { get; set; }  // DateOnly yerine DateTime

        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Yeriniz")]
        public Sehirler DogumYeri { get; set; }

        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [MaxLength(11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [MinLength(11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC Kimlik Numarası sadece 11 haneli bir sayı olmalıdır.")]
        public string TC { get; set; }

        [DataType(DataType.Date)]
        public DateTime IseGirisTarihi { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? IstenCikisTarihi { get; set; }  // Nullable DateTime

        public Guid SirketId { get; set; }
        public Sirket Sirket { get; set; }

        public Meslek Meslek { get; set; }
        public Departman Departman { get; set; }

        public string Adres { get; set; }
        public decimal Maas { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public string? Token { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;

        public string? YoneticiId { get; set; }
        public virtual ApplicationUser? Yonetici { get; set; }

        public virtual ICollection<ApplicationUser> Calisanlar { get; set; }
        public virtual ICollection<IzinTipi> Izinler { get; set; }
        //public virtual ICollection<Masraf> Yoneticiler { get; set; }
    }
}
