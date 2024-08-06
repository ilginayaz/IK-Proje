using IKProjectAPI.Data.Enums;

namespace IKProjectAPI.Data.Abstract
{
    public interface IBaseEntity
    {
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Status Status { get; set; }
    }
}
