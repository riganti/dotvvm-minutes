using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;

namespace DotvvmMinutes.UnsavedChangesHandler.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
		public List<int> Slides { get; set; } = new List<int>() { 1, 2, 3, 4, 5 };

        public int CurrentSlideId { get; set; } = 1;

        public int SelectedSlideId { get; set; } = 1;

        public string CommentText { get; set; }

        public void PostComment()
        {
            // TODO: save comment
            CommentText = string.Empty;
        }

        public void OnSlideChanged()
        {
            CurrentSlideId = SelectedSlideId;
            CommentText = string.Empty;
        }
    }
}
