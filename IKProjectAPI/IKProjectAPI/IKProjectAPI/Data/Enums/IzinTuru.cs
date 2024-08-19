using System.ComponentModel.DataAnnotations;

namespace IKProjectAPI.Data.Enums
{
    public enum IzinTuru
    {
        [Display(Name = "Yıllık İzin")]
        YillikIzin,

        [Display(Name = "Hastalık İzni")]
        HastalikIzni,

        [Display(Name = "Ücretsiz İzin")]
        UcretsizIzin,

        [Display(Name = "Doğum İzni")]
        DogumIzni,

        [Display(Name = "Babalık İzni")]
        BabalikIzni,

        [Display(Name = "Evlenme İzni")]
        EvlenmeIzni,

        [Display(Name = "Ölüm İzni")]
        OlumIzni,

        [Display(Name = "Resmi Tatil İzni")]
        ResmiTatilIzni,

        [Display(Name = "Öğrenim İzni")]
        OgrenimIzni,

        [Display(Name = "Görev İzni")]
        GorevIzni,

        [Display(Name = "Kan Bağışı İzni")]
        KanBagisiIzni
    }
}
