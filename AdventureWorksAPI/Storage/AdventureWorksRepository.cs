using System.Linq;
using System.Threading.Tasks;
using AdventureWorksAPI.Models;
using System;
using Microsoft.EntityFrameworkCore;
using AdventureWorksAPI.Responses;

namespace AdventureWorksAPI.Storage
{
    public class AdventureWorksRepository : IAdventureWorksRepository
    {
        private readonly AdventureWorksDbContext DbContext;
        private Boolean Disposed;

        public AdventureWorksRepository(AdventureWorksDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                DbContext?.Dispose();

                Disposed = true;
            }
        }

        public async Task<Product> AddProductAsync(Product entity)
        {
            entity.DaysToManufacture = 0;
            entity.FinishedGoodsFlag = false;
            entity.ListPrice = 0.0m;
            entity.MakeFlag = false;
            entity.ModifiedDate = DateTime.Now;
            entity.ReorderPoint = 1;
            entity.rowguid = Guid.NewGuid();
            entity.SafetyStockLevel = 1;
            entity.SellStartDate = DateTime.Now;
            entity.StandardCost = 0.0m;

            DbContext.Set<Product>().Add(entity);

            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Product> DeleteProductAsync(Product changes)
        {
            var entity = await GetProductAsync(changes);

            if (entity != null)
            {
                DbContext.Set<Product>().Remove(entity);

                await DbContext.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<Product> GetProductAsync(Product entity)
        {
            return await DbContext.Set<Product>().FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);
        }

        public async Task<IListModelResponse<Product>> GetProducts(int? pageSize, int? pageNumber, string name)
        {
            var response = new ListModelResponse<Product>();

            var items = await DbContext.Set<Product>()
                .ToListAsync();
            
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(item => item.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            response.TotalCount = items?.Count() ?? 0;

            if (pageSize != null && pageNumber != null)
            {
                items = items?.Skip((pageNumber.GetValueOrDefault() - 1) * pageSize.GetValueOrDefault()).Take(pageSize.GetValueOrDefault()).ToList();
            }

            response.Model = items;

            return response;
        }

        public async Task<Product> UpdateProductAsync(Product changes)
        {
            var entity = await GetProductAsync(changes);

            if (entity != null)
            {
                entity.Name = changes.Name;
                entity.ProductNumber = changes.ProductNumber;

                await DbContext.SaveChangesAsync();
            }

            return entity;
        }
    }
}
