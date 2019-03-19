using DotvvmMinutes.LoadingAnimation.Extensions;
using DotVVM.Framework.Compilation.Styles;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Bootstrap4;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;
using Button = DotVVM.Framework.Controls.Button;

namespace DotvvmMinutes.LoadingAnimation
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);

            config.AddBootstrap4Configuration(new DotvvmBootstrapOptions()
            {
                IncludeBootstrapResourcesInPage = false,
                IncludeJQueryResourceInPage = false
            });

            //config.Styles.Register<Button>()
            //    .WithCondition(b => b.Property<object>(Button.IsSubmitButtonProperty).Equals(true))
            //    .SetControlProperty<LoadingAnimationPostBackHandler>(PostBack.HandlersProperty, controlBuilder => { }, 
            //        StyleOverrideOptions.Append)
            //    .SetDotvvmProperty(PostBack.ConcurrencyProperty, PostbackConcurrencyMode.Deny);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/default.dothtml");
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddMarkupControl("cc", "Cart", "Controls/Cart.dotcontrol");
            config.Markup.AddMarkupControl("cc", "BillingAddress", "Controls/BillingAddress.dotcontrol");
            config.Markup.AddMarkupControl("cc", "Payment", "Controls/Payment.dotcontrol");
            config.Markup.AddMarkupControl("cc", "Preferences", "Controls/Preferences.dotcontrol");

            config.Markup.AddCodeControls("cc", typeof(LoadingAnimationPostBackHandler));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("LoadingAnimationPostBackHandler", new ScriptResource()
            {
                Location = new UrlResourceLocation("~/scripts/LoadingAnimationPostBackHandler.js"),
                Dependencies = new [] { "dotvvm" }
            });
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
        }
    }
}
