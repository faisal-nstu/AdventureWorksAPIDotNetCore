using System.Collections.Generic;

namespace AdventureWorksAPI.Responses
{
    public class ListModelResponse<TModel> : IListModelResponse<TModel>
    {
        public int TotalCount { get ; set ; }
        public IEnumerable<TModel> Model { get ; set ; }
        public string Message { get ; set ; }
        public bool DidError { get ; set ; }
        public string ErrorMessage { get ; set ; }
    }
}
