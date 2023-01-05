using News.Domain.Core;
using News.Domain.TopHeadlines;

namespace News.Domain.Sources
{
    public class Source : Entity<int>
    {
        public string? NewsApiId { get; set; }
        public string? Name { get; set; }
    }
}
