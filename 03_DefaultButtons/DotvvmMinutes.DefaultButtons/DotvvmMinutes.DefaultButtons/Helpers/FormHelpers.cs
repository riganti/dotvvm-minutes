using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotvvmMinutes.DefaultButtons.Helpers
{
    [ContainsDotvvmProperties]
    public class FormHelpers
    {
        [AttachedProperty(typeof(bool))]
        [MarkupOptions(AllowBinding = false)]
        public static readonly DotvvmProperty DefaultButtonContainerProperty
            = DelegateActionProperty<bool>.Register<FormHelpers>("DefaultButtonContainer", AddDefaultButtonContainer);

        [AttachedProperty(typeof(bool))]
        [MarkupOptions(AllowBinding = false)]
        public static readonly DotvvmProperty IsDefaultButtonProperty
            = DelegateActionProperty<bool>.Register<FormHelpers>("IsDefaultButton", AddIsDefaultButton);

        [AttachedProperty(typeof(bool))]
        [MarkupOptions(AllowBinding = false)]
        public static readonly DotvvmProperty IsCancelButtonProperty
            = DelegateActionProperty<bool>.Register<FormHelpers>("IsCancelButton", AddIsCancelButton);


        private static void AddDefaultButtonContainer(IHtmlWriter writer, IDotvvmRequestContext context, DotvvmProperty property, DotvvmControl control)
        {
            writer.AddKnockoutDataBind("dotvvm-formhelpers-defaultbuttoncontainer", "true");
        }

        private static void AddIsDefaultButton(IHtmlWriter writer, IDotvvmRequestContext context, DotvvmProperty property, DotvvmControl control)
        {
            writer.AddAttribute("data-dotvvm-formhelpers-defaultbutton", "true");
        }

        private static void AddIsCancelButton(IHtmlWriter writer, IDotvvmRequestContext context, DotvvmProperty property, DotvvmControl control)
        {
            writer.AddAttribute("data-dotvvm-formhelpers-cancelbutton", "true");
        }
    }
}
