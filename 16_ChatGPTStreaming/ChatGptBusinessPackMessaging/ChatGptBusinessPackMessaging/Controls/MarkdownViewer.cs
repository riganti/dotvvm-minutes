using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace ChatGptBusinessPackMessaging.Controls
{
    public class MarkdownViewer : CompositeControl
    {

        public DotvvmControl GetContents(
            IValueBinding<string> text,
            HtmlCapability htmlCapability)
        {
            var dataBind = new KnockoutBindingGroup
            {
                { "markdown-viewer", text.GetKnockoutBindingExpression(this) }
            };

            return new HtmlGenericControl("div", htmlCapability)
                .AddAttribute("data-bind", dataBind.ToString());
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("markdown-viewer");
            base.OnPreRender(context);
        }
    }
}
