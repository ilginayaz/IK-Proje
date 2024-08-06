using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Concrete
{
    public class IzinTipi
    {
        public IzinTipi() 
        {
            IzinIstegis = new HashSet<IzinIstegi>();
        }

        public int Id { get; set; }
        public string IzinTipiAdi { get; set; }
        public decimal? DefaultDays { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;

        public Guid? SirketId { get; set; }
        public Sirket Sirket { get; set; }


        public virtual ICollection<IzinIstegi> IzinIstegis { get; set; }

    }
}
