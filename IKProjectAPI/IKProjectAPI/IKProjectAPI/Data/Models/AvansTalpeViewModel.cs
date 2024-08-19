using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Models
{
    public class AvansTalpeViewModel
    {
        public int Id { get; set; }
        public DateTime TalepTarihi { get; set; }
        public decimal Tutar { get; set; }
        public ParaBirimi ParaBirimi { get; set; }
        public string Aciklama { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
