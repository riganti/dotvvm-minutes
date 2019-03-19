using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotvvmMinutes.LoadingAnimation.Extensions
{
    public class LoadingAnimationPostBackHandler : PostBackHandler
    {

        [MarkupOptions(AllowBinding = false)]
        public string LoadingIconCssClass
        {
            get { return (string)GetValue(LoadingIconCssClassProperty); }
            set { SetValue(LoadingIconCssClassProperty, value); }
        }
        public static readonly DotvvmProperty LoadingIconCssClassProperty
            = DotvvmProperty.Register<string, LoadingAnimationPostBackHandler>(c => c.LoadingIconCssClass, "fa fa-spinner fa-pulse fa-fw margin-bottom");


        [MarkupOptions(AllowBinding = false)]
        public string ButtonCssClass
        {
            get { return (string)GetValue(ButtonCssClassProperty); }
            set { SetValue(ButtonCssClassProperty, value); }
        }
        public static readonly DotvvmProperty ButtonCssClassProperty
            = DotvvmProperty.Register<string, LoadingAnimationPostBackHandler>(c => c.ButtonCssClass, "disabled");
        

        protected override Dictionary<string, object> GetHandlerOptions()
        {
            return new Dictionary<string, object>()
            {
                ["loadingIconCssClass"] = GetValueRaw(LoadingIconCssClassProperty),
                ["buttonCssClass"] = GetValueRaw(ButtonCssClassProperty)
            };
        }

        protected override string ClientHandlerName => "loadingAnimation";
    }
}
