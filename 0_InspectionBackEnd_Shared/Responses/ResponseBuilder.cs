using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_InspectionBackEnd_Shared.Responses
{
    public class ResponseBuilder<TEntity>
    {
        public Error Error { get; set; } = new Error();
        public string? Message { get; set; }
        public TEntity? Data { get; set; }
    }

    public class Error
    {
        public bool IsError { get; set; } = false;
        public string? ErrorType { get; set; }
        public string? ErrorId { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
