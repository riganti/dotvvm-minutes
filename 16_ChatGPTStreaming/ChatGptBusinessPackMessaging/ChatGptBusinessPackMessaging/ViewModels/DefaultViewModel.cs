using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace ChatGptBusinessPackMessaging.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        public string CurrentMessage { get; set; }

        public List<string> Messages { get; set; } = new();

        public bool UserCanWrite { get; set; } = true;

        
        [FromRoute("conversationId")]
        public Guid? ConversationId { get; set; }

        public override Task Init()
        {
            if (ConversationId == null)
            {
                Context.RedirectToRoute("Default", new { conversationId = Guid.NewGuid() });
            }

            return base.Init();
        }
    }
}
