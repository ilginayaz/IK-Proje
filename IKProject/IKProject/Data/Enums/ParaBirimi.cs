using System.ComponentModel.DataAnnotations;

namespace IKProject.Data.Enums
{
    public enum ParaBirimi
    {
        [Display(Name = "Türk Lirası")]
        TRY,  // Türk Lirası

        [Display(Name = "Amerikan Doları")]
        USD,  // Amerikan Doları

        [Display(Name = "Euro")]
        EUR,  // Euro

        [Display(Name = "İngiliz Sterlini")]
        GBP,  // İngiliz Sterlini

        [Display(Name = "Japon Yeni")]
        JPY,  // Japon Yeni

        [Display(Name = "Kanada Doları")]
        CAD,  // Kanada Doları

        [Display(Name = "İsviçre Frangı")]
        CHF   // İsviçre Frangı
    }
}
