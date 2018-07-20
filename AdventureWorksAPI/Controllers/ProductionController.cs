using AdventureWorksAPI.Extensions;
using AdventureWorksAPI.Models;
using AdventureWorksAPI.Responses;
using AdventureWorksAPI.Storage;
using AdventureWorksAPI.ViewModels;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductionController : Controller
    {
        private IAdventureWorksRepository AdventureWorksRepository;
        private ILoggerManager _logger;
        
        public ProductionController(IAdventureWorksRepository adventureWorksRepository, ILoggerManager logger)
        {
            AdventureWorksRepository = adventureWorksRepository;
            _logger = logger;
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Get all products with pagination
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product")]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = null, int? pageNumber = null, string name = null)
        {
            var products = new ListModelResponse<ProductViewModel>();

            try
            {
                var response = await AdventureWorksRepository
                        .GetProducts(pageSize, pageNumber, name);

                products.Model = response.Model.Select(item => item.ToViewModel()).ToList();
                products.TotalCount = response.TotalCount;
                products.Message = string.Format($"Showing {response.Model?.Count()} of total {response.TotalCount} items.");
                _logger?.LogInfo("Products: "+products.Message);
            }
            catch (Exception ex)
            {
                products.DidError = true;
                products.ErrorMessage = ex.Message;
                _logger.LogError(ex.Message);
            }

            return products.ToHttpResponse();
        }

        /// <summary>
        /// Get Product by product Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/{id}")]
        public async Task<IActionResult> GetProductById(Int32 id)
        {
            var response = new SingleModelResponse<ProductViewModel>();

            try
            {
                var entity = await AdventureWorksRepository.GetProductAsync(new Product { ProductID = id });

                response.Model = entity?.ToViewModel();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
                _logger.LogError(ex.Message);
            }

            return response.ToHttpResponse();
        }

        /// <summary>
        /// Creates a new product on product catalog
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> PostProductAsync([FromBody]ProductViewModel request)
        {
            var response = new SingleModelResponse<ProductViewModel>();

            try
            {
                var entity = await AdventureWorksRepository.AddProductAsync(request.ToEntity());

                response.Model = entity?.ToViewModel();
                response.Message = "The data was saved successfully";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
                _logger.LogError(ex.Message);
            }

            return response.ToHttpResponse();
        }

        /// <summary>
        /// Update Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("product/{id}")]
        public async Task<IActionResult> UpdateProductAsync(Int32 id, [FromBody]ProductViewModel request)
        {
            var response = new SingleModelResponse<ProductViewModel>();

            try
            {
                var entity = await AdventureWorksRepository.UpdateProductAsync(request.ToEntity());

                response.Model = entity?.ToViewModel();
                response.Message = "The record was updated successfully";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
                _logger.LogError(ex.Message);
            }

            return response.ToHttpResponse();
        }

        /// <summary>
        /// Delete product by Product ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("product/{id}")]
        public async Task<IActionResult> DeleteProductAsync(Int32 id)
        {
            var response = new SingleModelResponse<ProductViewModel>();

            try
            {
                var entity = await AdventureWorksRepository.DeleteProductAsync(new Product { ProductID = id });

                response.Model = entity?.ToViewModel();
                response.Message = "The record was deleted successfully";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
                _logger.LogError(ex.Message);
            }

            return response.ToHttpResponse();
        }
    }
}
