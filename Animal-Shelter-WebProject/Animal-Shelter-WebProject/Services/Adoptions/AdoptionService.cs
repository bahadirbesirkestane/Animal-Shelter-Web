using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Entities;
using AutoMapper;

namespace Animal_Shelter_WebProject.Services.Adoptions
{
    public class AdoptionService : IAdoptionService
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IMapper _mapper;
        public AdoptionService(Animal_Shelter_WebProjectDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Create(int petId, int sahiplenenId, int sahibiId, int sahiplenmeBilgisi)
        {

            Adoption adoption = new Adoption();
            //string sahiplenme = "";
            if (sahiplenmeBilgisi == 0)
            {
                adoption.SahiplenmeBilgisi = "Var";
            }
            else
            {
                adoption.SahiplenmeBilgisi = "Yok";
            }
            adoption.Pet = petId;
            adoption.Sahiplenen = sahiplenenId;
            adoption.Sahibi = sahibiId;

            adoption.SurecDurumlari = SurecDurumlari.TalepOlusturuldu;

            _context.Adoptions.Add(adoption);
            _context.SaveChanges();

        }
        public Adoption GetById(int Id)
        {
            var adoption = _context.Adoptions.Where(x => x.Id == Id).FirstOrDefault();

            return adoption;
        }
        public List<Adoption> GetByPetId(int petId)
        {
            var adoptions = _context.Adoptions.Where(x => x.Pet == petId).ToList();

            return adoptions;
        }
        public List<Adoption> GetBySahiplenenId(int sahiplenenId)
        {
            var adoptions = _context.Adoptions.Where(x => x.Sahiplenen == sahiplenenId).ToList();

            return adoptions;
        }

        public List<Adoption> GetAllAdoptions(SurecDurumlari surecDurumu)
        {
            var adoptions=new List<Adoption>();
            if (surecDurumu == SurecDurumlari.TalepOlusturuldu)
            {
                adoptions = (from Adoptions in _context.Adoptions
                                 select Adoptions).ToList();
            }
            else
            {
                adoptions=_context.Adoptions.Where(x=>x.SurecDurumlari == surecDurumu).ToList();
            }
            

            return adoptions;
        }

        public void TalepDurumUpdate(int adoptionId, SurecDurumlari surecDurumu)
        {
            var adoption = _context.Adoptions.Where(x => x.Id == adoptionId).FirstOrDefault();

            //_context.Adoptions.ElementAt(adoptionId).SurecDurumlari=surecDurumu;
            //_context.Pets.ElementAt(0).suer

            if (adoption != null)
            {
                adoption.SurecDurumlari = surecDurumu;
            }


            _context.SaveChanges();
        }

        public void DeletePet(int petId)
        {
            var adoptions = _context.Adoptions.Where(x => x.Pet == petId).ToList();

            foreach (var item in adoptions)
            {
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }

            
        }
    }
}
