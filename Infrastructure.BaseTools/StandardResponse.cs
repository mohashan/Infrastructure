using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseTools
{
    public class StandardResponse : StandardResponse<string>
    {
        public StandardResponse(bool? isSuccess, string? message, string? data) : base(isSuccess, message, data)
        {
        }
    }
    public class StandardResponse<T>
    {
        public StandardResponse(bool? isSuccess, string? message, T? data)
        {
            Message = message ?? string.Empty;
            Data = data;
            IsSuccess = isSuccess ?? false;
        }


        public string Message { get; set; }
        public T? Data { get; set; }
        public DateTimeOffset ResponseDateTime { get; } = DateTimeOffset.Now;
        public bool IsSuccess { get; set; } = true;
    }
}
