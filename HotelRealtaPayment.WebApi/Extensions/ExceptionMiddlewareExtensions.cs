using Microsoft.AspNetCore.Diagnostics;
using HotelRealtaPayment.Domain.ErrorModel;
using HotelRealtaPayment.Services.Abstraction;
using System.Net;

namespace HotelRealtaPayment.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = context.Response.BodyWriter.ToString()
                        }.ToString());
                    }
                });
            });
        }
    }
}