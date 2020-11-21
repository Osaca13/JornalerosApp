using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(JornalerosApp.Areas.Identity.IdentityHostingStartup))]
namespace JornalerosApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}