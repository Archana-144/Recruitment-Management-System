using Recruitment.Common.Models;
using Recruitment.Store.Abstraction;

namespace Recruitment.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IErrorLogStore errorLogStore)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    ControllerName =
                        context.Request.Path,
                    MethodName =
                        context.Request.Method
                };

                await errorLogStore.InsertErrorLogAsync(errorLog);

                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }
    }
}