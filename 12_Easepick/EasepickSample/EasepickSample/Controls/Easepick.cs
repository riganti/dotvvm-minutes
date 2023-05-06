using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace EasepickSample.Controls;

public class Easepick : CompositeControl
{

    public DotvvmControl GetContents(
        IValueBinding<DateTime?> selectedDate,
        ICommandBinding? changed = null
    )
    {
        var binding = new KnockoutBindingGroup();
        binding.Add("easepick", selectedDate.GetKnockoutBindingExpression(this));

        return new HtmlGenericControl("input")
            .AddAttribute("data-bind", binding.ToString())
            .AddAttribute("onchange", changed != null 
                ? KnockoutHelper.GenerateClientPostBackScript("Changed", changed, this)
                : null);
    }

    protected override void OnPreRender(IDotvvmRequestContext context)
    {
        context.ResourceManager.AddRequiredResource("dotvvm-easepick");
        base.OnPreRender(context);
    }
}