using News.Domain.Core;
using News.Domain.Sources;

namespace News.Domain.TopHeadlines
{
    public class TopHeadline : Entity<int>
    {
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? Content { get; set; }
        public string? CountryCode { get; set; }
        public int SourceId { get; set; }
        
        public Source Source { get; set; }
    }
}
