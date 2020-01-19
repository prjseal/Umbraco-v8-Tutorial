using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace CleanBlog.Core.ViewModels
{
    public class ArticleResultSet
    {
        public IEnumerable<IPublishedContent> Results { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public double PageCount { get; set; }
        public bool IsArticleListPage { get; set; }
    }
}
