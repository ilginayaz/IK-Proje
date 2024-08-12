using IKProject.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace IKProject.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Adresiniz")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Şifreniz")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Profil Fotoğrafınız")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string ProfilePhoto { get; set; }
        //[PictureFileExtention]
        //public IFormFile? UploadPath { get; set; }
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
        [MaxLength(11), MinLength(11)]
        [Display(Name = "Telefon Numaraniz")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonNumarasi { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Tarihiniz")]
        [DataType(DataType.Date)]
        public DateOnly DogumTarihi { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Yeriniz")]
        public Sehirler DogumYeri { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [MaxLength(11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [MinLength(11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC Kimlik Numarası sadece 11 haneli bir sayı olmalıdır.")]
        public string TC { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public Departman Departman { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public Meslek Meslek { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [MaxLength(100), MinLength(5)]
        [Display(Name = "Adresiniz")]
        public string Adres { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public IdentityResult? Result { get; set; }
        public string? Token { get; set; }

        public string SirketAdi { get; set; }
        public string SirketNumarasi { get; set; }
        public string VergiNo { get; set; }
        public string VergiOfisi { get; set; }
        public string SirketEmail { get; set; }
        public Sehirler Sehir { get; set; }
        public string Address { get; set; }
        public string PostaKodu { get; set; }
    }
}
