using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Volvox.Helios.Service;
using Volvox.Helios.Service.Clients;

namespace Tests.Integration.Infrastructure
{
    public class CustomWebApplicationFactory_Authed<TStartup> : WebApplicationFactory<Volvox.Helios.Web.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //TODO: seed in memory db with test data
                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                services.AddDbContext<VolvoxHeliosContext>(options =>
                {
                    options.UseInMemoryDatabase("HeliosInMemory");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddHttpClient<IDiscordAPIClient, TestDiscordAPIClient>();

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<VolvoxHeliosContext>();
                    var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory_Authed<TStartup>>>();

                    db.Database.EnsureCreated();
                }

            });
            base.ConfigureWebHost(builder);
        }
    }
}
