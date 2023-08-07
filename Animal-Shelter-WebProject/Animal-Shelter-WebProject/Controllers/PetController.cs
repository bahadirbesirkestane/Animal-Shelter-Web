using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Dtos;
using Animal_Shelter_WebProject.Services.Adoptions;
using Animal_Shelter_WebProject.Services.Pets;
using Animal_Shelter_WebProject.Services.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Animal_Shelter_WebProject.Data;
using Microsoft.AspNetCore.Authorization;
using Animal_Shelter_WebProject.Models.Entities;

namespace Animal_Shelter_WebProject.Controllers
{
    public class PetController : Controller
    {
        // Servis ve Veritabanı context tanımlamaları

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
        
        // Barınaktaki Hayvanları listeleyen action
        

        [HttpGet]
        public IActionResult Index(string? dil, string? log)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            ViewBag.UserId = userId;

            // Bu sayfaya log In olmadan da girelebileceği için authorize yapılmamıştır.
            // Dil seçenekleri ve layout login/logout durumuna göre seçilir

            if(userId==0)
            {
                if (dil == "us")
                {
                    ViewBag.LayoutName = "~/Views/Shared/_en_usLogOutLayout.cshtml";

                    TempData["dil"] = "us";
                }
                else
                {
                    ViewBag.LayoutName = "~/Views/Shared/_LogOutLayout.cshtml";

                    TempData["dil"] = "tr";
                }
            }
            else
            {
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
                
            }
            var pets = _service.GetAllPets();

            return View(pets);
        }

        // Hayvan Ekleme Acion Get 

        [Authorize]
        [HttpGet]
        public IActionResult AddPet(string? dil, string? log)
        {
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

            return View();
        }

        // Hayvan Ekleme Acion Post

        [HttpPost]
        public IActionResult AddPet(PetAddDto petAddDto)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            _service.Create(petAddDto, userId);

            return RedirectToAction("Index");
            //return View();
        }

        // Hayvanlarım sayfası kişinin hayvanları görüntülenir

        [HttpGet]
        public IActionResult MyPets(string? dil, string? log)
        {
            
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

            var myPets = _service.GetById(userId);

           

            return View(myPets);
        }

        

        [HttpGet]
        public IActionResult Adopt()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Adopt(PetGetDto petGetDto)
        {
            return RedirectToAction("Adoption", "Index", petGetDto);
        }


        // Hayvanın Bilgilerini güncelleme sayfası Get
        
        [HttpGet]
        public IActionResult EditPet(int petId, string? dil)
        {
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
            var pet=_service.GetByPetId(petId);


            return View(pet);
        }
        // Hayvanın Bilgilerini güncelleme sayfası Post
        [HttpPost]
        public IActionResult EditPet(Pet pet,int petId)
        {
            _service.Update(pet,petId);


            return RedirectToAction("MyPets");
        }

        // Hayvanı silme işlemi

        [HttpGet]
        public IActionResult DeletePet(int petId,string? dil)
        {
            _service.Delete(petId);
            _adoptionService.DeletePet(petId);
            return View();
        }

    }
}
