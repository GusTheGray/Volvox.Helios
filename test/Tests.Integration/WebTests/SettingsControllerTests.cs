using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tests.Integration.Infrastructure;
using Xunit;

namespace Tests.Integration.WebTests
{
    public class SettingsControllerTests : IClassFixture<CustomWebApplicationFactory_Authed<Volvox.Helios.Web.Startup>>
    {
        private readonly CustomWebApplicationFactory_Authed<Volvox.Helios.Web.Startup> _webApplicationFactory;

        public SettingsControllerTests(CustomWebApplicationFactory_Authed<Volvox.Helios.Web.Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task Get_Settings()
        {
            var client = _webApplicationFactory.CreateClient();

            var respsonse = await client.GetAsync("/Settings");

            respsonse.EnsureSuccessStatusCode();
        }

    }
}
