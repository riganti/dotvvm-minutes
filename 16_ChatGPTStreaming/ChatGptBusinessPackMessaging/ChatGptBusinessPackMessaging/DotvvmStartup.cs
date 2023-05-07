using ChatGptBusinessPackMessaging.Controls;
using DotVVM.BusinessPack.Messaging;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGptBusinessPackMessaging
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "{conversationId?:guid}", "Views/Default.dothtml");
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddCodeControls("cc", typeof(MarkdownViewer));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.RegisterStylesheetFile("bootstrap", "wwwroot/lib/bootstrap/css/bootstrap.min.css");
            config.Resources.RegisterStylesheetFile("bootstrap-icons", "wwwroot/lib/bootstrap-icons/font/bootstrap-icons.min.css");
            config.Resources.RegisterStylesheetFile("Styles", "wwwroot/style.css", dependencies: new [] { "bootstrap", "bootstrap-icons" });
            
            config.Resources.RegisterScriptFile("showdown", "wwwroot/lib/showdown/showdown.min.js");
            config.Resources.RegisterScriptFile("markdown-viewer", "wwwroot/markdown-viewer.js", dependencies: new[] { "dotvvm", "showdown" });
        }

		public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
            options.AddHotReload();
            options.AddBusinessPackMessaging();
		}
    }
}
