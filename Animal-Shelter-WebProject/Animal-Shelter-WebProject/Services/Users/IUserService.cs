using Animal_Shelter_WebProject.Models.Dtos.Users;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Services.Users
{
    public interface IUserService
    {
        User GetById(int id);
        User GetByEmail(string email);
        void Create(UserRegisterDto userRegisterDto);
        void Update(UserUpdateDto userUpdateDto);
        void Delete(int id);
    }
}
