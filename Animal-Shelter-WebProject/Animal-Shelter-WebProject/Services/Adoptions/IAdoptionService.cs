using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Services.Adoptions
{
    public interface IAdoptionService
    {
        public void Create(int petId, int sahiplenenId, int sahibiId, int sahiplenmeBilgisi);
        public Adoption GetById(int Id);
        public List<Adoption> GetByPetId(int petId);
        public List<Adoption> GetBySahiplenenId(int sahiplenenId);
        public List<Adoption> GetAllAdoptions(SurecDurumlari surecDurumu);
        public void TalepDurumUpdate(int adoptionId, SurecDurumlari surecDurumu);
        public void DeletePet(int petId);
    }
}

