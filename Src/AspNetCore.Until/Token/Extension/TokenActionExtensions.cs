using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Until
{
    public static class TokenActionExtensions
    {
        public static IServiceCollection AddTokenAction<TTokenCheck>(this IServiceCollection services) where TTokenCheck : class,ITokenCheck
        {
            services.AddScoped<ITokenCheck, TTokenCheck>();
            return services;
        }
    }
}
