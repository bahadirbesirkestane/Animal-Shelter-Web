using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos;
using Animal_Shelter_WebProject.Models.Entities;
using Animal_Shelter_WebProject.Services.Adoptions;
using Animal_Shelter_WebProject.Services.Pets;
using Animal_Shelter_WebProject.Services.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter_WebProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IMapper _mapper;
        private readonly IAdoptionService _adoptionService;
        private readonly IPetService _petService;
        private readonly IUserService _userService;

        public AdminController(Animal_Shelter_WebProjectDBContext context, IMapper mapper, IAdoptionService adoptionService, IPetService petService, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _petService = petService;
            _userService = userService;
            _adoptionService = adoptionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OnayTalepleri()
        {
            var adoptions = _adoptionService.GetAllAdoptions(SurecDurumlari.AdminOnayiBekleniyor);

            //List<List<User>> SahibiUserSahiplenen = new List<List<User>>();

            var sahibiList=new List<User>();
            var sahiplenenList=new List<User>();
            var petInfoList=new List<Pet>();


            foreach (var adoption in adoptions)
            {
                
                var sahibiUser = _userService.GetById(adoption.Sahibi);
                sahibiList.Add(sahibiUser);

                var sahiplenenUser = _userService.GetById(adoption.Sahiplenen);
                sahiplenenList.Add(sahiplenenUser);

                var petInfo = _petService.GetByPetId(adoption.Pet);
                petInfoList.Add(petInfo);

            }

            var adminGetModel = new AdminGetModel()
            {
                SahibiUserList = sahibiList,
                SahiplenenUserList = sahiplenenList,
                PetInfoList = petInfoList,
                Adoptions=adoptions,
            };

            


            return View(adminGetModel);
        }

        //[HttpPost]
        //public IActionResult OnayTalepleri()
        //{
        //    return RedirectToAction();
        //}
    }
}
