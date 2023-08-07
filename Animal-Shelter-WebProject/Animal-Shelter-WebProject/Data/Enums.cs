namespace Animal_Shelter_WebProject.Data
{
    // Cinsiyet 
    public enum Gender
    {
        Erkek = 0,
        Dişi = 1,
        Diger = 2,
    }

    // Hayvanıb kısırlık durumları
    public enum Kisirlik
    {
        Evet = 0,
        Hayir = 1,
        Belirtilmedi = 2,
    }

    // Sahiplenme surec durumları
    // Hayvanlar ilk olusturuldugunda "talep yok" olarak atanır.
    public enum SurecDurumlari
    {
        TalepYok = 0,
        TalepOlusturuldu = 1,
        SahibininOnayiBekleniyor = 2,
        AdminOnayiBekleniyor = 3,
        SahiplendirmeOnaylandi = 4,
    }
}
