namespace Animal_Shelter_WebProject.Models.Entities
{
    public class Adoption : IEntity
    {
        public int Pet { get; set; }
        public int Sahiplenen { get; set; }
        public int Sahibi { get; set; }
    }
}
