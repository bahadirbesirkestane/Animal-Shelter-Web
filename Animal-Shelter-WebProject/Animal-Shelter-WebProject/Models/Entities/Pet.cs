using Animal_Shelter_WebProject.Data;
using System.Reflection;

namespace Animal_Shelter_WebProject.Models.Entities
{
    public class Pet : IEntity
    {
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public int? AgeOrMonth { get; set; }
        public string? Breed { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? Weight { get; set; }
        public Kisirlik? Kisirlik { get; set; }
        public string? About { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
