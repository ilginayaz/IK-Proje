using IKProject.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IKProject.Models
{
    public class YoneticiRegisterModel
    {
        [Display(Name = " Email Adresi")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = " Profil Fotoğrafı")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string ProfilePhoto { get; set; }
        //[PictureFileExtention]
        //public IFormFile? UploadPath { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [MaxLength(25), MinLength(2)]
        [Display(Name = " Adı")]
        public string Adi { get; set; }
        [MaxLength(25), MinLength(2)]
        [Display(Name = " İkinci Adı")]
        public string? IkinciAdi { get; set; }
        [MaxLength(25), MinLength(2)]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Soyadı")]
        public string Soyadi { get; set; }
        [MaxLength(25), MinLength(2)]
        [Display(Name = "İkinci Soyadı")]
        public string? IkinciSoyadi { get; set; }
        [MaxLength(11), MinLength(11)]
        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonNumarasi { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateOnly DogumTarihi { get; set; }
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [Display(Name = "Doğum Yeri")]
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
        [Display(Name = "Adresi")]
        public string Adres { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public IdentityResult? Result { get; set; }
        public string? Token { get; set; }
    }
}
