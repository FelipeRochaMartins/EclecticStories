using AutoMapper;
using Business.DataRepository;
using Data.Context;

namespace Data.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;

        public HistoryRepository(IMapper mapper, EStoriesContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
