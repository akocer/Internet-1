using Internet_1.Models;

namespace Internet_1.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetList()
        {
            var products = _context.Products.ToList();
            return products;
        }
        public Product GetById(int id)
        {
            var product = _context.Products.Where(s => s.Id == id).FirstOrDefault();
            return product;
        }
        public void Add(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
        }
        public void Update(Product model)
        {
            var product = GetById(model.Id);
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
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
