using IKProject.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKProject.Models
{
    public class SirketRegisterModel
    {
        public string YoneticiId { get; set; }
        [Display(Name = "Şirket Adı")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string SirketAdi { get; set; }

        [Display(Name = "Şirket Numarası")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string SirketNumarasi { get; set; }

        [Display(Name = "Vergi Numarası")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string VergiNo { get; set; }

        [Display(Name = "Vergi Ofisi")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string VergiOfisi { get; set; }

        public string? LogoUrl { get; set; } // Şirket logosu URL'si
        public string? SirketUnvani { get; set; } // Şirket unvanı
        public string? Telefon { get; set; } // Şirketin telefon numarası

        [Display(Name = "Şirket Email")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string SirketEmail { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public Sehirler Sehir { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Girilmesi Zorunlu Alan.")]
        public string Address { get; set; }

        [Display(Name = "PostaKodu")]
        public string PostaKodu { get; set; }


        public Status Status { get; set; } = Status.AwatingApproval;
    }
}
