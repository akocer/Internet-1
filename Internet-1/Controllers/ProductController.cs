using Internet_1.Models;
using Internet_1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Internet_1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetList();
            return View(products);
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product model)
        {

            _productRepository.Delete(model.Id);
            return RedirectToAction("Index");
        }
    }
}
