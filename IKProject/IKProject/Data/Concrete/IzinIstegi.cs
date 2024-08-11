using IKProject.Data.Enums;

namespace IKProject.Data.Concrete
{
    public class IzinIstegi
    {
        public int Id { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public decimal? IzinGunSayisi { get; set; }


        public string IstekYorumu { get; set; }

        public OnayDurumu? OnayDurumu { get; set; }


        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;

        public string AppUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public IzinTipi IzinTipi { get; set; }
        public int IzinTipiId { get; set; }
    }
}
