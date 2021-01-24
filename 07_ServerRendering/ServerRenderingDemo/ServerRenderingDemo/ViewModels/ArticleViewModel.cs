using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using ServerRenderingDemo.Model;
using ServerRenderingDemo.Services;

namespace ServerRenderingDemo.ViewModels
{
    public class ArticleViewModel : MasterPageViewModel
    {
        private readonly BlogService blogService;

        [Bind(Direction.None)]
        [FromRoute("id")]
        public int Id { get; set; }

        [Bind(Direction.None)]
        public ArticleDetailModel Article { get; set; }


        public ArticleViewModel(BlogService blogService)
        {
            this.blogService = blogService;
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                Article = blogService.GetArticle(Id);
            }
            return base.PreRender();
        }
    }
}

