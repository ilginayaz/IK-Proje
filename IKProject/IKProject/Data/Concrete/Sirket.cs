using IKProject.Data.Enums;

namespace IKProject.Data.Concrete
{
    public class Sirket
    {
        public Guid Id { get; set; }
        public string SirketAdi { get; set; }
        public string SirketNumarasi { get; set; }
        public string VergiNo { get; set; }
        public string VergiOfisi { get; set; }
        public int CalisanSayisi { get; set; }
        public string SirketEmail { get; set; }
        public Sehirler Sehir { get; set; }
        public string Address { get; set; }
        public string PostaKodu { get; set; }

        public Status Status { get; set; } = Status.AwatingApproval;

        public virtual ICollection<ApplicationUser> SirketYoneticileri { get; set; }
        public virtual ICollection<ApplicationUser>? SirketCalisanlari { get; set; }
        public virtual ICollection<IzinTipi> IzinTipis { get; set; }
    }
}
