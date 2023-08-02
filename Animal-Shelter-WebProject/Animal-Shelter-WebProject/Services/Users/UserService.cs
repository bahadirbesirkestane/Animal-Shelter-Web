using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos.Users;
using Animal_Shelter_WebProject.Models.Entities;
using Animal_Shelter_WebProject.Services.Password;
using AutoMapper;

namespace Animal_Shelter_WebProject.Services.Users
{
    public class UserService : IUserService
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public UserService(Animal_Shelter_WebProjectDBContext context, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public User GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            return user;
        }
        public User GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            return user;
        }
        public void Create(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.Password = _passwordHasher.HashPassword(userRegisterDto.Password);

            var user = _mapper.Map<User>(userRegisterDto);

            _context.Users.Add(user);

            _context.SaveChanges();
        }
        public void Update(UserUpdateDto userUpdateDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == userUpdateDto.Email);

            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.Age = userUpdateDto.Age;
            user.Email = userUpdateDto.Email;

            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
