using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmMinutes.SimpleShop
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);

            config.RegisterApiClient(typeof(Api.Client), "https://dotvvmminutesdemo.azurewebsites.net/api/", "wwwroot/Scripts/ApiClient.js", "_functions");
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/default.dothtml");
            config.RouteTable.Add("Product", "product/{Id}", "Views/ProductDetail.dothtml");
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AddMarkupControl("cc", "Rating", "Controls/Rating.dotcontrol");
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
        }
        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
        }
    }
}
