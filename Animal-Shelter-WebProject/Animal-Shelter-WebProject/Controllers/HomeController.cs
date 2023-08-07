using Animal_Shelter_WebProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Animal_Shelter_WebProject.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Ana Sayfa Log In

        [Authorize]
        public IActionResult Index(string? dil)
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

        // Ana sayfa Log Out

        public IActionResult Kayitsiz(string? dil)
        {
            HttpContext.SignOutAsync();

            HttpContext.Session.Clear();

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

            return View();
        }

        public IActionResult Dil(string dil, string log)
        {
            if (dil == "us")
            {
                if (log == "In")
                {
                    ViewBag.LangLayout = "~/Views/Shared/_en_usLayout.cshtml";
                }
                else // out
                {
                    ViewBag.LangLayout = "~/Views/Shared/_en_usLogOutLayout.cshtml";
                }
            }
            else
            {
                //ViewBag.LangLayout= "~/Views/Shared/_LogOutLayout.cshtml";
            }
           

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}