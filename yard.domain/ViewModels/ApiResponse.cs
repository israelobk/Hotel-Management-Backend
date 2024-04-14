using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.ViewModels
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; } 
        public int StatusCode { get; set; }

        public ApiResponse(int statusCode, bool success, string msg, object data)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
        }
        public ApiResponse()
        {
        }

        public static ApiResponse Fail(string? errorMessage = null, int statusCode = 404)
        {
            return new ApiResponse { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static ApiResponse Success(object data, string? successMessage = null, int statusCode = 200)
        {
            return new ApiResponse { Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
    }
}
