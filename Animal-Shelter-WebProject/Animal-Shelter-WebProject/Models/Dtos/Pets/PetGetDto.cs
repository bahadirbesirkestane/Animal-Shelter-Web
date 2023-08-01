using Animal_Shelter_WebProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter_WebProject.Models.Dtos.Pets
{
    public class PetGetDto
    {
        [BindProperty, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int AgeOrMonth { get; set; }
        public string Breed { get; set; }
        public double Weight { get; set; }
        public Kisirlik Kisirlik { get; set; }
        public string About { get; set; }
    }
}
