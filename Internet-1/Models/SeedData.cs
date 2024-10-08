using Microsoft.EntityFrameworkCore;

namespace Internet_1.Models
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                   new Product() { Id = 1, Name = "Kalem", Price = 10, Description = "Kalem açıklama", IsActive = true },
            new Product() { Id = 2, Name = "Defter", Price = 15, Description = "Defter açıklama", IsActive = true },
            new Product() { Id = 3, Name = "Silgi", Price = 20, Description = "Silgi açıklama", IsActive = false },
            new Product() { Id = 4, Name = "Kitap", Price = 30, Description = "Kitap açıklama", IsActive = false },
            new Product() { Id = 5, Name = "Boya", Price = 25, Description = "Boya açıklama", IsActive = false }
                );



        }
    }
}
