using Animal_Shelter_WebProject.Models.Dtos.Users;
using Animal_Shelter_WebProject.Models.Entities;
using AutoMapper;

namespace Animal_Shelter_WebProject.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }

    }
}
