using Animal_Shelter_WebProject.Models.Dtos.Admin;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Services.Admins
{
    public interface IAdminService
    {
        Admin GetById(int id);
        Admin GetByEmail(string adminEmail);
        void Update(AdminUpdateDto AdminUpdateDto);
        void Delete(int id);
    }
}
