using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ServerRenderingDemo.Model;

namespace ServerRenderingDemo.Services
{
    public class BlogService
    {
        private const int PageSize = 4;

        private readonly Article[] articles;

        public BlogService(IWebHostEnvironment environment)
        {
            var path = Path.Combine(environment.ContentRootPath, "Data/articles.json");
            articles = JsonConvert.DeserializeObject<Article[]>(File.ReadAllText(path, Encoding.UTF8));
        }

        public List<ArticleListModel> GetArticleList(int pageIndex)
        {
            return articles
                .Skip(pageIndex * PageSize)
                .Take(PageSize)
                .Select(a => new ArticleListModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Abstract = GetAbstract(a.Html)
                })
                .ToList();
        }

        public ArticleDetailModel GetArticle(int id)
        {
            var article = articles.Single(a => a.Id == id);
            return new ArticleDetailModel()
            {
                Id = article.Id,
                Title = article.Title,
                Html = article.Html
            };
        }

        public int GetNumberOfPages()
        {
            return (int)Math.Ceiling(articles.Length / (double)PageSize);
        }

        private string GetAbstract(string html)
        {
            return HtmlConversions.GetFirstParagraphText(html);
        }
    }
}