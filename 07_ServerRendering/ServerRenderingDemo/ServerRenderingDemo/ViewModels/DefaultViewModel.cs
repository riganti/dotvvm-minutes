using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using ServerRenderingDemo.Model;
using ServerRenderingDemo.Services;

namespace ServerRenderingDemo.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly BlogService blogService;

        [FromQuery("page")]
        public int PageIndex { get; set; }

        public int PagesCount { get; set; }

        public List<ArticleListModel> Articles { get; set; }


        public DefaultViewModel(BlogService blogService)
        {
            this.blogService = blogService;
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                Articles = blogService.GetArticleList(PageIndex);
                PagesCount = blogService.GetNumberOfPages();
            }

            return base.PreRender();
        }
    }
}
