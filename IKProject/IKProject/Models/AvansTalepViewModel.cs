using IKProject.Data.Enums;
using IKProject.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace IKProject.Models
{
    public class AvansTalepViewModel
    {
        public int Id { get; set; }
        public DateTime TalepTarihi { get; set; }
        [Required]
        [Display(Name = "Avans Tutarı")]
        [Range(0, int.MaxValue, ErrorMessage = "Avans tutarı pozitif olmalı.")]
        [MultipleOfTen(ErrorMessage = "Avans tutarı 10'un katı olmalıdır.")]
        public decimal Tutar { get; set; }
        public ParaBirimi ParaBirimi { get; set; }
        public string Aciklama { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
