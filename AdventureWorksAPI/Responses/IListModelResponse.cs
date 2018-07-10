using System;
using System.Collections.Generic;

namespace AdventureWorksAPI.Responses
{
    public interface IListModelResponse<TModel> : IResponse
    {
        Int32 TotalCount { get; set; }

        IEnumerable<TModel> Model { get; set; }
    }
}
