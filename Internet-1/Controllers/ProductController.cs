using Internet_1.Models;
using Internet_1.Repositories;
using Internet_1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Internet_1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductController(ProductRepository productRepository, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetList();
            return View(products);
        }
        public IActionResult Add()
        {
            var categories = _categoryRepository.GetList().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productRepository.Add(model);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {

            var categories = _categoryRepository.GetList().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categories;
            var product = _productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productRepository.Update(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(ProductModel model)
        {

            _productRepository.Delete(model.Id);
            return RedirectToAction("Index");
        }
    }
}
