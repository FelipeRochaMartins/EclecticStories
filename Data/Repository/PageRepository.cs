using AutoMapper;
using Business.DataRepository;
using Data.Context;

namespace Data.Repository
{
    public class PageRepository : IPageRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;

        public PageRepository(IMapper mapper, EStoriesContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
