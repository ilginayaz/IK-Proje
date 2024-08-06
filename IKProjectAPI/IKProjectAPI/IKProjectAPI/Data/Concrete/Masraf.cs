using IKProjectAPI.Data.Abstract;
using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Concrete
{
    public class Masraf : IBaseEntity
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public decimal GiderTutari { get; set; }
        public OnayDurumu OnayDurumu { get; set; }
        public DateTime MasrafTarihi { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int MasrafTipiId { get; set; }
        public MasrafTipi MasrafTipi { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;
    }
}
