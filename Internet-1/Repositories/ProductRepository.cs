using Internet_1.Models;

namespace Internet_1.Repositories
{
    public class ProductRepository
    {
        public static List<Product> products = new List<Product> {
            new Product() {Id=1,Name="Kalem",Price=10, Description="Kalem açıklama", IsActive=true },
            new Product() {Id=2,Name="Defter",Price=15, Description="Defter açıklama", IsActive=true },
            new Product() {Id=3,Name="Silgi",Price=20, Description="Silgi açıklama", IsActive=false },
            new Product() {Id=4,Name="Kitap",Price=30, Description="Kitap açıklama", IsActive=false },
            new Product() {Id=5,Name="Boya",Price=25, Description="Boya açıklama", IsActive=false }
        };
        public List<Product> GetList()
        {
            return products.ToList();
        }
        public Product GetById(int id)
        {
            var product = products.Where(s => s.Id == id).FirstOrDefault();
            return product;
        }
        public void Add(Product model)
        {
            Random rn = new Random();
            int id = rn.Next(1000);
            model.Id = id;
            products.Add(model);
        }
        public void Update(Product model)
        {
            var product = GetById(model.Id);
            if (product != null)
            {
                var index = products.FindIndex(x => x.Id == product.Id);
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.IsActive = model.IsActive;
                products[index] = product;
            }
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                products.Remove(product);
            }
        }
    }
}
