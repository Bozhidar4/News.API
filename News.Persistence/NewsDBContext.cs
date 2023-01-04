using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using News.Domain.Articles;
using News.Domain.Countries;
using News.Domain.Sources;
using News.Persistence.Extensions;
using News.Shared.Persistence;
using System.Reflection;

namespace News.Persistence
{
    public class NewsDBContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Article> Articles { get; set; }

        private readonly IConfiguration _configuration;

        public NewsDBContext(
            DbContextOptions<NewsDBContext> dbContextOptions,
            IConfiguration configuration)
            : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString(DbConfig.CONNECTION_STRING_NEWS));
        }
    }
}
