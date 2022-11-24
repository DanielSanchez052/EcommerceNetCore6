using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiExcepcion : ApiResponse
    {
        public string Details { get; set; }
        public ApiExcepcion(int statusCode, string message = null, string details = null) 
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}