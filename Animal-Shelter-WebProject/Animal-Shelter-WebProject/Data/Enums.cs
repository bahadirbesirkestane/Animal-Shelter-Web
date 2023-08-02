namespace Animal_Shelter_WebProject.Data
{
    public enum Gender
    {
        Erkek = 0,
        Dişi = 1,
        Diger = 2,
    }

    public enum Kisirlik
    {
        Evet = 0,
        Hayir = 1,
        Belirtilmedi = 2,
    }

    public enum SurecDurumlari
    {
        TalepYok = 0,
        TalepOlusturuldu = 1,
        SahibininOnayiBekleniyor = 2,
        AdminOnayiBekleniyor = 3,
        SahiplendirmeOnaylandi = 4,
    }
}
