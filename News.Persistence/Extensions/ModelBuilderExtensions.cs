using Microsoft.EntityFrameworkCore;
using News.Domain.Countries;

namespace News.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Bulgaria", Code = "bg" },
                new Country { Id = 2, Name = "Canada", Code = "ca" },
                new Country { Id = 3, Name = "France", Code = "fr" },
                new Country { Id = 4, Name = "Germany", Code = "de" },
                new Country { Id = 5, Name = "Great Britain", Code = "gb" },
                new Country { Id = 6, Name = "Greece", Code = "gr" },
                new Country { Id = 7, Name = "Italy", Code = "it" },
                new Country { Id = 8, Name = "Japan", Code = "jp" },
                new Country { Id = 9, Name = "Poland", Code = "pl" },
                new Country { Id = 10, Name = "USA", Code = "us" }
            );
        }
    }
}
