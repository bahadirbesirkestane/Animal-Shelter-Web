using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Models.Dtos
{
    public class AdminGetModel
    {
        public List<User> SahibiUserList { get; set; }
        public List<User> SahiplenenUserList { get; set; }
        public List<Pet> PetInfoList { get; set; }
        public List<Adoption> Adoptions { get; set; }
    }
}
