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
    public class AdoptionController : Controller
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IMapper _mapper;
        private readonly IAdoptionService _adoptionService;
        private readonly IPetService _petService;
        private readonly IUserService _userService;

        public AdoptionController(Animal_Shelter_WebProjectDBContext context, IMapper mapper, IAdoptionService adoptionService, IPetService petService, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _petService = petService;
            _userService = userService;
            _adoptionService = adoptionService;
        }

        public IActionResult Index(int petUserId, int petId)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var homeViewModel = new HomeViewModel()
            {
                PetInfo = _petService.GetByPetId(petId),
                UserInfo = _userService.GetById(petUserId),

            };

            return View(homeViewModel);
        }

        public IActionResult Talepler(int petUserId, int petId)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var pet = _petService.GetByPetId(petId);

            var talepler = _adoptionService.GetByPetId(petId);

            List<User> sahiplenenler = new List<User>();
            List<string> sahiplenmeBilgileri = new List<string>();

            foreach (var talep in talepler)
            {
                sahiplenenler.Add(_userService.GetById(talep.Sahiplenen));
                sahiplenmeBilgileri.Add(talep.SahiplenmeBilgisi);
            }

            var homeViewModel = new HomeViewModel()
            {
                Adoptions = talepler,
                PetInfo = pet,
                Users = sahiplenenler,
                Temp = sahiplenmeBilgileri,
            };

            return View(homeViewModel);
        }

        [HttpPost]
        public IActionResult RequestAdoption(int petId, int sahiplenmeBilgisi)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var pet = _petService.GetByPetId(petId);



            _adoptionService.Create(petId, userId, pet.UserId, sahiplenmeBilgisi);

            _petService.TalepDurumUpdate(petId, SurecDurumlari.TalepOlusturuldu);

            var homeViewModel = new HomeViewModel()
            {
                PetInfo = pet,
                UserInfo = _userService.GetById(pet.UserId),

            };

            return RedirectToAction("Index", "Home");
        }

        public IActionResult MyAdoption()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var MyAdoption = _adoptionService.GetBySahiplenenId(userId);

            List<Pet> sahiplenenPet = new List<Pet>();

            List<SurecDurumlari> surecler = new List<SurecDurumlari>();

            foreach (var item in MyAdoption)
            {
                // buraya bak
                sahiplenenPet.Add(_petService.GetByPetId(item.Pet));
                surecler.Add(item.SurecDurumlari);
            }
            //SurecTutucu

            var homeViewModel = new HomeViewModel()
            {
                PetList = sahiplenenPet,
                SurecTutucu = surecler,

            };

            return View(homeViewModel);
        }

        //[HttpPost]
        public IActionResult TalepOnayi(int sahiplenenUserId, int PetId)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var pets = _adoptionService.GetByPetId(PetId);

            Adoption adoption = new Adoption();

            foreach (var item in pets)
            {
                if (item.Sahiplenen == sahiplenenUserId)
                {
                    adoption = item;
                    //adoption.SurecDurumlari = SurecDurumlari.AdminOnayiBekleniyor;,

                    _adoptionService.TalepDurumUpdate(adoption.Id, SurecDurumlari.AdminOnayiBekleniyor);
                    _petService.TalepDurumUpdate(adoption.Pet, SurecDurumlari.AdminOnayiBekleniyor);
                    break;
                }

            }
            //var pet=_adoptionService.
            //_petService.TalepDurumUpdate()

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Onaylandi(int adoptionId)
        {
            var adoption=_adoptionService.GetById(adoptionId);

            _adoptionService.TalepDurumUpdate(adoption.Id, SurecDurumlari.SahiplendirmeOnaylandi);
            _petService.TalepDurumUpdate(adoption.Pet, SurecDurumlari.SahiplendirmeOnaylandi);

            return View();
        }

    }
}
