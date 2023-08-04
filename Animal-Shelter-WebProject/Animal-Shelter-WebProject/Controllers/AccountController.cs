﻿using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Models.Dtos.Admin;
using Animal_Shelter_WebProject.Models.Dtos.Users;
using Animal_Shelter_WebProject.Services.Admins;
using Animal_Shelter_WebProject.Services.Password;
using Animal_Shelter_WebProject.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animal_Shelter_WebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly Animal_Shelter_WebProjectDBContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;
        public AccountController(Animal_Shelter_WebProjectDBContext context, IPasswordHasher passwordHasher, IUserService userService, IAdminService adminService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _userService = userService;
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(AdminLoginDto adminLoginDto)
        {
            var admin = _adminService.GetByEmail(adminLoginDto.AdminEmail);

            if (admin == null)
            {
                return View();
            }

            bool adminPass = false;
            if (adminLoginDto.AdminPassword == admin.AdminPassword)
            {
                adminPass = true;
            }

            if (!adminPass)
            {
                return View();
            }

            SaveCookie(admin.AdminEmail, admin.Id);

            //return RedirectToAction("Index", "Information");
            return RedirectToAction("Index", "Admin");
        }




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var user = _userService.GetByEmail(userLoginDto.Email);



            if (user == null)
            {
                return View();
            }

            var password = _passwordHasher.VerifyPassword(userLoginDto.Password, user.Password);


            if (!password)
            {
                return View();
            }

            SaveCookie(user.Email, user.Id);

            //return RedirectToAction("Index", "Information");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            _userService.Create(userRegisterDto);

            var user = _userService.GetByEmail(userRegisterDto.Email);

            SaveCookie(user.Email, user.Id);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Kayitsiz", "Home");
        }
        [NonAction]
        private void SaveCookie(string email, int id)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, email),
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            HttpContext.SignInAsync(principal);

            HttpContext.Session.SetString("userId", id.ToString());
        }
    }
}
