using Microsoft.EntityFrameworkCore;

namespace Internet_1.Models
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {




            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Categori 1", IsActive = true },
                new Category() { Id = 2, Name = "Categori 1", IsActive = true },
                new Category() { Id = 3, Name = "Categori 2", IsActive = true }
);


            modelBuilder.Entity<Product>().HasData(new Product() { Id = 1, Name = "Kalem", Price = 10, Description = "Kalem açıklama", IsActive = true, CategoryId = 1 },
            new Product() { Id = 2, Name = "Defter", Price = 15, Description = "Defter açıklama", IsActive = true, CategoryId = 1 },
            new Product() { Id = 3, Name = "Silgi", Price = 20, Description = "Silgi açıklama", IsActive = false, CategoryId = 2 },
            new Product() { Id = 4, Name = "Kitap", Price = 30, Description = "Kitap açıklama", IsActive = false, CategoryId = 2 },
            new Product() { Id = 5, Name = "Boya", Price = 25, Description = "Boya açıklama", IsActive = false, CategoryId = 3 }
                );



        }
    }
}
