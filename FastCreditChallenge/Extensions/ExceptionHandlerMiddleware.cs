using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace FastCreditChallenge.Extensions
{
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error => {
                error.Run(async context => {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeatures != null)
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "An error occured please try again;"
                        }));
                    }
                });
            });
        }
    }
}
