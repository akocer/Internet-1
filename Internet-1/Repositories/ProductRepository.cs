using Internet_1.Models;
using Internet_1.ViewModels;

namespace Internet_1.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ProductModel> GetList()
        {
            var products = _context.Products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                IsActive = x.IsActive
            }).ToList();

            return products;
        }
        public ProductModel GetById(int id)
        {
            var product = _context.Products.Where(s => s.Id == id).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                IsActive = x.IsActive
            }).FirstOrDefault();

            return product;
        }
        public void Add(ProductModel model)
        {
            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive,
                Price = model.Price

            };
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
