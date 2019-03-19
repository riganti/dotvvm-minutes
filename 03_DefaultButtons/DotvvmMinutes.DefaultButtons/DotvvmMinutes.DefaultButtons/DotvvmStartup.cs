using DotvvmMinutes.DefaultButtons.Helpers;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;

namespace DotvvmMinutes.DefaultButtons
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);

            config.AddBusinessPackConfiguration();
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("BasicForm", "", "Views/BasicForm.dothtml");
            config.RouteTable.Add("Grid", "grid", "Views/Grid.dothtml");
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddCodeControls("cc", typeof(FormHelpers));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("FormHelpers", new ScriptResource()
            {
                Location = new UrlResourceLocation("~/FormHelpers.js"),
                Dependencies = new [] { "knockout", "jquery" }
            });
        }
    }
}
