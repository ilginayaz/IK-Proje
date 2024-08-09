using IKProject.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKProject.Data.Concrete
{
    public class IzinTipi
    {
            public int Id { get; set; }

            [Required(ErrorMessage = "Başlangıç tarihi gereklidir.")]
            public DateTime BaslangicTarihi { get; set; }

            [Required(ErrorMessage = "Bitiş tarihi gereklidir.")]
            public DateTime BitisTarihi { get; set; }

            public decimal? IzinGunSayisi { get; set; }

            [Required(ErrorMessage = "İstek yorumu gereklidir.")]
            public string IstekYorumu { get; set; }

            //public OnayDurumu? OnayDurumu { get; set; }

            public DateTime CreatedTime { get; set; } = DateTime.Now;
            public DateTime? UpdatedTime { get; set; }
            public DateTime? DeletedTime { get; set; }
            public Status Status { get; set; } = Status.AwatingApproval;

            [Required(ErrorMessage = "Kullanıcı ID'si gereklidir.")]
            public string AppUserId { get; set; }

            public virtual ApplicationUser ApplicationUser { get; set; }

            [Required(ErrorMessage = "İzin tipi gereklidir.")]
            //public IzinTipi IzinTipi { get; set; }
            public int IzinTipiId { get; set; }
        
    }
}
