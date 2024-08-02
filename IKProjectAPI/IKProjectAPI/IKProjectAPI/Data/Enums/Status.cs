using System.ComponentModel.DataAnnotations;

namespace IKProjectAPI.Data.Enums
{
    public enum Status
    {
        [Display(Name = "Aktif")]
        Active = 1,
        //[Display(Name = "Active-Modified")]
        //Modified = 2,
        [Display(Name = "Pasif")]
        Passive = 2,
        [Display(Name = "Cevap Bekleniyor")]
        AwatingApproval = 3,
    }
}
