using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data {  get; set; }
        public static ApiResponse OK(bool success, string? message=null, Object? Data = null)
        {
            return new ApiResponse { Success =true,Data = Data,Message=message??"succes"};
        }
        public static ApiResponse Fail(bool success, string? message)
        {
            return new ApiResponse { Success = false, Message = message };
        }
    }
}
