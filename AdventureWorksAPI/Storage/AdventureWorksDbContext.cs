using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AdventureWorksAPI.Storage
{
    public class AdventureWorksDbContext : DbContext
    {
        public string ConnectionString { get; set; }
        public IEntityMapper EntityMapper { get; }

        public AdventureWorksDbContext(IOptions<AppSettings> appSettings, IEntityMapper entityMapper) 
        {
            ConnectionString = appSettings.Value.ConnectionString;
            EntityMapper = entityMapper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityMapper.MapEntities(modelBuilder); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
