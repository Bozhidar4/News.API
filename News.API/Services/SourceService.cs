using AutoMapper;
using News.API.Models;
using News.API.Services.Interfaces;
using News.Domain.Sources;

namespace News.API.Services
{
    public class SourceService : ISourceService
    {
        private readonly IMapper _mapper;
        private readonly ISourceRepository _sourceRepository;

        public SourceService(
            IMapper mapper,
            ISourceRepository sourceRepository)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
        }

        public async Task<IEnumerable<SourceModel>> GetAsync()
        {
            return _mapper.Map<IEnumerable<SourceModel>>(
                await _sourceRepository.GetAllAsync());
        }
    }
}
