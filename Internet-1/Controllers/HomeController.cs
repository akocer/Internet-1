using Internet_1.Models;
using Internet_1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Internet_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _productRepository;
        public HomeController(ILogger<HomeController> logger, ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetList();
            products = products.Where(s => s.IsActive == true).ToList();
            return View(products);
        }


        public IActionResult TestWithLayout()
        {
            return View();
        }

        public IActionResult TestWithOutLayout()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}