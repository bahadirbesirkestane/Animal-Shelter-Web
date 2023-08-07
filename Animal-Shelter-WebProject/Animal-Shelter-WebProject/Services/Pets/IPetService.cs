using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Dtos.Users;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Services.Pets
{
    public interface IPetService
    {
        public void Create(PetAddDto petAddDto, int userId);
        public List<PetGetDto> GetAll(int userId);
        public Pet GetByPetId(int petId);
        public List<Pet> GetAllPets();
        public List<Pet> GetById(int userId);
        public void TalepDurumUpdate(int petId, SurecDurumlari surecDurumu);
        public void Update(Pet pet, int petId);
        public void Delete(int petId);

    }
}
