using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksAPI.Responses
{
    public class SingleModelResponse<TModel> : ISingleModelResponse<TModel>
    {
        public TModel Model { get ; set ; }
        public string Message { get ; set ; }
        public bool DidError { get ; set ; }
        public string ErrorMessage { get ; set ; }
    }
}
