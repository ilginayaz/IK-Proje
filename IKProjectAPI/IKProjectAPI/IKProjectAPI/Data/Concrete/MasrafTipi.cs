using IKProjectAPI.Data.Abstract;
using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Concrete
{
    public class MasrafTipi : IBaseEntity
    {
        public MasrafTipi() 
        {
            Masraflar = new HashSet<Masraf>();
        }
        public int Id { get; set; }
        public string MasrafTipiAdi {  get; set; }

        public Guid SirketId { get; set; }
        public Sirket? Sirket { get; set; }
        public virtual ICollection<Masraf> Masraflar { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;
    }
}
