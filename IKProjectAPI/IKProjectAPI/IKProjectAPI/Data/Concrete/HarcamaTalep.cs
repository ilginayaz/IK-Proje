using IKProjectAPI.Data.Abstract;
using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Concrete
{
    public class HarcamaTalep : IBaseEntity
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public decimal GiderTutari { get; set; }
        public ParaBirimi ParaBirimi { get; set; }
        public OnayDurumu OnayDurumu { get; set; } = Enums.OnayDurumu.Beklemede;
        public DateTime MasrafTarihi { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;
    }
}
