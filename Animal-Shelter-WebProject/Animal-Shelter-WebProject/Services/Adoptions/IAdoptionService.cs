using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Services.Adoptions
{
    public interface IAdoptionService
    {
        public void Create(int petId, int sahiplenenId, int sahibiId, int sahiplenmeBilgisi);
        public List<Adoption> GetByPetId(int petId);
        public List<Adoption> GetBySahiplenenId(int sahiplenenId);
        public void TalepDurumUpdate(int adoptionId, SurecDurumlari surecDurumu);
    }
}

