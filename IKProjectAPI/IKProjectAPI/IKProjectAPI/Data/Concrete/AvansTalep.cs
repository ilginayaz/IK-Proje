using IKProjectAPI.Data.Abstract;
using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Concrete
{
    public class AvansTalep : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime TalepTarihi { get; set; }
        public decimal Tutar {  get; set; }
        public ParaBirimi ParaBirimi { get; set; }
        public string Aciklama { get; set; }
        public OnayDurumu OnayDurumu { get; set; } = Enums.OnayDurumu.Beklemede;
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;
    }
}
