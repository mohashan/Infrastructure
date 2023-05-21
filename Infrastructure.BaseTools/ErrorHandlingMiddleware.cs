using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.BaseTools
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                string ERROR_MESSAGE = $"An error has occured while processing your request. Trace Code : {context.TraceIdentifier}";

                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case FileNotFoundException:
                    case KeyNotFoundException:
                    case EntryPointNotFoundException:
                    case VersionNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentOutOfRangeException:
                        response.StatusCode = (int)HttpStatusCode.RequestedRangeNotSatisfiable;
                        break;
                    case DirectoryNotFoundException:
                    case ArgumentNullException:
                    case ArgumentException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case AccessViolationException:
                    case MemberAccessException:
                    case TypeAccessException:
                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case DllNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.FailedDependency;
                        break;
                    case TimeoutException:
                        response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new StandardResponse(false, ERROR_MESSAGE/*error?.Message*/, null));
                await response.WriteAsync(result);
            }
        }
    }

}
