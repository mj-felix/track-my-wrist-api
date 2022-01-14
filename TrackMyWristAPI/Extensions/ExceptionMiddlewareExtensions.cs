using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace TrackMyWristAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    await context.Response.WriteAsJsonAsync(
                                        new
                                        {
                                            Message = ex.Error.Message,
                                            ExceptionType = ex.Error.GetType().ToString()
                                        }
                                    );
                                }
                            }
                        );
                    }
                );
            }
        }
    }
}