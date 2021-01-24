using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ServerRenderingDemo.Services
{
    public class HtmlConversions
    {

        /// <summary>
        /// Gets the first paragraph text from the specified HTML.
        /// </summary>
        public static string GetFirstParagraphText(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var sb = new StringBuilder();
            var paragraphs = doc.DocumentNode.Elements("p").ToList();
            var i = 0;
            while (sb.Length < 100 && i < paragraphs.Count)
            {
                GetTextFromHtmlNode(paragraphs[i], sb);
                i++;
            }

            // remove empty lines
            sb.Replace("\r\n\r\n", "\r\n");
            sb.Replace("\n\n", "\n");

            // return the text
            return sb.ToString().Trim();
        }

        private static readonly string[] BreakElements = new[] { "p", "div", "br", "tr", "h1", "h2", "h3", "h4", "h5", "h6", "li" };
        private static readonly string[] SkippedElements = new[] { "script" };
        /// <summary>
        /// Extracts a text content from a specified HTML element into a specified string builder
        /// </summary>
        private static void GetTextFromHtmlNode(HtmlNode htmlNode, StringBuilder sb)
        {
            if ((htmlNode.NodeType == HtmlNodeType.Text) && (htmlNode.InnerText.Trim() != string.Empty))
            {
                // current node is text node
                sb.Append(WebUtility.HtmlDecode(htmlNode.InnerText));
            }
            else if ((htmlNode.NodeType == HtmlNodeType.Element) || (htmlNode.NodeType == HtmlNodeType.Document))
            {
                // current node is element, if it is not style or script, dump its contents
                if (Array.IndexOf<string>(SkippedElements, htmlNode.Name.ToLower()) < 0)
                    foreach (HtmlNode n in htmlNode.ChildNodes)
                        GetTextFromHtmlNode(n, sb);

                // add a newline after block elements
                if (Array.IndexOf<string>(BreakElements, htmlNode.Name.ToLower()) >= 0)
                    sb.Append(Environment.NewLine);
            }
        }

    }
}
