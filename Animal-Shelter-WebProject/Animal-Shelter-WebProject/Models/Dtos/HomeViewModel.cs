using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Models.Dtos
{
    public class HomeViewModel
    {
        public User UserInfo { get; set; }
        public List<PetGetDto> Pets { get; set; }
        public Pet PetInfo { get; set; }
    }
}
