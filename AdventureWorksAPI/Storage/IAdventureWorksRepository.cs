using AdventureWorksAPI.Models;
using AdventureWorksAPI.Responses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksAPI.Storage
{
    public interface IAdventureWorksRepository : IDisposable
    {
        Task<IListModelResponse<Product>> GetProducts(int? pageSize, int? pageNumber, String name);

        Task<Product> GetProductAsync(Product entity);

        Task<Product> AddProductAsync(Product entity);

        Task<Product> UpdateProductAsync(Product changes);

        Task<Product> DeleteProductAsync(Product changes);
    }
}
