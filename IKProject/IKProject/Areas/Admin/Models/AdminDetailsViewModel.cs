namespace IKProject.Areas.Admin.Models
{
    public class AdminDetailsViewModel
    {
        public string ProfilePhoto { get; set; }
        public string Adi { get; set; }
        public string? IkinciAdi { get; set; }
        public string Soyadi { get; set; }
        public string? IkinciSoyadi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string TC { get; set; }
        public string Adres { get; set; }
        public string Cinsiyet { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

}
