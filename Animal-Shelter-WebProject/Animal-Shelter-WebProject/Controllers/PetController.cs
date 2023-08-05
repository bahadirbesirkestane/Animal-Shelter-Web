﻿using Animal_Shelter_WebProject.Models.Dtos.Pets;
using Animal_Shelter_WebProject.Models.Dtos;
using Animal_Shelter_WebProject.Services.Adoptions;
using Animal_Shelter_WebProject.Services.Pets;
using Animal_Shelter_WebProject.Services.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Animal_Shelter_WebProject.Data;
using Microsoft.AspNetCore.Authorization;

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

            if(userId==0)
            {
                ViewBag.LayoutName = "~/Views/Shared/_LogOutLayout.cshtml";
            }
            else
            {
                ViewBag.LayoutName = "~/Views/Shared/_Layout.cshtml";
            }
            var pets = _service.GetAllPets();

            return View(pets);
        }

        [Authorize]
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
