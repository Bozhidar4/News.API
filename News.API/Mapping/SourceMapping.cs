using AutoMapper;

namespace News.API.Mapping
{
    public class SourceMapping : Profile
    {
        public SourceMapping()
        {
            CreateMap<NewsAPI.Models.Source, Domain.Sources.Source>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(s => s.NewsApiId, opt => opt.MapFrom(s => s.Id))
                .ForMember(s => s.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
