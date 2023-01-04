using News.Domain.Articles;
using News.Domain.Core;

namespace News.Domain.Sources
{
    public class Source : Entity<int>
    {
        public string Name { get; set; }
        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
