using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Internet_1.Models;
using Internet_1.Repositories;
using Internet_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Internet_1.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public ProductController(ProductRepository productRepository, CategoryRepository categoryRepository, IMapper mapper, INotyfService notyf)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return View(productModels);
        }
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoriesSelectList = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categoriesSelectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var product = _mapper.Map<Product>(model);
            product.Created = DateTime.Now;
            product.Updated = DateTime.Now;
            await _productRepository.AddAsync(product);
            _notyf.Success("Ürün Eklendi...");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {

            var categories = await _categoryRepository.GetAllAsync();

            var categoriesSelectList = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categoriesSelectList;
            var product = await _productRepository.GetByIdAsync(id);
            var productModel = _mapper.Map<ProductModel>(product);
            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var product = await _productRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;
            product.CategoryId = model.CategoryId;
            product.Updated = DateTime.Now;

            await _productRepository.UpdateAsync(product);
            _notyf.Success("Ürün Güncellendi...");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productModel = _mapper.Map<ProductModel>(product);
            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductModel model)
        {

            await _productRepository.DeleteAsync(model.Id);
            _notyf.Success("Ürün Silindi...");
            return RedirectToAction("Index");
        }
    }
}
