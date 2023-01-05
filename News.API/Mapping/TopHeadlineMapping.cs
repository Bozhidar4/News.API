using AutoMapper;
using News.Domain.TopHeadlines;
using NewsAPI.Models;

namespace News.API.Mapping
{
    public class TopHeadlineMapping : Profile
    {
        public TopHeadlineMapping() 
        {
            CreateMap<Article, TopHeadline>()
                .ForMember(th => th.SourceId, opt => opt.Ignore());
        }
    }
}
