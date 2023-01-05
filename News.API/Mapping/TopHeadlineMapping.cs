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
                .ForMember(th => th.SourceId, opt => opt.Ignore())
                .ForPath(th => th.Source.NewsApiId, opt => opt.MapFrom(a => a.Source.Id))
                .ForPath(th => th.Source.Name, opt => opt.MapFrom(a => a.Source.Name));
        }
    }
}
