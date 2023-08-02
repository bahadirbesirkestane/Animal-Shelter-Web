using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Models.Dtos
{
    public class HomeViewModel
    {
        public User UserInfo { get; set; }
        public List<PetGetDto> Pets { get; set; }
        public Pet PetInfo { get; set; }
        public List<Pet> PetList { get; set; }
        public List<Adoption> Adoptions { get; set; }
        public List<User> Users { get; set; }
        public List<string> Temp { get; set; }
        public List<SurecDurumlari> SurecTutucu { get; set; }
    }
}
