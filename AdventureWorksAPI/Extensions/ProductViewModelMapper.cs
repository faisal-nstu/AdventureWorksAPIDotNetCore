using AdventureWorksAPI.Models;
using AdventureWorksAPI.ViewModels;

namespace AdventureWorksAPI.Extensions
{
    public static class ProductViewModelMapper
    {
        public static ProductViewModel ToViewModel(this Product entity)
        {
            return new ProductViewModel
            {
                ProductID = entity.ProductID,
                ProductName = entity.Name,
                ProductNumber = entity.ProductNumber
            };
        }

        public static Product ToEntity(this ProductViewModel viewModel)
        {
            return new Product
            {
                ProductID = viewModel.ProductID,
                Name = viewModel.ProductName,
                ProductNumber = viewModel.ProductNumber
            };
        }
    }
}
