
using Contracts.Exceptions;
using Contracts.GlobalErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace TimeSheet.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    { 
                        if(contextFeature.Error is NotFoundException){
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.NotFound,
                                ErrorMessage = contextFeature.Error.Message
                            }.ToString());
                        }
                        else if(contextFeature.Error is EntityAlreadyExists){
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.Conflict,
                                ErrorMessage = contextFeature.Error.Message
                            }.ToString());
                        }
                        else if(contextFeature.Error is AuthException){
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.Unauthorized,
                                ErrorMessage = contextFeature.Error.Message
                            }.ToString());
                        }
                        else if (contextFeature.Error is Exception)
                        {
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.InternalServerError,
                                ErrorMessage = contextFeature.Error.Message
                            }.ToString());
                        }
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");

                    }
                });
            });
        }
    }
}
