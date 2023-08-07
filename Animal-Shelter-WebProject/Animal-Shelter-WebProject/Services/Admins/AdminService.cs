using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos.Admin;
using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Entities;
using Animal_Shelter_WebProject.Services.Password;
using AutoMapper;

namespace Animal_Shelter_WebProject.Services.Admins
{
    public class AdminService : IAdminService
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public AdminService(Animal_Shelter_WebProjectDBContext context, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public void Create(AdminLoginDto adminLogin, int userId)
        {
            var admin = _mapper.Map<Admin>(adminLogin);


            admin.AdminRole = "A";
            

            _context.Admins.Add(admin);
            _context.SaveChanges();
        }
        
        public Admin GetById(int id)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.Id == id);

            return admin;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Admin GetByEmail(string adminEmail)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.AdminEmail == adminEmail);

            return admin;
        }

        public void Update(AdminUpdateDto AdminUpdateDto)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.AdminEmail == AdminUpdateDto.AdminEmail);

            admin.AdminEmail = AdminUpdateDto.AdminEmail;


            _context.SaveChanges();
        }
    }
}
