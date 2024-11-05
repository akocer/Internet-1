using Microsoft.AspNetCore.Mvc;

namespace Internet_1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
