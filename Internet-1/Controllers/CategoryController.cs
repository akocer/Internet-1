using AspNetCoreHero.ToastNotification.Abstractions;
using Internet_1.Repositories;
using Internet_1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Internet_1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly INotyfService _notyf;

        public CategoryController(CategoryRepository categoryRepository, INotyfService notyf, ProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _notyf = notyf;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetList();

            return View(categories);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _categoryRepository.Add(model);
            _notyf.Success("Kategori Eklendi...");
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var category = _categoryRepository.GetById(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _categoryRepository.Update(model);
            _notyf.Success("Kategori Güncellendi...");
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            return View(category);
        }


        [HttpPost]
        public IActionResult Delete(CategoryModel model)
        {

            var products = _productRepository.GetList();
            if (products.Count(c => c.CategoryId == model.Id) > 0)
            {
                _notyf.Error("Üzerinde Ürün Kayıtlı Olan Kategori Silinemez!");
                return RedirectToAction("Index");
            }

            _categoryRepository.Delete(model.Id);
            _notyf.Success("Kategori Silindi...");
            return RedirectToAction("Index");

        }
    }
}
