using AutoMapper;
using EmpresaApp.Services;
using EmpresasApp.Data;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace EmpresasApp
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
            services.AddDbContext<EmpresasDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Singleton);

            services.AddTransient<IDbServices<Empresa>, EmpresaDbServices>();
            services.AddControllers();
            services.AddMvc();
            services.AddHttpClient();
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Empresa API", Version = "v1" });
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
            services.AddSingleton<EventBusRabbitMQProducer>();


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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Empresa Api v1");
            });
        }
    }
}
