using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using JornalerosApp.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfertasApp.Data;
using OfertasApp.Extentions;
using OfertasApp.Handlers;
using OfertasApp.RabbitMQ;
using OfertasApp.Services;
using OfertasApp.Services.Interfaces;
using RabbitMQ.Client;
using System.Reflection;

namespace OfertasApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OfertaDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Singleton);

            services.AddControllers();

            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Add MediatR
            services.AddMediatR(typeof(OfertaHandlers).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CheckOutOfertaHandler).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IOfertaDbServices), typeof(OfertaDbServices));
            services.AddTransient<IOfertaDbServices, OfertaDbServices>();
            services.AddScoped(typeof(IGetDbServices<>), typeof(GetDbServices<>));
            services.AddScoped(typeof(IDbServices<>), typeof(DbServices<>));
            services.AddScoped(typeof(IDbServices<>), typeof(OfertaDbServices));
            services.AddTransient<IDbServices<Oferta>, OfertaDbServices>();

            services.AddHttpClient();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Oferta API", Version = "v1" });
            });
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };
                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }
                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }
                return new RabbitMQConnection(factory);
            });
            services.AddSingleton<EventBusRabbitMQConsumer>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitListener();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oferta Api v1");
            });
        }
    }
}
