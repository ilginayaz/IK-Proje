using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Models
{
    public class HarcamaTalepViewModel
    {
        public int Id { get; set; }  // Güncelleme veya silme işlemlerinde kullanılabilir.
        public string Aciklama { get; set; }
        public decimal GiderTutari { get; set; }
        public ParaBirimi ParaBirimi { get; set; }  // Enum türünde olduğunu varsayıyoruz.
        public DateTime MasrafTarihi { get; set; }  // Masrafın yapıldığı tarih.
        public string ApplicationUserId { get; set; }  // Kullanıcı kimliği, hangi kullanıcıya ait olduğu.
    }
}
