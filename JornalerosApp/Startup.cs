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
using System.Globalization;
using Microsoft.Extensions.Options;
using JornalerosApp.Shared.Services;
using JornalerosApp.Data;
using AutoMapper;
using JornalerosApp.Shared.Models;
using BlazorStrap;
using Syncfusion.Blazor;

namespace JornalerosApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<ISQLDatabaseSettings>(Configuration.GetSection(nameof(SQLDatabaseSettings)));
            //services.AddSingleton<ISQLDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SQLDatabaseSettings>>().Value);
            services.AddDbContext<ApplicationDbContext>(options =>
               {
                   options.UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection"));    
               });
           
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<ApplicationDbContext>();
            services.AddControllers().AddNewtonsoftJson();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMvc();
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
            services.AddAutoMapper(typeof(ModelMapper));
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<ISQLDatabaseServices, SQLDatabaseServices>();
            services.AddLogging();
            services.AddTransient<ValidacionNIF>();
            services.AddScoped<IGetDbServices<RelacionMunicipioProvincia>, MunicipiosDbServices>();
            services.AddScoped<IDbServices<Persona>, PersonaDbServices>();
            services.AddScoped<IDbServices<Empresa>, EmpresaDbServices>();
            services.AddScoped<IDbServices<Oferta>, OfertaDbServices>();
            services.AddHttpClient();
            services.AddApiClient();
            services.AddSyncfusionBlazor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                var properties = env.WebRootPath;
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
        }
    }
}
