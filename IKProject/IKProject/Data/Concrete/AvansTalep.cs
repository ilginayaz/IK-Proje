using IKProject.Data.Enums;

namespace IKProject.Data.Concrete
{
    public class AvansTalep 
    {
        public int Id { get; set; }
        public DateTime TalepTarihi { get; set; }
        public decimal Tutar { get; set; }
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
