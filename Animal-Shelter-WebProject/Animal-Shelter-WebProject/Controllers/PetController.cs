using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Dtos;
using Animal_Shelter_WebProject.Services.Adoptions;
using Animal_Shelter_WebProject.Services.Pets;
using Animal_Shelter_WebProject.Services.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Animal_Shelter_WebProject.Data;

namespace Animal_Shelter_WebProject.Controllers
{
    public class PetController : Controller
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IMapper _mapper;
        private readonly IPetService _service;
        private readonly IUserService _userService;
        private readonly IAdoptionService _adoptionService;

        public PetController(Animal_Shelter_WebProjectDBContext context, IMapper mapper, IPetService service, IUserService userService, IAdoptionService adoptionService)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
            _userService = userService;
            _adoptionService = adoptionService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var pets = _service.GetAllPets();

            var homeViewModel = new HomeViewModel()
            {
                Pets = _service.GetAll(userId),
                UserInfo = _userService.GetById(userId),

            };
            //var petsDto = _service.GetAll(userId);

            return View(pets);
        }
        [HttpGet]
        public IActionResult AddPet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPet(PetAddDto petAddDto)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            _service.Create(petAddDto, userId);

            return RedirectToAction("Index");
            //return View();
        }
        [HttpGet]
        public IActionResult MyPets()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var myPets = _service.GetById(userId);

            //var talepler= _adoptionService.GetBySahibiId(userId);

            //List<User> sahiplenenler= new List<User>();

            //foreach (var talep in talepler)
            //{
            //    sahiplenenler.Add(_userService.GetById(talep.Sahiplenen));
            //}

            //var homeViewModel = new HomeViewModel()
            //{
            //    Adoptions = talepler,
            //    PetList = myPets,
            //    Users = sahiplenenler,
            //};

            return View(myPets);
        }
        [HttpGet]
        public IActionResult Adopt()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            return View();
        }
        [HttpPost]
        public IActionResult Adopt(PetGetDto petGetDto)
        {
            return RedirectToAction("Adoption", "Index", petGetDto);
        }
    }
}
