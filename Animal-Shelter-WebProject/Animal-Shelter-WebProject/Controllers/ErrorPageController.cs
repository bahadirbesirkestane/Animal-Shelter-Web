using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter_WebProject.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error1(int code)
        {
            return View();
        }
    }
}
