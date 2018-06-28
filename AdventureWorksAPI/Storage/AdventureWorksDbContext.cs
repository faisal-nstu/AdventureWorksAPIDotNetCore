using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AdventureWorksAPI.Storage
{
    public class AdventureWorksDbContext : DbContext
    {
        public string ConnectionString { get; set; }

        public AdventureWorksDbContext(IOptions<AppSettings> appSettings) 
        {
            ConnectionString = appSettings.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapProduct();

            base.OnModelCreating(modelBuilder);
        }
    }
}
