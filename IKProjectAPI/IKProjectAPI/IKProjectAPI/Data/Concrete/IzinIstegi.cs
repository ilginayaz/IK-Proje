using IKProjectAPI.Data.Abstract;
using IKProjectAPI.Data.Enums;
using System.Text.Json.Serialization;



namespace IKProjectAPI.Data.Concrete
{
    public class IzinIstegi : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public decimal? IzinGunSayisi { get; set; }


        public string IstekYorumu { get; set; }

        public OnayDurumu? OnayDurumu { get; set; } = Enums.OnayDurumu.Beklemede;

        public IzinTuru IzinTuru { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;

        public string ApplicationUserId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }


    }
}
