using IKProject.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKProject.Models
{
    public class SirketRegisterModel
    {
        [Display(Name = "Şirket Adı")]
        [Required(ErrorMessage = "Şirket adı girilmesi zorunludur.")]
        public string SirketAdi { get; set; }

        [Display(Name = "Şirket Numarası")]
        [Required(ErrorMessage = "Şirket numarası girilmesi zorunludur.")]
        public string SirketNumarasi { get; set; }

        [Display(Name = "Vergi Numarası")]
        [Required(ErrorMessage = "Vergi numarası girilmesi zorunludur.")]
        public string VergiNo { get; set; }

        [Display(Name = "Vergi Ofisi")]
        [Required(ErrorMessage = "Vergi ofisi girilmesi zorunludur.")]
        public string VergiOfisi { get; set; }

        [Display(Name = "Şirket Email")]
        [Required(ErrorMessage = "Şirket e-posta adresi girilmesi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string SirketEmail { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Şehir girilmesi zorunludur.")]
        public Sehirler Sehir { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Adres girilmesi zorunludur.")]
        public string Address { get; set; }

        [Display(Name = "Posta Kodu")]
        [Required(ErrorMessage = "Posta kodu girilmesi zorunludur.")]
        public string PostaKodu { get; set; }

        public string? LogoUrl { get; set; } // Şirket logosu URL'si
        public string? SirketUnvani { get; set; } // Şirket unvanı
        public string? Telefon { get; set; } // Şirketin telefon numarası

        public Status Status { get; set; } = Status.AwatingApproval;
    }

}
