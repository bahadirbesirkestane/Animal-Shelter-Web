using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Entities;
using AutoMapper;

namespace Animal_Shelter_WebProject.Mappers
{
    public class PetMapper : Profile
    {
        public PetMapper()
        {
            CreateMap<Pet, PetAddDto>().ReverseMap();
            CreateMap<Pet, PetGetDto>().ReverseMap();
        }


    }
}
