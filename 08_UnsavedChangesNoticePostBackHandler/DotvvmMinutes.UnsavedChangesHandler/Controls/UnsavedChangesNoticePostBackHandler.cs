using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotvvmMinutes.UnsavedChangesHandler.Controls
{
    public class UnsavedChangesNoticePostBackHandler : PostBackHandler
    {
        protected override string ClientHandlerName => "unsavedChangedNotice";

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DotvvmProperty TitleProperty
            = DotvvmProperty.Register<string, UnsavedChangesNoticePostBackHandler>(c => c.Title, "Discard changes");
        
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DotvvmProperty MessageProperty
            = DotvvmProperty.Register<string, UnsavedChangesNoticePostBackHandler>(c => c.Message, "Do you really want to leave this page and lose the changes?");

        public string ContinueButtonText
        {
            get { return (string)GetValue(ContinueButtonTextProperty); }
            set { SetValue(ContinueButtonTextProperty, value); }
        }
        public static readonly DotvvmProperty ContinueButtonTextProperty
            = DotvvmProperty.Register<string, UnsavedChangesNoticePostBackHandler>(c => c.ContinueButtonText, "Continue and discard changes");

        public string CancelButtonText
        {
            get { return (string)GetValue(CancelButtonTextProperty); }
            set { SetValue(CancelButtonTextProperty, value); }
        }
        public static readonly DotvvmProperty CancelButtonTextProperty
            = DotvvmProperty.Register<string, UnsavedChangesNoticePostBackHandler>(c => c.CancelButtonText, "Stay here");


        [MarkupOptions(AllowHardCodedValue = false, Required = true)]
        public bool HasUnsavedChangesBinding
        {
            get { return (bool)GetValue(HasUnsavedChangesBindingProperty); }
            set { SetValue(HasUnsavedChangesBindingProperty, value); }
        }
        public static readonly DotvvmProperty HasUnsavedChangesBindingProperty
            = DotvvmProperty.Register<bool, UnsavedChangesNoticePostBackHandler>(c => c.HasUnsavedChangesBinding, false);


        protected override Dictionary<string, object> GetHandlerOptions()
        {
            return new Dictionary<string, object>()
            {
                ["title"] = GetValueRaw(TitleProperty),
                ["message"] = GetValueRaw(MessageProperty),
                ["continueButtonText"] = GetValueRaw(ContinueButtonTextProperty),
                ["cancelButtonText"] = GetValueRaw(CancelButtonTextProperty),
                ["hasUnsavedChangesBinding"] = GetValueRaw(HasUnsavedChangesBindingProperty)
            };
        }
    }
}
