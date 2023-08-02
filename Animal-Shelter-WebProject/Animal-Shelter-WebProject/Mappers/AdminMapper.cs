using Animal_Shelter_WebProject.Models.Dtos.Admin;
using Animal_Shelter_WebProject.Models.Entities;
using AutoMapper;

namespace Animal_Shelter_WebProject.Mappers
{
    public class AdminMapper : Profile
    {
        public AdminMapper()
        {
            CreateMap<Admin, AdminLoginDto>().ReverseMap();

        }
    }
}
