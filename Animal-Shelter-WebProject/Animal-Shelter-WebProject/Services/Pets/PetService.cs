using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Dtos.Users;
using Animal_Shelter_WebProject.Models.Entities;
using AutoMapper;

namespace Animal_Shelter_WebProject.Services.Pets
{
    public class PetService : IPetService
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IMapper _mapper;
        public PetService(Animal_Shelter_WebProjectDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Hayvan ekleme işlemi
        public void Create(PetAddDto petAddDto, int userId)
        {
            var pet = _mapper.Map<Pet>(petAddDto);

            pet.UserId = userId;
            //pet.UserId = userId;
            pet.SurecDurumlari = SurecDurumlari.TalepYok;

            _context.Pets.Add(pet);
            _context.SaveChanges();
        }

        // Hayvanları listeleme
        public List<PetGetDto> GetAll(int userId)
        {
            var pets = (from Pets in _context.Pets
                        select Pets).ToList();

            var petDtos = _mapper.Map<List<PetGetDto>>(pets);

            foreach (var petDto in petDtos)
            {
                //if(petDto.BirthDate != null)
                //{
                //    int _age=DateTime.Now.Year-petDto.BirthDate.Year;

                //    if(_age==0)
                //    {
                //        _age=DateTime.Now.Month-petDto.BirthDate.Month;
                //    }
                //    else
                //    {
                //        petDto.AgeOrMonth = _age;

                //    }

                //}
            }

            return petDtos;

        }


        public Pet GetByPetId(int petId)
        {
            var pet = _context.Pets.Where(x => x.Id == petId).FirstOrDefault();

            return pet;
        }

        public List<Pet> GetAllPets()
        {
            var pets = (from Pets in _context.Pets
                        select Pets).ToList();

            return pets;
        }

        public List<Pet> GetById(int userId)
        {
            var myPets = _context.Pets.Where(x => x.UserId == userId).ToList();

            //var myPetDto = _mapper.Map<List<Pet>>(myPets);

            return myPets;
        }

        // Talep durumu her hayvan için ayrı tutulur ve herhangi kişinin sonuca daha yakın olan işlemi yazılır
        public void TalepDurumUpdate(int petId, SurecDurumlari surecDurumu)
        {
            var pet = _context.Pets.Where(x => x.Id == petId).FirstOrDefault();

            pet.SurecDurumlari = surecDurumu;

            //_context.Pets.

            //_context.Pets.ElementAt(petId).SurecDurumlari=surecDurumu;

            _context.SaveChanges();
        }

        // Hayvanın bilgilerini güncelleme işlemi
        public void Update(Pet pet, int petId)
        {
            

            var petGet = _context.Pets.Where(x => x.Id == petId).FirstOrDefault();

            petGet.Name= pet.Name;
            petGet.Breed= pet.Breed;
            petGet.Kisirlik= pet.Kisirlik;
            petGet.Gender= pet.Gender;
            petGet.AgeOrMonth= pet.AgeOrMonth;
            petGet.About=pet.About;
            petGet.Weight=pet.Weight;
            //petGet.BirthDate= pet.BirthDate;

            _context.SaveChanges();
        }

        // Hayvanı silme işlemi
        public void Delete(int petId)
        {
            var petGet = _context.Pets.Where(x => x.Id == petId).FirstOrDefault();

            if (petGet != null)
            {
                _context.Remove(petGet);
                _context.SaveChanges();
            }
        }
    }
}
