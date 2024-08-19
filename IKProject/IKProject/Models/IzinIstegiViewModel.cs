using IKProject.Data.Enums;
using System;

namespace IKProject.Models
{
    public class IzinIstegiViewModel
    {
        public int Id { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public decimal? IzinGunSayisi { get; set; }
        public string IstekYorumu { get; set; }



        public IzinTuru IzinTuru { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
