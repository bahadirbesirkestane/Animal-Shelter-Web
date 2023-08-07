using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos;
using Animal_Shelter_WebProject.Models.Entities;
using Animal_Shelter_WebProject.Services.Adoptions;
using Animal_Shelter_WebProject.Services.Pets;
using Animal_Shelter_WebProject.Services.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter_WebProject.Controllers
{
    public class AdoptionController : Controller
    {
        // Servis ve Veritabanı context tanımlamaları

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

        // Hayvan Sahiplenme form sayfası

        [Authorize]
        public IActionResult Index(int petUserId, int petId,string? dil)
        {

            //Dil Geçişleri

            if (dil == "us")
            {
                ViewBag.LayoutName = "~/Views/Shared/_en_usLayout.cshtml";

                TempData["dil"] = "us";
            }
            else
            {
                ViewBag.LayoutName = "~/Views/Shared/_Layout.cshtml";

                TempData["dil"] = "tr";
            }


            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var adoptionsGet=_adoptionService.GetBySahiplenenId(userId);
            var adoptions= new List<Adoption>();

            // Kullanıcı bilgilerini ve seçilen hayvan için veri döndüren tanımlamalar

            foreach (var item in adoptionsGet)
            {
                if(item.Pet==petId)
                {
                    adoptions.Add(item);
                    break;
                }
            }
            var homeViewModel = new HomeViewModel()
            {
                PetInfo = _petService.GetByPetId(petId),
                UserInfo = _userService.GetById(petUserId),
                Adoptions= adoptions,

            };

            return View(homeViewModel);
        }

        // Kullanıcının hayvanlarına gelen talepler sayfası
        public IActionResult Talepler(int petUserId, int petId,string? dil)
        {
            // Dil seçenekleri

            if (dil == "us")
            {
                ViewBag.LayoutName = "~/Views/Shared/_en_usLayout.cshtml";

                TempData["dil"] = "us";
            }
            else
            {
                ViewBag.LayoutName = "~/Views/Shared/_Layout.cshtml";

                TempData["dil"] = "tr";
            }

            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            // Hayvan ve talep bilgileri servisten çekiliyor.
            var pet = _petService.GetByPetId(petId);
            var talepler = _adoptionService.GetByPetId(petId);


            List<User> sahiplenenler = new List<User>();
            List<string> sahiplenmeBilgileri = new List<string>();

            foreach (var talep in talepler)
            {
                sahiplenenler.Add(_userService.GetById(talep.Sahiplenen));
                sahiplenmeBilgileri.Add(talep.SahiplenmeBilgisi);
            }

            // Hayvanın, hayvan sahibinin ve sahiplenmek isteyen kullanıcın bilgileri view e gönderiliyor.

            var homeViewModel = new HomeViewModel()
            {
                Adoptions = talepler,
                PetInfo = pet,
                Users = sahiplenenler,
                Temp = sahiplenmeBilgileri,
            };

            return View(homeViewModel);
        }

        // Sahiplenme talebi Action ı

        [HttpPost]
        public IActionResult RequestAdoption(int petId, int sahiplenmeBilgisi)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var pet = _petService.GetByPetId(petId);


            // Yeni sahiplenme durumu oluşturuluyor.
            _adoptionService.Create(petId, userId, pet.UserId, sahiplenmeBilgisi);

            _petService.TalepDurumUpdate(petId, SurecDurumlari.TalepOlusturuldu);

            return RedirectToAction("Index", "Home");
        }

        // Kullanıcının yapmış olduğu sahiplenme isteklerini görüntülediği sayfa action ı
        public IActionResult MyAdoption(string? dil, string? log)
        {
            // Dil seçenekleri

            if (dil == "us")
            {
                ViewBag.LayoutName = "~/Views/Shared/_en_usLayout.cshtml";

                TempData["dil"] = "us";
            }
            else
            {
                ViewBag.LayoutName = "~/Views/Shared/_Layout.cshtml";

                TempData["dil"] = "tr";
            }

            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            var MyAdoption = _adoptionService.GetBySahiplenenId(userId);

            List<Pet> sahiplenenPet = new List<Pet>();

            List<SurecDurumlari> surecler = new List<SurecDurumlari>();

            // İstekler ve Hayvanlar servisten çekiliyor.

            foreach (var item in MyAdoption)
            {
                
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

        // Hayvan Sahibinin talepleri onaylama işlemi
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
                    
                    // Talep durumu hayvan sahibinin onaylaması ile birlikte admin onayı gerektiriyor.

                    _adoptionService.TalepDurumUpdate(adoption.Id, SurecDurumlari.AdminOnayiBekleniyor);
                    _petService.TalepDurumUpdate(adoption.Pet, SurecDurumlari.AdminOnayiBekleniyor);
                    break;
                }

            }
            

            return RedirectToAction("Index", "Home");
        }

        // Sahiplendirme işleminin onaylanma işlemi
        public IActionResult Onaylandi(int adoptionId)
        {
            var adoption=_adoptionService.GetById(adoptionId);

            _adoptionService.TalepDurumUpdate(adoption.Id, SurecDurumlari.SahiplendirmeOnaylandi);
            _petService.TalepDurumUpdate(adoption.Pet, SurecDurumlari.SahiplendirmeOnaylandi);

            return RedirectToAction("Index","Admin");
        }

    }
}
