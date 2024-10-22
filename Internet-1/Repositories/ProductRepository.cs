using AutoMapper;
using Internet_1.Models;
using Internet_1.ViewModels;

namespace Internet_1.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProductModel> GetList()
        {
            var products = _context.Products.ToList();
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return productModels;
        }
        public ProductModel GetById(int id)
        {
            var product = _context.Products.Where(s => s.Id == id).FirstOrDefault();
            var productModel = _mapper.Map<ProductModel>(product);
            return productModel;
        }
        public void Add(ProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Update(ProductModel model)
        {
            var product = _context.Products.Where(s => s.Id == model.Id).FirstOrDefault();
            if (product != null)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.IsActive = model.IsActive;
                product.CategoryId = model.CategoryId;

                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var product = _context.Products.Where(s => s.Id == id).FirstOrDefault();
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
