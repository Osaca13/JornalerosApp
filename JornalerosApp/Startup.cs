using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JornalerosApp.Areas.Identity;
using JornalerosApp.Services;
using JornalerosApp;
using System.Globalization;
using Microsoft.Extensions.Options;
using JornalerosApp.Shared.Services;
using JornalerosApp.Infrastructure.Data;
using AutoMapper;
using JornalerosApp.Shared.Models;
using BlazorStrap;

using Microsoft.AspNetCore.Mvc;
using System;
using JornalerosApp.Pages.Validators;
using BlazorDateRangePicker;
using MediatR;
using JornalerosApp.Application.Handlers;
using EventBusRabbitMQ;
using RabbitMQ.Client;

namespace JornalerosApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            //services.Configure<ISQLDatabaseSettings>(Configuration.GetSection(nameof(SQLDatabaseSettings)));
            //services.AddSingleton<ISQLDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SQLDatabaseSettings>>().Value);
            services.AddDbContext<ApplicationDbContext>(options =>
               {
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
               }, ServiceLifetime.Singleton);
            

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<ApplicationDbContext>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMvc();
            services.AddScoped<NifValidatorAttribute>();
            //services.AddScoped<PersonaValidator>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => {
                    if (actionContext.HttpContext.Request.Path == "/Personas")
                    {
                        return new BadRequestObjectResult(actionContext.ModelState);
                    }
                    else
                    {
                        return new BadRequestObjectResult(
                            new ValidationProblemDetails(actionContext.ModelState));
                    }
                };
            });
            services.AddLocalization();
            var supportedCultures = new List<CultureInfo> { new CultureInfo("es"), new CultureInfo("ca") };
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es");
                opt.SupportedUICultures = supportedCultures;
;            });
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                //options.Password.RequiredUniqueChars = 6;
            });
            services.AddBootstrapCss();
            //services.AddAutoMapper(typeof(ModelMapper));
            //services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<ISqlDataAccess, SqlDataAccess>();
            services.AddScoped<ISQLDatabaseServices, SQLDatabaseServices>();
            services.AddLogging();
            
            
            services.AddScoped<IGetDbServices<RelacionMunicipioProvincia>, MunicipiosDbServices>();
            //services.AddTransient<IDbServices<Persona>, PersonaDbServices>();
            
            services.AddHttpClient();
            services.AddApiClient();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Jornaleros API", Version = "v1" });
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
            
            //services.AddDateRangePicker(config =>
            //{
            //    config.Attributes = new Dictionary<string, object>
            //    {
            //        { "class", "form-control form-control-sm" }
            //    };
            //    config.Name = "DateRangePickerConfig";
            //});

        }        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jornaleros Api v1");
            });
        }

        

        
    }
}
