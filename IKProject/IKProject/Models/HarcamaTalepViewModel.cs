using IKProject.Data.Concrete;
using IKProject.Data.Enums;
using IKProject.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace IKProject.Models
{
    public class HarcamaTalepViewModel
    {
        public int Id { get; set; }  // Güncelleme veya silme işlemlerinde kullanılabilir.
        [Required]
        public string Aciklama { get; set; }
        [Required]
        [Display(Name = "Gider Tutarı")]
        [Range(0, int.MaxValue, ErrorMessage = "Gider tutarı pozitif olmalı.")]
        [MultipleOfTen(ErrorMessage = "Gider tutarı 10'un katı olmalıdır.")]
        public decimal GiderTutari { get; set; }
        public ParaBirimi ParaBirimi { get; set; }  // Enum türünde olduğunu varsayıyoruz.
        [Required]
        public DateTime MasrafTarihi { get; set; }  // Masrafın yapıldığı tarih.
        public string ApplicationUserId { get; set; }  // Kullanıcı kimliği, hangi kullanıcıya ait olduğu.
    }
}
