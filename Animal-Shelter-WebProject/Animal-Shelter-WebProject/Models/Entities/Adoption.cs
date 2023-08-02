using Animal_Shelter_WebProject.Data;

namespace Animal_Shelter_WebProject.Models.Entities
{
    public class Adoption : IEntity
    {
        public int Pet { get; set; }
        public int Sahiplenen { get; set; }
        public int Sahibi { get; set; }
        public string SahiplenmeBilgisi { get; set; }
        public SurecDurumlari SurecDurumlari { get; set; }
    }
}
