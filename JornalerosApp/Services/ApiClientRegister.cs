using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public static class ApiClientRegister
    {

        public static IServiceCollection AddApiClient(this IServiceCollection services)
        {
            services.AddHttpClient("clientFirst", client =>
            {
                
                client.BaseAddress = new Uri(@"https://localhost:44392/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

            });

            return services;
        }
    }
}
