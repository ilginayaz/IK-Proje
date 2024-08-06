using IKProjectAPI.Data.Abstract;
using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Concrete
{
    public class IzinOdenek : IBaseEntity
    {
        public int Id { get; set; }
        public int GunSayisi { get; set; }
        public int Periyot {  get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int IzinTipiId { get; set; }
        public IzinTipi IzinTipi { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; } = Status.AwatingApproval;
    }
}
